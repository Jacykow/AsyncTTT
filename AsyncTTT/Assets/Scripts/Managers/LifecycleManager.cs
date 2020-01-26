using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    public class LifecycleManager : MonoBehaviour
    {
        private void Start()
        {
            if (SceneManager.sceneCount < 2)
            {
                ViewManager.Main.ChangeView("Main");
            }
        }
    }
}