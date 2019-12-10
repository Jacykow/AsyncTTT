using UniRx;
using UnityEngine;

namespace AsyncTTT
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            var u = new GetUsers();

            u.Observe().Subscribe(users =>
            {
                foreach (var user in users)
                {
                    Debug.Log(user.Name);
                }
            });
            u.Observe().Subscribe(users =>
            {
                foreach (var user in users)
                {
                    Debug.Log(user.Name + "2");
                }
            });
            u.Execute();
        }
    }
}