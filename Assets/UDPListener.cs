using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System;

public class UDPListener : MonoBehaviour
{
    private const int Port = 1490;
    private IPEndPoint EndPoint = new(IPAddress.Any, Port);
    private float Angle = 0;
    public GameObject Cube;

    // Start is called before the first frame update
    void Start()
    {
        Task.Run(() => SetupClient());
    }

    void SetupClient()
    {
        using(var client = new UdpClient(Port))
        {
            Debug.Log($"Listening to UDP event on {Port}");
            while (true)
            {
                byte[] bytes = client.Receive(ref EndPoint);
                
                Angle = (float)BitConverter.ToDouble(bytes, 0);
                Debug.Log($"UDP: {bytes.Length} bytes received ({Angle}°).");
            }       
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cube.transform.rotation = Quaternion.Euler(new(0, Angle, 0));
    }
}
