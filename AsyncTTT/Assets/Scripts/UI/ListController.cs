using Assets.Scripts.Abstraction;
using Assets.Scripts.Utility;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ListController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _listItemPrefab;

        public IObservable<Unit> AddItem(ConfigurationDictionary configuration)
        {
            var item = Instantiate(_listItemPrefab, transform);
            item.GetComponent<IConfigurable>().Configure(configuration);
            return item.GetComponent<Button>().OnClickAsObservable();
        }
    }
}
