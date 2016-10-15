using System;

namespace nestedWorld.network.http.models.request.auth
{
    [Serializable]
    public class LogInRequest
    {
        public string email;
        public string password;
        public string app_token;
        public string data;
    }
}
