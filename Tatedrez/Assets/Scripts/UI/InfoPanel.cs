using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InfoPanel : MonoBehaviour
    {
        private const string PlayerChangeText = "Current player: {0}";
        private const string PlayerVictoryText = "{0} won!";
        [SerializeField] private TMP_Text infoText;

        public async Task SetPlayerVictory(string playerColor)
        {
            infoText.SetText(string.Format(PlayerVictoryText, playerColor));
            await Enable();
        }
        
        public async Task SetPlayerInfo(string playerColor)
        {
            infoText.SetText(string.Format(PlayerChangeText, playerColor));
            await Enable();
        }
        
        private async Task Enable()
        {
            gameObject.SetActive(true);
            await Task.Delay(2000);
            gameObject.SetActive(false);
        }
    }
}
