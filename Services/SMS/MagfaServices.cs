using RestSharp;
using RestSharp.Authenticators;


namespace Services.SMS
{
    public static class MagfaServices
    {


        public static IRestResponse MagfaSendSMS(Entities.Basic.SMS.MessageSend messageSend)
        {

            string username = messageSend.MessageSend_SmsProvider.UserName;
            string password = messageSend.MessageSend_SmsProvider.Password;
            string domain = messageSend.MessageSend_SmsProvider.DomainName;
            string phoneSender = messageSend.MessageSend_SmsProvider.PhonSender;

            var client = new RestClient(messageSend.MessageSend_SmsProvider.MethodSendUrl);

            client.Authenticator = new HttpBasicAuthenticator(username + "/" + domain, password);


            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("senders", phoneSender);
            request.AddParameter("messages", messageSend.Message);
            request.AddParameter("recipients", messageSend.PhoneNummber.Nummber);

            return client.Execute(request);



        }


    }
}
