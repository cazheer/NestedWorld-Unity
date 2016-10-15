using System;

namespace nestedWorld.network.http.callback
{
    public class Callback
    {
        public Func<string, bool> OnSuccess;
        public Func<string, string, bool> OnFailure;
    }
}