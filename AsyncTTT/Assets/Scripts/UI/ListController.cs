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

        public IObservable<Unit> AddItem(string title, string subtitle, Color subtitleColor)
        {
            var itemController = Instantiate(_listItemPrefab, transform).GetComponent<ListItemController>();
            itemController.TitleText = title;
            itemController.SubtitleText = subtitle;
            itemController.SubtitleColor = subtitleColor;
            return itemController.GetComponent<Button>().OnClickAsObservable();
        }
    }
}
