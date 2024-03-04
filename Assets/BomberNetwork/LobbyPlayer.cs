
namespace BomberNetwork
{
    using UnityEngine;
using TMPro;
using Unity.Netcode;

public class LobbyPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Dropdown teamDropdown;
    [SerializeField] private NetworkManager networkManager;
   

    private NetworkVariable<string> playerName = new NetworkVariable<string>("Player", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<int> selectedTeam = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private void Start()
    {
        if (IsOwner)
        {
            nameInputField.onValueChanged.AddListener(SetPlayerName);
            teamDropdown.onValueChanged.AddListener(SetTeam);
        }
    }

    private void SetPlayerName(string name)
    {
        playerName.Value = name;
    }

    private void SetTeam(int teamIndex)
    {
        selectedTeam.Value = teamIndex;
    }

    public void StartGame()
    {
        if (IsEveryoneReady() && networkManager != null)
        {
            // Отримати префаб гравця з NetworkManager
            GameObject playerPrefab = networkManager.NetworkConfig.PlayerPrefab;

            if (playerPrefab != null)
            {
                // Викликати метод створення гравця на сервері
               // SubmitPlayerDataServerRpc(playerName.Value, selectedTeam.Value, playerPrefab);
            }
            else
            {
                Debug.LogError("PlayerPrefab not found in the NetworkManager.");
            }
        }
    }

   /* [ServerRpc]
    private void SubmitPlayerDataServerRpc(string playerName, int teamIndex, GameObject playerPrefab)
    {
        // Створити гравця на стороні сервера
        GameObject newPlayer = Instantiate(playerPrefab);

        // Отримати компонент гравця (PlayerController)
        PlayerController playerScript = newPlayer.GetComponent<PlayerController>();

        // Ініціалізація гравця з отриманими даними
        playerScript.Initialize(playerName, teamIndex);

        // Позначити гравця як з'єднаного за допомогою Unity.Netcode
        NetworkObject newPlayerNetworkObject = newPlayer.GetComponent<NetworkObject>();
        if (newPlayerNetworkObject != null)
        {
            newPlayerNetworkObject.Spawn();
        }
        else
        {
            Debug.LogError("NetworkObject component not found on the instantiated player prefab.");
        }
    }
*/
    private bool IsEveryoneReady()
    {
        var allPlayers = NetworkManager.Singleton.ConnectedClientsList;
        if (allPlayers.Count >= 2)
        {
            int teamCount = 0;
            foreach (var player in allPlayers)
            {
                var lobbyPlayer = player.PlayerObject.GetComponent<LobbyPlayer>();
                if (lobbyPlayer != null && lobbyPlayer.selectedTeam.Value != 0)
                {
                    teamCount++;
                }
            }

            return teamCount >= 2;
        }

        return false;
    }
}
}