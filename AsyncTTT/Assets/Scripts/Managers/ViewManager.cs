using System.Collections.Generic;
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

        public void ChangeView(string viewSceneName)
        {
            if (_viewSceneNames.Count >= 1)
            {
                SceneManager.UnloadSceneAsync(_viewSceneNames[_viewSceneNames.Count - 1]);
            }
            SceneManager.LoadScene(viewSceneName, LoadSceneMode.Additive);
        }
    }
}
