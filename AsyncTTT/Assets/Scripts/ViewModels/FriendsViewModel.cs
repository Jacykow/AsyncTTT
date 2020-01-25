using Assets.Scripts.Abstraction;
using Assets.Scripts.BLL.Enums;
using Assets.Scripts.BLL.Models;
using Assets.Scripts.BLL.Operations;
using Assets.Scripts.Managers;
using Assets.Scripts.UI;
using Assets.Scripts.Utility;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.ViewModels
{
    public class FriendsViewModel : ViewModel
    {
        [SerializeField]
        private ListController _friendList;

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
                                            PopupManager.Main.ShowPopup(exception.Message);
                                        });
                                    break;
                            }
                        }).AddTo(this);
                }
            }).AddTo(this);
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
