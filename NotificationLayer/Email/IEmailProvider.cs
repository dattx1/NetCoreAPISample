using System.Threading.Tasks;
using NotificationLayer.Email.Models;
using SendGrid;

namespace NotificationLayer
{
    public interface IEmailProvider
    {
        Task ExecuteAsync(EmailProviderParameter parameter);
        Task<EmailTemplates> GetTemplatesAsync();
        Task<EmailTemplate> GetTemplateByNaneAsync(string templateName);
    }
}
