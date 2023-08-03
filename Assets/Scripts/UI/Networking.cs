using Unity.Netcode;
using UnityEngine;

namespace UI
{
    public class Networking : MonoBehaviour
    {
        public void Host()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void Client()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
