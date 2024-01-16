using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace UI
{
    public class NetworkFunctionUI : MonoBehaviour
    {
        public void Host()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void Join()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
