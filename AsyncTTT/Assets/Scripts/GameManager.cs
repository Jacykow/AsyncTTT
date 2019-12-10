using Assets.Scripts.DAL.Rest.Operations;
using UniRx;
using UnityEngine;

namespace AsyncTTT
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            new CheckCredentials("jacyk", "guczi").Execute().Subscribe(response => Debug.Log("Success: " + response.Success));
        }
    }
}