using Assets.Scripts.Utility;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    public class ViewManager
    {
        private static ViewManager _manager;

        public static ViewManager Main
        {
            get
            {
                if (_manager == null)
                {
                    _manager = new ViewManager();
                }
                return _manager;
            }
        }

        private readonly List<string> _viewSceneNames = new List<string>();

        public ConfigurationDictionary ViewParameters { get; } = new ConfigurationDictionary();

        public void ChangeView(string viewSceneName)
        {
            if (_viewSceneNames.Count >= 1)
            {
                SceneManager.UnloadSceneAsync(_viewSceneNames[_viewSceneNames.Count - 1]);
            }
            _viewSceneNames.Add(viewSceneName);
            SceneManager.LoadScene(viewSceneName, LoadSceneMode.Additive);
        }

        public void Back()
        {
            if (_viewSceneNames.Count >= 2)
            {
                string lastSceneName = _viewSceneNames[_viewSceneNames.Count - 2];
                _viewSceneNames.RemoveRange(_viewSceneNames.Count - 2, 2);
                ChangeView(lastSceneName);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
