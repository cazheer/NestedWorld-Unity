using nestedWorld.logs;
using UnityEngine;
using UnityEngine.UI;

namespace nestedWorld.ui.home
{
    public class HomeUi : MonoBehaviour
    {
        [SerializeField] private Text _nickname;
        [SerializeField] private Text _level;

        [SerializeField] private InputField _friendName;

        public GameObject TemplateMonster;
        public GameObject TemplateFriend;

        public RawImage UserImage;

        public string Nickname
        {
            get { return _nickname.text; }
            set { _nickname.text = value; }
        }

        public string Level
        {
            get { return _level.text; }
            set { _level.text = value; }
        }

        public string FriendName
        {
            get { return _friendName.text; }
            set { _friendName.text = value; }
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
