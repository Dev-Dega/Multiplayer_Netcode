using Unity.Netcode;
using UnityEngine;

namespace Dega
{
    public class Player : NetworkBehaviour
    {
        public NetworkVariable<Vector2> Position = new NetworkVariable<Vector2>();

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                Move();
                
            }
        }

        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                var randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;
                Position.Value = randomPosition;
            }
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetRandomPositionOnPlane();
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        }

        void Update()
        {
            transform.position = Position.Value;
        }
    }
}