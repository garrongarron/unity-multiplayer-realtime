//Networking.cs

//related link 
//https://www.youtube.com/watch?v=xx5Xyxrq5Bc
//https://www.youtube.com/watch?v=WwI_tDqKS8Y
//https://forum.unity.com/threads/writing-and-reading-a-socket-with-tcp.282071/

using System.Net.Sockets;   //library to use Sockets
using UnityEngine;
using System;               //to use Action (callbacks)
using System.Text;          //to decode bytes from server

public class Networking : MonoBehaviour
{
    TcpClient client = new TcpClient();
    NetworkStream stream;   //to read and write

    const string IP = "192.168.1.40";           //server ip (To be changed)
    const int PORT = 3000;                      //port (To be changed)
    const double memory = 5e+6;                 //means 5mbs en bytes
    const int timeTryingConnect = 5000;         //time to connect
    public byte[] data = new byte[(int)memory]; //where we store data from server

    private void Start()
    {
        conectar((bool res) =>
        {
            if (res == true)
            {
                stream = client.GetStream();//getting stream instance
                Debug.Log("ok");
            }
            else
            {
                Debug.Log("Connection Fail");
            }
        });
    }

    private void conectar(Action<bool> callback)
    {
        bool result = client.ConnectAsync(IP, PORT).Wait(timeTryingConnect);
        callback(result);
    }

    private void Update()
    {
        if (stream.DataAvailable)
        {
            receiveMsg();
        }
    }

    void receiveMsg()
    {
        int size = stream.Read(data, 0, data.Length);
        string message = Encoding.UTF8.GetString(data, 0, size);
        Debug.Log(message);
    }

    void sendMsg(string msg)
    {
        Byte[] sendBytes = Encoding.UTF8.GetBytes(msg);
        stream.Write(sendBytes, 0, sendBytes.Length);
    }

    private void OnApplicationQuit()
    {
        sendMsg("Closing (Message from CLIENT)");
        client.Close();
    }

}
