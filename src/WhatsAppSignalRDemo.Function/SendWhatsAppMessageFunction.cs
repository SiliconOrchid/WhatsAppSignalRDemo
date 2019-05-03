using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;
using Twilio;
using Twilio.Base;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

using WhatsAppSignalRDemo.Function.Models;
using WhatsAppSignalRDemo.Function.Common;


namespace WhatsAppSignalRDemo.Function
{
    public class SendWhatsAppMessageFunction
    {
        private GenericConfig _genericConfig;

        [FunctionName(GlobalConstants.FunctionName_SendWhatsAppMessage)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest httpRequest,
            ExecutionContext executionContext
            )
        {

            _genericConfig = StartupHelper.GetConfig(executionContext);

            string requestBody = await new StreamReader(httpRequest.Body).ReadToEndAsync();
            dynamic requestObject = JsonConvert.DeserializeObject(requestBody);

            // connect to Twilio
            TwilioClient.Init(_genericConfig.TwilioAccountSid, _genericConfig.TwilioAuthToken);

            // query twilio api ... and get distinct phone numbers of whatsapp users subscribed to chat
            ResourceSet<MessageResource> messageCollection = await MessageResource.ReadAsync(to: _genericConfig.TwilioWhatsAppPhoneNumber);
            IEnumerable<PhoneNumber> distinctPhoneNumbers = messageCollection.GroupBy(a => a.From.ToString()).Select(b => b.FirstOrDefault().From);

            foreach(var phoneNumber in distinctPhoneNumbers)
            {
                if (phoneNumber.ToString() == requestObject.User.ToString())
                {
                    // if message originated from whatsapp, don't send a copy straight back to sender
                    continue;
                }

                // send message to whatsapp user
                var message = MessageResource.Create(
                    from: new Twilio.Types.PhoneNumber(_genericConfig.TwilioWhatsAppPhoneNumber),
                    body: $"{requestObject.User} : {requestObject.Message}",
                    to: phoneNumber
                );
            }

            return (ActionResult)new OkResult();
        }
    }
}
