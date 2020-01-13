using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Abstraction
{
    public abstract class ViewModel : MonoBehaviour
    {
        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ViewManager.Main.Back();
            }
        }
    }
}
