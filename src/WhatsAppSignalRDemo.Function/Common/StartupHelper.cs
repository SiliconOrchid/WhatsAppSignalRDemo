using System;

using Microsoft.Azure.WebJobs;

using WhatsAppSignalRDemo.Function.Models;

namespace WhatsAppSignalRDemo.Function.Common
{
    public static class StartupHelper
    {
        public static GenericConfig GetConfig(ExecutionContext executionContext)
        {
            string functionHostName = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");

            return new GenericConfig
            {
                TwilioAccountSid = Environment.GetEnvironmentVariable(GlobalConstants.Config_TwilioAccountSid), 
                TwilioAuthToken = Environment.GetEnvironmentVariable(GlobalConstants.Config_TwilioAuthToken), 
                TwilioWhatsAppPhoneNumber = Environment.GetEnvironmentVariable(GlobalConstants.Config_TwilioWhatsAppPhoneNumber),
                ApiEndpoint = $"http://{functionHostName}/api/"
            };
        }
    }
}
