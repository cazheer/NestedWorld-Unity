using System.Collections.Generic;
using nestedWorld.models;
using nestedWorld.network.http.callback;
using nestedWorld.network.http.errorHandler;
using nestedWorld.network.http.implementation;
using nestedWorld.ui.settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace nestedWorld.actions.settings
{
    public class SettingsAction : MonoBehaviour
    {
        private SettingsUi _settingsUi;
        private NestedWorldHttpApi _api;

        private float _currentSoundVolume = 1f;
        private int _currentResolution;

        void Start()
        {
            _settingsUi = GetComponent<SettingsUi>();

            var requester = GameObject.FindGameObjectWithTag("Requester");
            _api = requester.GetComponent<NestedWorldHttpApi>();

            // Resolution
            var listOption = new List<string>();
            var resolutions = Screen.resolutions;
            var currentRes = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
            var resPosition = 0;
            var i = 0;
            foreach (var res in resolutions)
            {
                var resString = res.width + "x" + res.height;
                listOption.Add(resString);
                if (resString == currentRes)
                    resPosition = i;
                i++;
            }
            _settingsUi.Resolution.value = resPosition;
            _settingsUi.Resolution.AddOptions(listOption);
        }

        public void Save()
        {
            if (_currentResolution != _settingsUi.Resolution.value)
            {
                var resolutions = Screen.resolutions;
                if (_settingsUi.Resolution.value < resolutions.Length)
                {
                    Screen.SetResolution(resolutions[_settingsUi.Resolution.value].width,
                        resolutions[_settingsUi.Resolution.value].height, true);
                }
                _currentResolution = _settingsUi.Resolution.value;
            }
            if (_currentSoundVolume != _settingsUi.Music.value)
            {
                // TODO : change sound
            }
            Return();
        }

        public void Cancel()
        {
            _settingsUi.Resolution.value = _currentResolution;
            _settingsUi.Music.value = _currentSoundVolume;
            Return();
        }

        private void Return()
        {
            var home = _settingsUi.MenuContainer.transform.FindChild("HomeMenu");
            home.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void LogOut()
        {
            _api.LogOut(new Callback()
            {
                OnSuccess = LogOutOnSuccess,
                OnFailure = ErrorHandler.OnFailure
            }, DataContainer.Instance.Token);
        }

        private bool LogOutOnSuccess(string text)
        {
            if (PlayerPrefs.HasKey(Constants.EmailCache))
            {
                PlayerPrefs.DeleteKey(Constants.EmailCache);
                PlayerPrefs.DeleteKey(Constants.PasswordCache);
                PlayerPrefs.Save();
            }

            Destroy(GameObject.FindGameObjectWithTag("Requester"));
            SceneManager.LoadScene("LoginScene");
            Destroy(GameObject.FindGameObjectWithTag("MenuContainer"));

            return true;
        }
    }
}
