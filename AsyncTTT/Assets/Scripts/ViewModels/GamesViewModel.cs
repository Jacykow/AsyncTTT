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
    public class GamesViewModel : ViewModel
    {
        [SerializeField]
        private ListController _gameList;

        private void Start()
        {
            new GetGames().Execute().Subscribe(games =>
            {
                games.Sort((a, b) => -(a.Order - b.Order));
                foreach (var game in games)
                {
                    _gameList.AddItem(GetConfiguration(game)).Subscribe(_ =>
                    {
                        switch (game.State)
                        {
                            case GameState.Invited:
                                new AcceptGameInvitation(game).Execute()
                                    .Subscribe(__ =>
                                    {
                                        ViewManager.Main.Back();
                                    }, exception =>
                                    {
                                        PopupManager.Main.ShowPopup(exception.Message);
                                    });
                                break;
                            case GameState.YourTurn:
                            case GameState.TheirTurn:
                                ViewManager.Main.ViewParameters["selected_game"] = game;
                                ViewManager.Main.ChangeView("Board");
                                break;
                        }
                    }).AddTo(this);
                }
            }).AddTo(this);
        }

        private ConfigurationDictionary GetConfiguration(Game game)
        {
            var configuration = new ConfigurationDictionary
            {
                ["title_text"] = game.Name
            };
            switch (game.State)
            {
                case GameState.Victory:
                    configuration["subtitle_text"] = "Zwycięstwo";
                    configuration["subtitle_color"] = Color.green;
                    break;
                case GameState.Draw:
                    configuration["subtitle_text"] = "Remis";
                    configuration["subtitle_color"] = Color.gray;
                    break;
                case GameState.Loss:
                    configuration["subtitle_text"] = "Przegrana";
                    configuration["subtitle_color"] = Color.red;
                    break;
                case GameState.YourTurn:
                    configuration["subtitle_text"] = "Twój ruch!";
                    configuration["subtitle_color"] = Color.yellow;
                    break;
                case GameState.TheirTurn:
                    configuration["subtitle_text"] = "Oczekiwanie na przeciwnika...";
                    configuration["subtitle_color"] = Color.gray;
                    break;
                case GameState.Invited:
                    configuration["subtitle_text"] = "Zaakceptuj zaproszenie";
                    configuration["subtitle_color"] = Color.blue;
                    break;
                default:
                    configuration["subtitle_text"] = "Błąd";
                    configuration["subtitle_color"] = Color.black;
                    break;
            }
            return configuration;
        }
    }
}
