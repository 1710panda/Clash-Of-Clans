using DevelopersHub.RealtimeNetworking.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ClashOfClans
{
    public enum RequestsID
    {
        AUTH = 1  // ��¼��֤
    }



    public class Player : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            RealtimeNetworking.OnLongReceived += ReceivedLong;
            //RealtimeNetworking.OnPacketReceived += PacketReceived; 

            ConnectedToServer();
        }



        private void ReceivedLong(int requestID, long value)
        {
            switch (requestID)
            {
                case (int)RequestsID.AUTH:
                    Debug.Log("User auth success database ID:" + value);
                    break;
                default:
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// ���ӽ��
        /// </summary>
        /// <param name="successful"></param>
        private void ConnectResult(bool successful)
        {
            if (successful)
            {
                Debug.Log("Successfully to connect the server.");
                // ��ȡ�û��豸��ʶ��
                RealtimeNetworking.OnDisconnectedFromServer += Disconnected;
                string decive = SystemInfo.deviceUniqueIdentifier;
                Sender.TCP_Send((int)RequestsID.AUTH, decive);
            }
            else
            {
                Debug.Log("Failed to connect the server.");
                // todo: connect fail and retry box
            }
            RealtimeNetworking.OnConnectingToServerResult -= ConnectResult;

        }

        /// <summary>
        /// �Ͽ�����
        /// </summary>
        private void Disconnected()
        {
            Debug.Log("Server is disconnected.");
            RealtimeNetworking.OnDisconnectedFromServer -= Disconnected;
            // todo : disConnected

        }


        /// <summary>
        /// ���ӷ�����
        /// </summary>
        public void ConnectedToServer()
        {

            RealtimeNetworking.OnConnectingToServerResult += ConnectResult;  // �������ӽ��
            RealtimeNetworking.Connect();
        }

        private void PacketReceived(Packet packet)
        {
            string str = packet.ReadString();
            Debug.Log("PacketReceived: " + str);

        }

    }
}