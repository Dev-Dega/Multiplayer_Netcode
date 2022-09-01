using Unity.Netcode;
using UnityEngine;

public class NetworkVariableTest : NetworkBehaviour
{
    private NetworkVariable<float> ServerNetworkVariable = new NetworkVariable<float>();
    private NetworkVariable<float> ClientNetworkVariable = new NetworkVariable<float>();
    private float last_t_S = 0.0f;
    private float last_t_C = 0.0f;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            ServerNetworkVariable.Value = 0.0f;
            Debug.Log("Server's var initialized to: " + ServerNetworkVariable.Value);
        }
    }

    void Update()
    {
        var t_now_S = Time.time;
        if (IsServer)
        {
            ServerNetworkVariable.Value = ServerNetworkVariable.Value + 0.1f;
            if (t_now_S - last_t_S > 3f)
            {
                last_t_S = t_now_S;
                Debug.Log("Sever.log Server set its var to: " + ServerNetworkVariable.Value + ", has client var at: " +
                    ClientNetworkVariable.Value);
            }
        }

        var t_now_C = Time.time;
        if (IsClient)
        {
            ClientNetworkVariable.Value = ServerNetworkVariable.Value;
            if (t_now_C - last_t_C > 3f)
            {
                last_t_C = t_now_C;
                Debug.Log("Client.log Server set its var to: " + ServerNetworkVariable.Value + ", has client var at: " +
                    ClientNetworkVariable.Value);
            }
        }
    }
}