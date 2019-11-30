using UniRx;
using UnityEngine;

namespace AsyncTTT
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            new GetUsers().Subscribe(users =>
            {
                foreach (var user in users)
                {
                    Debug.Log(user.Name);
                }
            }, () =>
            {
                Debug.Log("oncomplete");
            });
        }
    }
}