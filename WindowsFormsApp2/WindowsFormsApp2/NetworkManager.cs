using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class NetworkManager
    {

        public static NetworkManager insance;
        private void Awake()
        {

            insance = this;
        }
        public void Start()
        {
            
            ClientHandleData.InitializePackets();
            ClientTCP.InitializingNetworking();

        }




        private void OnApplicationQuit()
        {
            ClientTCP.Disconnect();
        }

    }

}
