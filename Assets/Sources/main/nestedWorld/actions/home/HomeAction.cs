using nestedWorld.logs;
using nestedWorld.models;
using nestedWorld.network.http.callback;
using nestedWorld.network.http.errorHandler;
using nestedWorld.network.http.implementation;
using nestedWorld.network.http.models.user;
using nestedWorld.ui.home;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace nestedWorld.actions.home
{
    public class HomeAction : MonoBehaviour
    {
        private HomeUi _homeUi;
        private NestedWorldHttpApi _api;

        private void Start()
        {
            _homeUi = GetComponent<HomeUi>();

            var requester = GameObject.FindGameObjectWithTag("Requester");
            _api = requester.GetComponent<NestedWorldHttpApi>();

            UpdateUserInfo();
        }

        private void OnEnable()
        {
            UpdateUserInfo();
        }

        public void UpdateUserInfo()
        {
            _api.User(new Callback()
            {
                OnSuccess = UserOnSuccess,
                OnFailure = ErrorHandler.OnFailure
            }, DataContainer.Instance.Token);
            _api.UserMonsters(new Callback()
            {
                OnSuccess = MonsterOnSuccess,
                OnFailure = ErrorHandler.OnFailure
            }, DataContainer.Instance.Token);
            _api.UserFriends(new Callback()
            {
                OnSuccess = UserFriendsOnSuccess,
                OnFailure = ErrorHandler.OnFailure
            }, DataContainer.Instance.Token);
        }

        private bool UserOnSuccess(string text)
        {
            var response = JsonUtility.FromJson<UserResponse>(text);
            if (response == null) return false;
            DataContainer.Instance.Self = response.user;

            _homeUi.Nickname = DataContainer.Instance.Self.pseudo;
            _homeUi.Level = DataContainer.Instance.Self.level.ToString();

            return true;
        }

        private bool MonsterOnSuccess(string text)
        {
            var response = JsonUtility.FromJson<UserMonstersResponse>(text);
            if (response == null) return false;
            DataContainer.Instance.UserMonsters = response.monsters;

            foreach (var userMonster in DataContainer.Instance.UserMonsters)
            {
                var obj = Instantiate(_homeUi.TemplateMonster);
                obj.SetActive(true);

                obj.transform.SetParent(_homeUi.TemplateMonster.transform.parent);
                UpdateMonsterInfos(userMonster, obj);
            }

            return true;
        }

        private void UpdateMonsterInfos(UserMonster userMonster, GameObject obj)
        {
            var p = obj.transform.Find("FirstPanel");
            p.transform.Find("MonsterNick").GetComponent<Text>().text = userMonster.surname;
            p.transform.Find("MonsterName").GetComponent<Text>().text = userMonster.infos.name;
            p.transform.Find("Level").Find("Text").GetComponent<Text>().text = userMonster.experience.ToString();

            var a = obj.transform.Find("AdditionalPanel");
            a.transform.Find("Id").GetComponent<Text>().text = userMonster.infos.id.ToString();
            a.transform.Find("Hp").GetComponent<Text>().text = userMonster.infos.hp.ToString();
            a.transform.Find("Attack").GetComponent<Text>().text = userMonster.infos.attack.ToString();
            a.transform.Find("Defense").GetComponent<Text>().text = userMonster.infos.defense.ToString();
            a.transform.Find("Speed").GetComponent<Text>().text = userMonster.infos.speed.ToString();
            a.transform.Find("Type").GetComponent<Text>().text = userMonster.infos.type;
        }

        private bool UserFriendsOnSuccess(string text)
        {
            var response = JsonUtility.FromJson<UserFriendsResponse>(text);
            if (response == null) return false;
            DataContainer.Instance.Friends = response.friends;

            foreach (var friend in DataContainer.Instance.Friends)
            {
                var obj = Instantiate(_homeUi.TemplateFriend);
                obj.SetActive(true);

                obj.transform.SetParent(_homeUi.TemplateFriend.transform.parent);
                UpdateFriendInfos(friend, obj);
            }

            return true;
        }

        private void UpdateFriendInfos(Friend friend, GameObject obj)
        {
            var p = obj.transform.Find("FirstPanel");
            p.Find("FriendName").GetComponent<Text>().text = friend.user.pseudo;
            p.Find("Active").GetComponent<RawImage>().color = friend.user.is_active ? Color.green : Color.red;
        }

        public void AddFriend()
        {
            if (_homeUi.FriendName.Length == 0)
                VisualLogger.Instance.Log(Constants.MissingName);
            else
            {
                _api.AddUserFriend(new Callback()
                {
                    OnSuccess = AddFriendOnSuccess,
                    OnFailure = ErrorHandler.OnFailure
                }, DataContainer.Instance.Token, _homeUi.FriendName);
            }
        }

        private bool AddFriendOnSuccess(string text)
        {
            var response = JsonUtility.FromJson<UserFriendsResponse>(text);
            if (response == null) return false;
            if (DataContainer.Instance.Friends != null &&
                DataContainer.Instance.Friends.Count == response.friends.Count)
            {
                VisualLogger.Instance.Log(Constants.FriendMissing);
                return true;
            }

            DataContainer.Instance.Friends = response.friends;

            foreach (var friend in DataContainer.Instance.Friends)
            {
                var obj = Instantiate(_homeUi.TemplateFriend);
                obj.SetActive(true);

                obj.transform.SetParent(_homeUi.TemplateFriend.transform.parent);
                UpdateFriendInfos(friend, obj);
            }
            return true;
        }

        public void Settings()
        {
            SceneManager.LoadScene("SettingsScene");
            _homeUi.gameObject.SetActive(false);
        }
    }
}
