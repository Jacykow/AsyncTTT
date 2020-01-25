﻿using Assets.Scripts.Abstraction;
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
                                        ViewManager.Main.ChangeView("Games");
                                    }, exception =>
                                    {
                                        PopupManager.Main.ShowPopup(exception.Message);
                                    });
                                break;
                            case GameState.YourTurn:
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
                    configuration["subtitle_text"] = "Victory";
                    configuration["subtitle_color"] = Color.green;
                    break;
                case GameState.Loss:
                    configuration["subtitle_text"] = "Loss";
                    configuration["subtitle_color"] = Color.red;
                    break;
                case GameState.YourTurn:
                    configuration["subtitle_text"] = "Your Turn!";
                    configuration["subtitle_color"] = Color.yellow;
                    break;
                case GameState.TheirTurn:
                    configuration["subtitle_text"] = "Waiting for opponent...";
                    configuration["subtitle_color"] = Color.gray;
                    break;
                case GameState.Invited:
                    configuration["subtitle_text"] = "Accept invitation";
                    configuration["subtitle_color"] = Color.blue;
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
