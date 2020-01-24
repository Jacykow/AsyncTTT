using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Abstraction
{
    public abstract class ViewModel : MonoBehaviour
    {
        protected void Awake()
        {
            if (SceneManager.sceneCount == 1)
            {
                SceneManager.LoadScene(0, LoadSceneMode.Additive);
            }
        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ViewManager.Main.Back();
            }
        }
    }
}
