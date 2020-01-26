using Assets.Scripts.Abstraction;
using Assets.Scripts.BLL.Enums;
using Assets.Scripts.BLL.Models;
using Assets.Scripts.BLL.Operations;
using Assets.Scripts.Managers;
using Assets.Scripts.UI;
using Assets.Scripts.Utility;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewModels
{
    public class FriendsViewModel : ViewModel
    {
        [SerializeField]
        private ListController _friendList;
        [SerializeField]
        private Button _requestFriendButton;
        [SerializeField]
        private TMP_InputField _friendNameInput;

        private void Start()
        {
            new GetFriends().Execute().Subscribe(friends =>
            {
                friends.Sort((a, b) => -(a.Order - b.Order));
                foreach (var friend in friends)
                {
                    _friendList.AddItem(GetConfiguration(friend))
                        .Subscribe(_ =>
                        {
                            switch (friend.State)
                            {
                                case FriendState.Accepted:
                                    new CreateGame(friend).Execute()
                                        .Subscribe(__ =>
                                        {
                                            ViewManager.Main.Back();
                                        }, exception =>
                                        {
                                            ViewManager.Main.Back();
                                            PopupManager.Main.ShowPopup(exception.Message);
                                        });
                                    break;
                                case FriendState.Invitation:
                                    new AcceptFriendInvitation(friend).Execute()
                                        .Subscribe(__ =>
                                        {
                                            ViewManager.Main.Back();
                                        }, exception =>
                                        {
                                            ViewManager.Main.Back();
                                            PopupManager.Main.ShowPopup(exception.Message);
                                        });
                                    break;
                            }
                        }).AddTo(this);
                }
            }).AddTo(this);

            _requestFriendButton.OnClickAsObservable().SelectMany(_ =>
            {
                return new InviteFriend(_friendNameInput.text).Execute();
            }).Subscribe(_ => { },
                    exception => PopupManager.Main.ShowPopup(exception.Message))
                .AddTo(this);
        }

        private ConfigurationDictionary GetConfiguration(Friend friend)
        {
            var configuration = new ConfigurationDictionary
            {
                ["title_text"] = friend.Name
            };
            switch (friend.State)
            {
                case FriendState.Accepted:
                    configuration["subtitle_text"] = "Invite to a game";
                    configuration["subtitle_color"] = Color.green;
                    break;
                case FriendState.Invitation:
                    configuration["subtitle_text"] = "Accept invitation";
                    configuration["subtitle_color"] = Color.yellow;
                    break;
                default:
                    configuration["subtitle_text"] = "Error";
                    configuration["subtitle_color"] = Color.black;
                    break;
            }
            return configuration;
        }
    }
}
