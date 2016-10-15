using nestedWorld.logs;
using UnityEngine;
using UnityEngine.UI;

namespace nestedWorld.ui.settings
{
    public class SettingsUi : MonoBehaviour
    {
        public Dropdown Resolution;
        public Slider Music;

        [HideInInspector] public GameObject MenuContainer;

        void Start()
        {
            VisualLogger.Instance.Init(GetComponent<Canvas>());

            MenuContainer = GameObject.FindGameObjectWithTag("MenuContainer");
            transform.SetParent(MenuContainer.transform);
        }

        void OnEnable()
        {
            VisualLogger.Instance.Init(GetComponent<Canvas>());
        }
    }
}
