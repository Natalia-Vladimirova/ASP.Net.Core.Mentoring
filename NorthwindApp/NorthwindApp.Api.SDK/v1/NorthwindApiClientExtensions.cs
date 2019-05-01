// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace NorthwindApp.Api.SDK.v1
{
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for NorthwindApiClient.
    /// </summary>
    public static partial class NorthwindApiClientExtensions
    {
            /// <summary>
            /// Gets a list of categories
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<Category> GetCategories(this INorthwindApiClient operations)
            {
                return operations.GetCategoriesAsync().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Gets a list of categories
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<Category>> GetCategoriesAsync(this INorthwindApiClient operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetCategoriesWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Gets an image of a category
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Category id
            /// </param>
            public static object GetCategoryImage(this INorthwindApiClient operations, int id)
            {
                return operations.GetCategoryImageAsync(id).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Gets an image of a category
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Category id
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetCategoryImageAsync(this INorthwindApiClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetCategoryImageWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Uploads new image to specified category
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Category id
            /// </param>
            /// <param name='image'>
            /// </param>
            public static ProblemDetails UploadCategoryImage(this INorthwindApiClient operations, int id, Stream image = default(Stream))
            {
                return operations.UploadCategoryImageAsync(id, image).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Uploads new image to specified category
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Category id
            /// </param>
            /// <param name='image'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ProblemDetails> UploadCategoryImageAsync(this INorthwindApiClient operations, int id, Stream image = default(Stream), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.UploadCategoryImageWithHttpMessagesAsync(id, image, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Gets a list of products
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='page'>
            /// A page of products which should be returned
            /// </param>
            /// <param name='pageSize'>
            /// A number of products which should be returned. Set to 0 to get all products
            /// </param>
            public static object GetProducts(this INorthwindApiClient operations, int? page = default(int?), int? pageSize = default(int?))
            {
                return operations.GetProductsAsync(page, pageSize).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Gets a list of products
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='page'>
            /// A page of products which should be returned
            /// </param>
            /// <param name='pageSize'>
            /// A number of products which should be returned. Set to 0 to get all products
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetProductsAsync(this INorthwindApiClient operations, int? page = default(int?), int? pageSize = default(int?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetProductsWithHttpMessagesAsync(page, pageSize, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Creates new product
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='product'>
            /// A product which should be created
            /// </param>
            public static Product CreateProduct(this INorthwindApiClient operations, Product product = default(Product))
            {
                return operations.CreateProductAsync(product).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Creates new product
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='product'>
            /// A product which should be created
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Product> CreateProductAsync(this INorthwindApiClient operations, Product product = default(Product), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CreateProductWithHttpMessagesAsync(product, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Gets a product by id
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Product id
            /// </param>
            public static object GetProduct(this INorthwindApiClient operations, int id)
            {
                return operations.GetProductAsync(id).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Gets a product by id
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Product id
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetProductAsync(this INorthwindApiClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetProductWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Updates an existing product
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of a product to update
            /// </param>
            /// <param name='product'>
            /// A product to update
            /// </param>
            public static ProblemDetails UpdateProduct(this INorthwindApiClient operations, int id, Product product = default(Product))
            {
                return operations.UpdateProductAsync(id, product).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Updates an existing product
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of a product to update
            /// </param>
            /// <param name='product'>
            /// A product to update
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ProblemDetails> UpdateProductAsync(this INorthwindApiClient operations, int id, Product product = default(Product), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.UpdateProductWithHttpMessagesAsync(id, product, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Deletes an existing product
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of a product to delete
            /// </param>
            public static ProblemDetails DeleteProduct(this INorthwindApiClient operations, int id)
            {
                return operations.DeleteProductAsync(id).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Deletes an existing product
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of a product to delete
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ProblemDetails> DeleteProductAsync(this INorthwindApiClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.DeleteProductWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}