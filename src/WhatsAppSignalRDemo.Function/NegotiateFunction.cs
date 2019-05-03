using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.AspNetCore.Http;

using WhatsAppSignalRDemo.Function.Common;

namespace WhatsAppSignalRDemo.Function
{
    public class NegotiateFunction
    {
        // with code from https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-signalr-service
        [FunctionName(GlobalConstants.FunctionName_Negotiate)]
        public SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous)]HttpRequest req,
            [SignalRConnectionInfo(HubName = GlobalConstants.SignalR_HubName)]SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
    }
}
