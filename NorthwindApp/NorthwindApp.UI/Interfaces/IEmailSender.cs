using System.Threading.Tasks;

namespace NorthwindApp.UI.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string content);
    }
}
