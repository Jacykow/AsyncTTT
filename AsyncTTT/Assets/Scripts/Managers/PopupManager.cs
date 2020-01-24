using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class PopupManager : MonoBehaviour
    {
        private static PopupManager _manager;

        public static PopupManager Main
        {
            get
            {
                if (_manager == null)
                {
                    Debug.LogError("No popup manager instance detected!");
                }
                return _manager;
            }
        }

        [SerializeField]
        private Button _okButton;
        [SerializeField]
        private TextMeshProUGUI _popupText;
        [SerializeField]
        private GameObject _popupContainer;

        private void Awake()
        {
            _manager = this;
            _popupContainer.SetActive(false);
            _okButton.OnClickAsObservable().Subscribe(_ =>
            {
                _popupContainer.SetActive(false);
            }).AddTo(this);
        }

        public void ShowPopup(string message)
        {
            _popupContainer.SetActive(true);
            _popupText.text = message;
        }
    }
}
