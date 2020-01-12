using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private readonly GameObject _scrollView;
        [SerializeField]
        private readonly ListController _listController;

        public static UIManager Main { get; private set; }

        private void Awake()
        {
            Main = this;
        }
    }
}
