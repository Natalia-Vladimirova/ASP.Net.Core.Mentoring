using HeyRed.Mime;
using NorthwindApp.Core.Interfaces;

namespace NorthwindApp.Core.Services
{
    public class MimeHelper : IMimeHelper
    {
        public string GetMimeType(byte[] content)
        {
            return MimeGuesser.GuessMimeType(content);
        }
    }
}
