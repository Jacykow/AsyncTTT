using Assets.Scripts.Operations.Rest.Operations;
using UniRx;
using UnityEngine;

namespace AsyncTTT.Managers
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