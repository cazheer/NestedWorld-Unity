using System;

namespace nestedWorld.network.http.models.request.auth
{
    [Serializable]
    public class RegisterRequest
    {
        public string pseudo;
        public string email;
        public string password;
    }
}
