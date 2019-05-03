using System.IO;
using System.Net;

using Newtonsoft.Json;

namespace WhatsAppSignalRDemo.Function.Common
{
    public static class HttpHelper
    {
        public static void PostMessage (string apiUrl, object message)
        {
            //send request
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(message));
                streamWriter.Flush();
                streamWriter.Close();
            }

            //get response
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}
