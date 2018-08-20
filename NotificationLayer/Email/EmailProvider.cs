using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;
using log4net;
using Newtonsoft.Json;
using NotificationLayer.Email.Models;
using Common;

namespace NotificationLayer
{
    public class EmailProvider : IEmailProvider
    {
        private readonly IConfiguration _config;
        private readonly ILog _logger;

        public EmailProvider(IConfiguration Configuration)
        {
            _config = Configuration;
            _logger = LogManager.GetLogger(typeof(EmailProvider));
        }

        public async Task ExecuteAsync(EmailProviderParameter parameter)
        {
            try
            {
                var apiKey = _config["SendGrid:TOKEN"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(parameter.mail_from, parameter.mail_from_title);
                var subject = parameter.mail_subject;


                var plainTextContent = parameter.plain_text_content;
                var htmlContent = parameter.html_content;
                SendGridMessage msg;
                if (parameter.mail_tos != null && parameter.mail_tos.Any())
                {
                    var tos = parameter.mail_tos.Select(t => new EmailAddress(t.Key, t.Value)).ToList();
                    msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, htmlContent);
                }
                else
                {
                    var to = new EmailAddress(parameter.mail_to, parameter.mail_to_title);
                    msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                }
                // set email tempate if any
                if (!string.IsNullOrEmpty(parameter.template_id))
                {
                    msg.TemplateId = parameter.template_id;
                }

                // merge fields
                if (parameter.merge_fields != null && parameter.merge_fields.Any())
                {
                    msg.AddSubstitutions(parameter.merge_fields);
                }

                await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmailTemplates> GetTemplatesAsync()
        {
            try
            {
                _logger.Info(string.Format(LogFormat.ServiceStart, MethodBase.GetCurrentMethod().Name));
                var sg = new SendGridClient(_config["SendGrid:TOKEN"]);
                var response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "templates");
                var content = await response.Body.ReadAsStringAsync();
                var templates = JsonConvert.DeserializeObject<EmailTemplates>(content);
                return templates;
            }
            catch (Exception ex)
            {
                _logger.Error((ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
                throw;
            }

            finally
            {
                _logger.Info(string.Format(LogFormat.ServiceEnd, MethodBase.GetCurrentMethod().Name));
            }
        }

        public async Task<EmailTemplate> GetTemplateByNaneAsync(string templateName)
        {
            try
            {
                _logger.Info(string.Format(LogFormat.ServiceStart, MethodBase.GetCurrentMethod().Name));
                var templates = await this.GetTemplatesAsync();
                if (templates != null && templates.templates.Any())
                {
                    return templates.templates.FirstOrDefault(t => t.name == templateName);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.Error((ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
                throw;
            }

            finally
            {
                _logger.Info(string.Format(LogFormat.ServiceEnd, MethodBase.GetCurrentMethod().Name));
            }
        }
    }
}
