using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

using WhatsAppSignalRDemo.Function.Common;
using WhatsAppSignalRDemo.Function.Models;

namespace WhatsAppSignalRDemo.Function
{
    public class BroadcastSignalRMessageFunction
    {
        private GenericConfig _genericConfig;

        [FunctionName(GlobalConstants.FunctionName_BroadcastSignalRMessage)]
        public Task SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]object message,
            [SignalR(HubName = GlobalConstants.SignalR_HubName)]IAsyncCollector<SignalRMessage> signalRMessages,
            ExecutionContext executionContext)
        {
            _genericConfig = StartupHelper.GetConfig(executionContext);

            // relay the message to the WhatsApp API
            HttpHelper.PostMessage(_genericConfig.ApiEndpoint + GlobalConstants.FunctionName_SendWhatsAppMessage, message);

            //relay the message to the SignalR Service
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = GlobalConstants.SignalR_TargetGroup,
                    Arguments = new[] { message }
                });
        }
    }
}
