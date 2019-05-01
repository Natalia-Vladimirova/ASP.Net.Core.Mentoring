// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace NorthwindApp.Api.SDK.v1.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class BaseCategory
    {
        /// <summary>
        /// Initializes a new instance of the BaseCategory class.
        /// </summary>
        public BaseCategory()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the BaseCategory class.
        /// </summary>
        public BaseCategory(int? categoryId = default(int?), string categoryName = default(string))
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "categoryId")]
        public int? CategoryId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }

    }
}