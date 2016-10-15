using System.Collections;
using System.Collections.Generic;
using nestedWorld.logs;
using nestedWorld.models;
using nestedWorld.network.http.callback;
using UnityEngine;

namespace nestedWorld.network.http.implementation
{
    public abstract class HttpApi : MonoBehaviour
    {
        private static Dictionary<string, string> CreateHeaders(Token token)
        {
            var headers = new Dictionary<string, string>
            {
                {"Content-Type", "application/json"},
                {"Accept", "application/json"}
            };
            if (token == null) return headers;
            headers.Add("X-User-Email", token.email);
            headers.Add("Authorization", "Bearer " + token.token);
            return headers;
        }

        public void Get(string url, Callback callback, Token token)
        {
            var request = new WWW(url, null, CreateHeaders(token));
            StartCoroutine(WaitForRequest(request, callback));
        }

        public void Post(string url, Callback callback, Token token, string data)
        {
            var postData = System.Text.Encoding.UTF8.GetBytes(data);
            var request = new WWW(url, postData, CreateHeaders(token));
            StartCoroutine(WaitForRequest(request, callback));
        }

        private static IEnumerator WaitForRequest(WWW request, Callback callback)
        {
            yield return request;

            ConsoleLogger.Instance.Log(request.text);
            if (request.error == null)
            {
                if (!callback.OnSuccess(request.text))
                    ConsoleLogger.Instance.LogWarning("Unrecognised format");
            }
            else
                callback.OnFailure(request.text, request.error);
        }
    }
}
