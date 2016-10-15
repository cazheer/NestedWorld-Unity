using nestedWorld.logs;
using nestedWorld.network.http.implementation;
using UnityEngine;
using UnityEngine.UI;

namespace nestedWorld.ui.login
{
    public class LoginUi : MonoBehaviour
    {
        [SerializeField] private InputField _emailField;
        [SerializeField] private InputField _passwordField;
        [SerializeField] private Toggle _saveToggle;

        public string Email
        {
            get { return _emailField.text; }
            set { _emailField.text = value; }
        }

        public string Password
        {
            get { return _passwordField.text; }
            set { _passwordField.text = value; }
        }

        public bool Save
        {
            get { return _saveToggle.isOn; }
        }

        void Start()
        {
            VisualLogger.Instance.Init(GetComponent<Canvas>());
        }

        private void OnEnable()
        {
            VisualLogger.Instance.Init(GetComponent<Canvas>());
        }
    }
}
