using UniRx;
using UnityEngine;

namespace AsyncTTT
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            new GetUser().Subscribe(_ =>
            {
                Debug.Log("here");
            });
        }
    }
}