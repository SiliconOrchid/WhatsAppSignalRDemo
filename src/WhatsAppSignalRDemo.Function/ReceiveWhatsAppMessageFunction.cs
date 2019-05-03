using System.IO;
using System.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

using WhatsAppSignalRDemo.Function.Models;
using WhatsAppSignalRDemo.Function.Common;

namespace WhatsAppSignalRDemo.Function
{
    public class ReceiveWhatsAppMessageFunction
    {
        private GenericConfig _genericConfig;

        [FunctionName(GlobalConstants.FunctionName_ReceiveWhatsAppMessage)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]           
            HttpRequest httpRequest,
            ExecutionContext executionContext) 
        {
            _genericConfig = StartupHelper.GetConfig(executionContext);

            string requestBody = await new StreamReader(httpRequest.Body).ReadToEndAsync();

            UserMessage userMessage = new UserMessage
            {
                User = HttpUtility.ParseQueryString(requestBody).Get("From"),
                Message = HttpUtility.ParseQueryString(requestBody).Get("Body")
            };

            HttpHelper.PostMessage(
                _genericConfig.ApiEndpoint + GlobalConstants.FunctionName_BroadcastSignalRMessage, 
                userMessage);

            return new OkResult();
        }
    }
}
