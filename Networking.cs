//Networking.cs
using System.Net.Sockets;//Libreria que nos permite usar Sockets
using UnityEngine;
using System;//Lo necesitamos para usar las interfaces Action
using System.Text;//Lo necesitamos para decodificar los bytes provenientes del servidor


public class Networking : MonoBehaviour
{
    TcpClient client = new TcpClient();//Instancia de nuestro client TCP
    NetworkStream stream;//Lo usamos para leer y escribir en el servidor

    const string IP = "192.168.1.40";//Direccion IP del servidor(al principio sera la ip de tu pc)
    const int PORT = 3000;//Puerto en el cual esta running el servidor
    const double memory = 5e+6;//Significa 5mbs en bytes
    const int timeTryingConnect = 5000;//Tiempo limite de conexion en milisegundos
    public byte[] data = new byte[(int)memory];//Donde almacenamos lo que viene del servidor
    public bool running = false;//Para saber si el client esta running
    private void Start()
    {
        //Intentamos conectarnos al servidor
        conectar((bool res) =>
        {
            if (res == true)
            {
                Debug.Log("OK");
                stream = client.GetStream();//Obtenemos la instancia del stream de la conexion
                running = true;
            }
            else
            {
                Debug.Log("NO SE PUDO CONECTAR");
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

        if (running == true)
        {
            if (stream.DataAvailable)//Asi sabemos si el servidor ha enviado algo
            {
                int n = stream.Read(data, 0, data.Length);
                string message = Encoding.UTF8.GetString(data, 0, n);
                Debug.Log(message);
            }
        }
    }
    
    private void OnApplicationQuit()
    {
        client.Close();
    }


}
