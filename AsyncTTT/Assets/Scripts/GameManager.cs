using Assets.Scripts.DAL.Rest.Operations;
using UniRx;
using UnityEngine;

namespace AsyncTTT
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            new CheckCredentials("testuser", "testp4$$W0rD").Execute()
                .Subscribe(response => Debug.Log("Success: " + response.Success + " Creds: " + response.Message))
                .AddTo(this);
        }
    }
}