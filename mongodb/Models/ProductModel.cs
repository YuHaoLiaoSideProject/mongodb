using mongodb.Modelss.Base;

namespace mongodb.Models
{

    public class ProductModel : BaseMongoDBModel
    {

        public string Name { get; set; }
    }
}
