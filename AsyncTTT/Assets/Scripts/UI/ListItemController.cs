using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ListItemController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _title, _subtitle;

        public string TitleText { get => _title.text; set => _title.text = value; }
        public string SubtitleText { get => _subtitle.text; set => _subtitle.text = value; }
        public Color SubtitleColor { get => _subtitle.color; set => _subtitle.color = value; }
    }
}
