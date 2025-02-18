﻿using Assets.Scripts.Abstraction;
using Assets.Scripts.BLL.Operations;
using Assets.Scripts.Managers;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewModels
{
    public class MainViewModel : ViewModel
    {
        [SerializeField]
        private Button _friends, _games, _login, _register;
        [SerializeField]
        private TMP_InputField _loginInput, _passwordInput;

        private void Start()
        {
            if (string.IsNullOrEmpty(AuthorizationManager.Main.Login) == false)
            {
                _loginInput.text = AuthorizationManager.Main.Login;
                _passwordInput.text = AuthorizationManager.Main.Password;
            }
            else
            {
                _friends.gameObject.SetActive(false);
                _games.gameObject.SetActive(false);
            }

            _friends.OnClickAsObservable().Subscribe(_ =>
            {
                ViewManager.Main.ChangeView("Friends");
            }).AddTo(this);

            _games.OnClickAsObservable().Subscribe(_ =>
            {
                ViewManager.Main.ChangeView("Games");
            }).AddTo(this);

            _login.OnClickAsObservable()
                .Where(_ => ValidateCredentials())
                .SelectMany(_ =>
                {
                    _friends.gameObject.SetActive(false);
                    _games.gameObject.SetActive(false);
                    AuthorizationManager.Main.Login = _loginInput.text;
                    AuthorizationManager.Main.Password = _passwordInput.text;
                    return new CheckLogin().Execute();
                }).Subscribe(_ =>
                {
                    _friends.gameObject.SetActive(true);
                    _games.gameObject.SetActive(true);
                }, error =>
                {
                    PopupManager.Main.ShowPopup(error.Message);
                }).AddTo(this);

            _register.OnClickAsObservable()
                .Where(_ => ValidateCredentials())
                .SelectMany(_ =>
                {
                    _friends.gameObject.SetActive(false);
                    _games.gameObject.SetActive(false);
                    AuthorizationManager.Main.Login = _loginInput.text;
                    AuthorizationManager.Main.Password = _passwordInput.text;
                    return new Register().Execute();
                }).Subscribe(_ =>
                {
                    _friends.gameObject.SetActive(true);
                    _games.gameObject.SetActive(true);
                }, error =>
                {
                    PopupManager.Main.ShowPopup(error.Message);
                }).AddTo(this);
        }

        private bool ValidateCredentials()
        {
            string validateMessage;
            validateMessage = IsValidLogin(_loginInput.text);
            if (validateMessage != null)
            {
                PopupManager.Main.ShowPopup(validateMessage);
                return false;
            }
            validateMessage = IsValidPassword(_passwordInput.text);
            if (validateMessage != null)
            {
                PopupManager.Main.ShowPopup(validateMessage);
                return false;
            }
            return true;
        }

        private string IsValidLogin(string login)
        {
            if (login.Length < 6)
            {
                return "Za krótki login!";
            }
            foreach (char c in login)
            {
                if (char.IsLetterOrDigit(c) == false)
                {
                    return "Niedozwolone znaki w loginie!";
                }
            }
            return null;
        }

        private string IsValidPassword(string password)
        {
            if (password.Length < 6)
            {
                return "Za krótkie hasło!";
            }
            foreach (char c in password)
            {
                if (char.IsLetterOrDigit(c) == false)
                {
                    return "Niedozwolone znaki w haśle!";
                }
            }
            return null;
        }
    }
}
