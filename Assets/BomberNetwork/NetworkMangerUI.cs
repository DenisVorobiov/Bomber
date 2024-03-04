using Unity.Netcode;
using UnityEngine;

namespace BomberNetwork
{
    public class NetworkMangerUI : MonoBehaviour
    {
        public void OnStartServerClick()
        {
            NetworkManager.Singleton.StartServer();
        }
        public void OnStartHostClick()
        {
            NetworkManager.Singleton.StartHost();
        }
        public void OnStartClientClick()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}