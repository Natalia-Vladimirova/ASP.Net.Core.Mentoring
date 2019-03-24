using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NorthwindApp.Core.Interfaces;
using NorthwindApp.UI.Interfaces;

namespace NorthwindApp.UI.Infrastructure
{
    public class ImagesCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;
        private readonly ICacheService _cacheService;
        private readonly IMimeHelper _mimeHelper;

        public ImagesCacheMiddleware(
            RequestDelegate next, 
            IHostingEnvironment env, 
            ICacheService cacheService,
            IMimeHelper mimeHelper)
        {
            _next = next;
            _env = env;
            _cacheService = cacheService;
            _mimeHelper = mimeHelper;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;

            if (!path.StartsWithSegments("/images", System.StringComparison.OrdinalIgnoreCase) &&
                !path.StartsWithSegments("/category/image", System.StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            var fileName = Path.GetFileName(context.Request.Path.Value);

            if (_cacheService.IsCached(fileName))
            {
                var content = await _cacheService.GetFileAsync(fileName);
                var contentType = _mimeHelper.GetMimeType(content);

                context.Response.ContentType = contentType;
                await context.Response.Body.WriteAsync(content);

                return;
            }

            var originalBody = context.Response.Body;
            byte[] responseContent;

            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await _next(context);

                    memStream.Seek(0, SeekOrigin.Begin);
                    responseContent = memStream.ToArray();

                    memStream.Seek(0, SeekOrigin.Begin);
                    await memStream.CopyToAsync(originalBody);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }

            if (!string.IsNullOrWhiteSpace(context.Response.ContentType) &&
                context.Response.ContentType.StartsWith("image") &&
                _cacheService.CanBeCached())
            {
                await _cacheService.AddFileAsync(fileName, responseContent);
            }
        }
    }

    public static class ImagesCacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseImagesCache(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ImagesCacheMiddleware>();
        }
    }
}
