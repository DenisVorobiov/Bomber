

namespace BomberNetwork
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using Unity.Netcode;
    using TMPro;

    public class NetworkGameManager : NetworkBehaviour
    {
        public GameObject startPanel;
        public GameObject hostPanel;
        public GameObject clientPanel;
        public GameObject lobbyPanel;
        public GameObject playerPrefab;

        public TMP_InputField nicknameInput;
        public TMP_Dropdown colorDropdown;

        private bool isHost;
        private bool isReady;

        private string playerNickname;
        private int playerColor;

        private void Start()
        {
            
            startPanel.SetActive(true);
            hostPanel.SetActive(false);
            clientPanel.SetActive(false);
            lobbyPanel.SetActive(false);
        }

        public void OnHostButtonClicked()
        {
           
            startPanel.SetActive(false);
            hostPanel.SetActive(true);
            isHost = true;
        }

        public void OnClientButtonClicked()
        {
            NetworkManager.Singleton.StartClient();
            startPanel.SetActive(false);
            clientPanel.SetActive(true);
            isHost = false;
        }

        public void ConfirmNickname()
        {
            string nickname = nicknameInput.text;
            if (nickname.Length >= 3 && nickname.Length <= 8)
            {
                if (isHost)
                {
                    
                }
                else
                {
                    
                }
                hostPanel.SetActive(false);
                lobbyPanel.SetActive(true);
            }
            else
            {
               
            }
        }

        public void SelectColor()
        {
            playerColor = colorDropdown.value; 
        }

        public void InitializePlayer()
        {
            var playerController = playerPrefab.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetPlayerDataServerRpc(playerNickname, playerColor);
            }
            else
            {
                Debug.LogError("PlayerPrefab не содержит компонент PlayerController.");
            }
        }

        private Color GetColorFromName(string colorName)
        {
            
            switch (colorName)
            {
                case "Red":
                    return Color.red;
                case "Blue":
                    return Color.blue;
                case "Green":
                    return Color.green;
                default:
                    return Color.white;
            }
        }

        public void ReadyButtonClicked()
        {
            isReady = true;
           
        }

        public void StartGame()
        {
          
            if (isHost)
            {
                SpawnPlayer();
            }
            
        }

        private void SpawnPlayer()
        {
            InitializePlayer(); 

            var spawnPosition = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
            var player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
    }
    }
