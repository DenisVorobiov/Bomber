using TMPro;
using UnityEngine;

namespace BomberNetwork
{
    using UnityEngine;
    using UnityEngine.UI;
    using Unity.Netcode;

    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private TMP_Text playerNameText;
        [SerializeField] private Renderer playerRenderer;

        private string playerName;
        private int playerColorIndex;

       
        [ServerRpc]
        public void SetPlayerDataServerRpc(string name, int colorIndex)
        {
            playerName = name;
            playerColorIndex = colorIndex;

           
            SetPlayerDataClientRpc(playerName, playerColorIndex);
        }

       
        [ClientRpc]
        private void SetPlayerDataClientRpc(string name, int colorIndex)
        {
            playerNameText.text = name;
            Color color = GetColorFromIndex(colorIndex);
            playerRenderer.material.color = color;
        }

        private Color GetColorFromIndex(int colorIndex)
        {
            
            Color[] colors = new Color[]
            {
                Color.red,
                Color.blue,
                Color.green
            };
            if (colorIndex >= 0 && colorIndex < colors.Length)
            {
                return colors[colorIndex];
            }
            else
            {
                return Color.white;
            }
        }
    }
}