using MimeDetective;
using NorthwindApp.Core.Interfaces;

namespace NorthwindApp.Core.Services
{
    public class MimeHelper : IMimeHelper
    {
        private const string DefaultMimeType = "application/octet-stream";

        public string GetMimeType(byte[] content)
        {
            return content?.GetFileType()?.Mime ?? DefaultMimeType;
        }
    }
}
