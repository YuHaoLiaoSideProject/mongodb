﻿using Newtonsoft.Json;
using Omu.ValueInjecter;
using Omu.ValueInjecter.Injections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace mongodb.Extension
{
    /// <summary>
    /// Object 的擴充方法
    /// </summary>
    public static class ExtensionOfObject
    {

        /// <summary>
        /// 將class中null字串改為空字串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public static void SetNullStringToEmpty<T>(this T model) where T : class
        {
            foreach (PropertyInfo item in model.GetType().GetProperties())
            {
                if (item.PropertyType == typeof(string) == false)
                    continue;

                var myValue = item.GetValue(model);

                if (myValue == null)
                {
                    item.SetValue(model, "");
                }
            }
        }

        public static List<T> ConvertToModel<S, T>(this List<S> input) where T : new()
        {
            return input.Select(e => ConvertToModel<S, T>(e)).ToList();
        }

        public static T ConvertToModel<S, T>(this S input) where T : new()
        {
            T result = new T();
            foreach (var item in typeof(T).GetProperties())
            {
                var value = typeof(S).GetProperty(item.Name)?.GetValue(input);
                item.SetValue(result, value);
            }
            return result;
        }

        /// <summary>
        /// 在物件下取某個欄位的值,若物件為NULL或物件下某個欄位為NULL則回傳default
        /// </summary>
        /// <typeparam name="S">物件型別</typeparam>
        /// <typeparam name="T">屬性型別</typeparam>
        /// <param name="obj">物件</param>
        /// <param name="SelectFunc">要取得的值</param>
        /// <param name="DefaultValue">物件或Select的值為null時的回傳值</param>
        /// <returns></returns>
        public static T GetValueOrDefaultValue<S, T>(this S obj, Func<S, T> SelectFunc, T DefaultValue = default(T))
        {
            if (obj == null || SelectFunc(obj) == null)
            {
                return DefaultValue;
            }
            return SelectFunc(obj);
        }


        public static T ReturnInjectObjectStringColumns<T>(this object fromModel) where T : new()
        {
            if (fromModel == null)
                return default(T);

            var model2 = new T();
            model2.InjectFrom<NumberStringAutoInject>(fromModel);
            return model2;
        }


        public class NumberStringAutoInject : LoopInjection
        {
            public bool IsSpecialMatch { get; set; }


            public static List<Type> types = new List<Type>
            {
                typeof(int),
                typeof(long),
                typeof(byte),
                typeof(short),
            };


            protected override bool MatchTypes(Type source, Type target)
            {
                //特殊轉換 string to int或相反
                if (types.Contains(source) && target == typeof(string))
                {
                    IsSpecialMatch = true;
                    return true;
                }

                if (source == typeof(string) && types.Contains(target))
                {
                    IsSpecialMatch = true;
                    return true;
                }

                return source == target;
            }


            protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
            {
                try
                {
                    //如果是我方指定的特殊型別 欄位轉換
                    if (IsSpecialMatch)
                    {
                        object myValue = Convert.ChangeType(sp.GetValue(source), tp.PropertyType);
                        tp.SetValue(target, myValue);
                    }
                    else
                    {
                        base.SetValue(source, target, sp, tp);
                    }
                }
                catch (Exception ex)
                {

                }

                IsSpecialMatch = false; //重製
            }
        }


        public class NumberStringAutoInjectNullable : LoopInjection
        {
            protected override bool MatchTypes(Type sourceType, Type targetType)
            {
                var sourceNullableType = Nullable.GetUnderlyingType(sourceType);
                var targetNullableType = Nullable.GetUnderlyingType(targetType);

                return sourceType == targetType
                       || sourceType == targetNullableType
                       || targetType == sourceNullableType
                       || sourceNullableType == targetNullableType;
            }
        }

        /// <summary>
        /// 把有指定 NullValueHandling.Ignore 或 JsonIgnore的欄位設為 null
        /// </summary>
        public static void SetListObjectJsonIgnorePropertyNull<T>(this IEnumerable<T> listObj) where T : class
        {
            foreach (var item in listObj)
            {
                SetJsonIgnorePropertyNull(item);
            }
        }


        /// <summary>
        /// 把有指定 NullValueHandling.Ignore 或 JsonIgnore的欄位設為 null
        /// </summary>
        public static void SetJsonIgnorePropertyNull<T>(this T obj) where T : class
        {
            foreach (var item in obj.GetType().GetProperties())
            {
                bool isAttrExist = Attribute.IsDefined(item, typeof(JsonIgnoreAttribute));

                //case 1. 透過JsonIgnore指定，若符合就把欄位值設成null
                if (isAttrExist)
                {
                    item.SetValue(obj, null);
                    continue;
                }

                //case 2. 透過NullValueHandling.Ignore 指定
                var jsonProperties = item.GetCustomAttributes(typeof(JsonPropertyAttribute), true);

                if (jsonProperties.IsEmptyOrNull())
                    continue;

                var matchFirst = jsonProperties[0] as JsonPropertyAttribute;

                if (matchFirst != null && matchFirst.NullValueHandling == NullValueHandling.Ignore)
                {
                    item.SetValue(obj, null);
                    continue;
                }
            }
        }




        public static string ToGetJsonString(this object obj, ReferenceLoopHandling loopHanding)
        {
            if (obj == null)
                return null;

            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = loopHanding
            });
        }



        public static T TryJsonConvertToModel<T>(this string jsonText)
        {
            try
            {
                return JsonConvertToModel<T>(jsonText);
            }
            catch
            {

            }
            return default(T);
        }



        public static T JsonConvertToModel<T>(this string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
                return default(T);

            return JsonConvert.DeserializeObject<T>(jsonText);
        }




        public static string ToGetJsonString(this object obj)
        {
            if (obj == null)
                return null;

            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 將Dictionary轉成 指定的T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T DictionaryToObject<T, T2>(this IDictionary<string, T2> dic) where T : new()
        {
            string jsonString = JsonConvert.SerializeObject(dic);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static List<S> ReturnInjectListObject<T, S>(this IEnumerable<T> fromModel)
            where T : new()
            where S : new()
        {
            if (fromModel == null)
                return new List<S>();

            return fromModel.Select(e => e.ReturnInjectObject<S>()).ToList();
        }
        public static List<T> ReturnInjectListObject<T>(this IEnumerable<object> fromModel)
           where T : new()
        {
            return ReturnInjectListObject<object, T>(fromModel);
        }

        public static T ReturnInjectObject<T>(this object fromModel) where T : new()
        {
            if (fromModel == null)
                return default(T);

            var model2 = new T();
            model2.InjectFrom<NumberStringAutoInject>(fromModel);
            return model2;
        }


        public static T ReturnInjectObjectNullable<T>(this object fromModel) where T : new()
        {
            if (fromModel == null)
                return default(T);

            var model2 = new T();
            model2.InjectFrom<NumberStringAutoInjectNullable>(fromModel);
            return model2;
        }


        public static void InjecterFrom(this object model, object fromModel)
        {
            if (fromModel == null)
                return;

            model.InjectFrom(fromModel);
        }

        public static List<KeyValuePair<string, object>> ToKeyValuePair(this object fromModel)
        {
            List<KeyValuePair<string, object>> keyvaluePairs = new List<KeyValuePair<string, object>>();

            foreach (var prop in fromModel.GetType().GetProperties())
            {
                var value = prop.GetValue(fromModel, null);
                if (value != null)
                {
                    Attribute routingAttribute = Attribute.GetCustomAttribute(prop, typeof(JsonPropertyAttribute));
                    JsonPropertyAttribute jsonAttribute = (JsonPropertyAttribute)routingAttribute;

                    if (jsonAttribute != null)
                        //如果有JsonPropertyAttribute就以裡面的值為主
                        keyvaluePairs.Add(new KeyValuePair<string, object>(jsonAttribute.PropertyName, value.ToString()));
                    else
                        //用預設的property name
                        keyvaluePairs.Add(new KeyValuePair<string, object>(prop.Name, value.ToString()));
                }
            }

            return keyvaluePairs;
        }

        public static string ToGetXmlString(this object fromModel, string xmlNameSpacePrefix = "", string xmlNameSpaceValue = "")
        {
            XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();
            xmlNameSpace.Add(xmlNameSpacePrefix, xmlNameSpaceValue);

            var stringWriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(fromModel.GetType());
            serializer.Serialize(stringWriter, fromModel, xmlNameSpace);
            return stringWriter.ToString();
        }

        public static List<T> SplitAndConvert<T>(this string text, char splitToken = ',')
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<T>();

            var convertList = text.Split(splitToken).Select(z => (T)Convert.ChangeType(z, typeof(T))).ToList();
            return convertList;
        }

        public static string ToGetQueryString(this object fromModel)
        {
            StringBuilder result = new StringBuilder();

            foreach (var prop in fromModel.GetType().GetProperties())
            {
                var value = prop.GetValue(fromModel, null);
                if (value != null)
                    result.Append($"&{prop.Name}={value}");
            }

            return result.ToString().Substring(1);
        }

        public static T GetPropValue<T>(this object src, string propName)
        {
            var value = src.GetType().GetProperties()
                .Where(e => String.Equals(e.Name, propName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().GetValue(src, null);

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static bool IsContainProperty(this object instance, string propName)
        {
            if (instance != null && !string.IsNullOrEmpty(propName))
            {
                return instance.GetType().GetProperties()
                    .Any(e => e.Name.ToUpper() == propName.ToUpper());
            }
            return false;
        }
    }
}
