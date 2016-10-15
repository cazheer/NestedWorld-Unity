using System.Collections;
using nestedWorld.logs;
using nestedWorld.models;
using nestedWorld.network.http.callback;
using nestedWorld.network.http.errorHandler;
using nestedWorld.network.http.implementation;
using nestedWorld.network.http.models.reponse.auth;
using nestedWorld.ui.login;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace nestedWorld.actions.login
{
    public class LoginAction : MonoBehaviour
    {
        private LoginUi _loginUi;
        private NestedWorldHttpApi _api;

        private bool _canRetry = true;

        private void Start()
        {
            _loginUi = GetComponent<LoginUi>();

            var requester = GameObject.FindGameObjectWithTag("Requester");
            _api = requester.GetComponent<NestedWorldHttpApi>();

            if (!PlayerPrefs.HasKey(Constants.EmailCache)) return;
            _loginUi.Email = PlayerPrefs.GetString(Constants.EmailCache);
            _loginUi.Password = PlayerPrefs.GetString(Constants.PasswordCache);
            StartCoroutine(WaitForConnexion());
        }

        private IEnumerator WaitForConnexion()
        {
            yield return ConsoleLogger.Instance;
            Connexion();
        }

        public void Connexion()
        {
            if (!_canRetry) return;
            if (_loginUi.Email.Length == 0)
                VisualLogger.Instance.Log(Constants.MissingEmail);
            else if (_loginUi.Password.Length == 0)
                VisualLogger.Instance.Log(Constants.MissingPassword);
            else
            {
                _canRetry = false;
                _api.LogIn(new Callback()
                {
                    OnSuccess = ConnexionOnSuccess,
                    OnFailure = ErrorHandler.OnFailure
                }, _loginUi.Email, _loginUi.Password);
            }
        }

        public void PasswordForgot()
        {
            if (_loginUi.Email.Length == 0)
                VisualLogger.Instance.Log(Constants.MissingEmail);
            else
            {
                _api.ResetPassword(new Callback()
                {
                    OnSuccess = PasswordForgotOnSuccess,
                    OnFailure = ErrorHandler.OnFailure
                }, _loginUi.Email);
            }
        }

        private bool ConnexionOnSuccess(string text)
        {
            if (_loginUi.Save)
            {
                PlayerPrefs.SetString(Constants.EmailCache, _loginUi.Email);
                PlayerPrefs.SetString(Constants.PasswordCache, _loginUi.Password);
                PlayerPrefs.Save();
            }

            var response = JsonUtility.FromJson<LogInResponse>(text);
            if (response.token == null)
            {
                VisualLogger.Instance.Log(Constants.TokenUnavailable);
                return false;
            }

            DataContainer.Instance.Token = new Token()
            {
                email = _loginUi.Email,
                token = response.token
            };

            // TODO : Initialize socket

            SceneManager.LoadScene("HomeScene");
            return true;
        }

        private bool PasswordForgotOnSuccess(string text)
        {
            VisualLogger.Instance.Log(Constants.ResetPassword);
            return true;
        }
    }
}
