using Assets.Scripts.Abstraction;
using Assets.Scripts.Utility;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class FriendListItemController : MonoBehaviour, IConfigurable
    {
        [SerializeField]
        private TextMeshProUGUI _title, _subtitle;

        public void Configure(ConfigurationDictionary configuration)
        {
            _title.text = configuration["title_text"] as string;
            _subtitle.text = configuration["subtitle_text"] as string;
            _subtitle.color = configuration["subtitle_color"] as Color? ?? Color.black;
        }
    }
}
