namespace WhatsAppSignalRDemo.Function.Common
{
    public static class GlobalConstants
    {
        public const string SignalR_HubName = "Chat";
        public const string SignalR_TargetGroup = "AllUsers";

        public const string FunctionName_Negotiate = "negotiate";
        public const string FunctionName_BroadcastSignalRMessage = "BroadcastSignalRMessage";
        public const string FunctionName_SendWhatsAppMessage = "SendWhatsAppMessage";
        public const string FunctionName_ReceiveWhatsAppMessage = "ReceiveWhatsAppMessage";

        public const string Config_TwilioAccountSid = "TwilioAccountSid";
        public const string Config_TwilioAuthToken = "TwilioAuthToken";
        public const string Config_TwilioWhatsAppPhoneNumber = "TwilioWhatsAppPhoneNumber";
    }
}
