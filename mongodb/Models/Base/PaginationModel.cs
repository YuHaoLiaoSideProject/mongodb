using mongodb.Enums.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongodb.Models.Base
{
    public class PaginationModel
    {

        public PaginationModel()
        {
            this.Page = 1;
            this.PageSize = 10;
            this.Order = SortDirectionEnum.DESC;
        }


        /// <summary>
        /// 目前頁數。(從1開始)
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每頁幾筆資料，預設為10。
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 總共資料數。
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 總共頁數。
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 排序欄位。
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// 排序方式為升冪（asc）或降冪（desc），預設為 desc。
        /// </summary>
        public SortDirectionEnum Order { get; set; }
    }
}
