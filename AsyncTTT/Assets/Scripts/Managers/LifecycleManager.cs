using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class LifecycleManager : MonoBehaviour
    {
        private void Start()
        {
            ViewManager.Main.ChangeView("Main");
        }
    }
}