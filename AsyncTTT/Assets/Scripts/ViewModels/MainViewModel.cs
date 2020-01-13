using Assets.Scripts.Abstraction;
using Assets.Scripts.Managers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewModels
{
    public class MainViewModel : ViewModel
    {
        [SerializeField]
        private Button _friends, _games;

        private void Start()
        {
            _friends.OnClickAsObservable().Subscribe(_ =>
            {
                ViewManager.Main.ChangeView("Friends");
            }).AddTo(this);

            _games.OnClickAsObservable().Subscribe(_ =>
            {
                ViewManager.Main.ChangeView("Games");
            }).AddTo(this);
        }
    }
}
