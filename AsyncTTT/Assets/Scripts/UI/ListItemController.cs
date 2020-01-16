using Assets.Scripts.Abstraction;
using Assets.Scripts.Utility;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ListItemController : MonoBehaviour, IConfigurable
    {
        [SerializeField]
        private TextMeshProUGUI _title, _subtitle;

        public string TitleText { get => _title.text; set => _title.text = value; }
        public string SubtitleText { get => _subtitle.text; set => _subtitle.text = value; }
        public Color SubtitleColor { get => _subtitle.color; set => _subtitle.color = value; }

        public void Configure(ConfigurationDictionary configuration)
        {
            _title.text = configuration["title_text"] as string;
            _subtitle.text = configuration["subtitle_text"] as string;
            _subtitle.color = configuration["subtitle_color"] as Color? ?? Color.black;
        }
    }
}
