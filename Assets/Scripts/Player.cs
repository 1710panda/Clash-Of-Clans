using DevelopersHub.RealtimeNetworking.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ClashOfClans
{
    public enum RequestsID
    {
        AUTH = 1  // 登录认证
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
        /// 连接结果
        /// </summary>
        /// <param name="successful"></param>
        private void ConnectResult(bool successful)
        {
            if (successful)
            {
                Debug.Log("Successfully to connect the server.");
                // 获取用户设备标识符
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
        /// 断开连接
        /// </summary>
        private void Disconnected()
        {
            Debug.Log("Server is disconnected.");
            RealtimeNetworking.OnDisconnectedFromServer -= Disconnected;
            // todo : disConnected

        }


        /// <summary>
        /// 连接服务器
        /// </summary>
        public void ConnectedToServer()
        {

            RealtimeNetworking.OnConnectingToServerResult += ConnectResult;  // 订阅连接结果
            RealtimeNetworking.Connect();
        }

        private void PacketReceived(Packet packet)
        {
            string str = packet.ReadString();
            Debug.Log("PacketReceived: " + str);

        }

    }
}