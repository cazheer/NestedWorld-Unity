using nestedWorld.logs;
using nestedWorld.network.http.models.reponse;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace nestedWorld.network.http.errorHandler
{
    public class ErrorHandler
    {
        private const string ErrorNoInternet = "400: Cannot connect to the server";
        private const string ErrorServer = "500: Server error";

        public static bool OnFailure(string text, string error)
        {
            string msg;
            if (error.StartsWith("400"))
                msg = ErrorNoInternet;
            else if (error.StartsWith("500"))
                msg = ErrorServer;
            else
            {
                msg = error;
                var response = JsonUtility.FromJson<ErrorResponse>(text);
                if (response != null)
                {
                    msg += ": " + response.message;
                }
            }

            VisualLogger.Instance.Log(msg);
            ConsoleLogger.Instance.LogError(msg);

            return true;
        }
    }
}