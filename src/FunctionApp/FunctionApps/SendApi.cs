using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PostmarkDotNet;
using RazorLight.Samples.Models;
using System;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;

namespace RazorLight.Samples.FunctionApps
{
    public class SendApi
    {
        private readonly IRazorLightEngine _engine;
        private readonly PostmarkClient _postmarkClient;

        public SendApi(IRazorLightEngine engine, PostmarkClient postmarkClient)
        {
            _engine = engine;
            _postmarkClient = postmarkClient;
        }

        [FunctionName(nameof(SendMail))]
        public async Task<IActionResult> SendMail(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var sendRequest = JsonConvert.DeserializeObject<SendMailRequest>(requestBody);

                var templateModel = JsonConvert.DeserializeObject<WelcomeTemplate>(sendRequest.TemplateModel.ToString());
                var templatePath = "Templates." + templateModel.TemplateName;
                dynamic viewBag = new ExpandoObject();
                viewBag.Title = "myTitle";
                viewBag.CompanyName = "COmpany!";
                viewBag.CompanyAddress = "myaddress";

                var htmlBody = await _engine.CompileRenderAsync(templatePath, templateModel, viewBag);

                var postmarkMessage = new PostmarkMessage()
                {
                    To = sendRequest.To,
                    From = sendRequest.From,
                    Subject = sendRequest.Subject,
                    HtmlBody = htmlBody,
                };

                var result = await _postmarkClient.SendMessageAsync(postmarkMessage);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
