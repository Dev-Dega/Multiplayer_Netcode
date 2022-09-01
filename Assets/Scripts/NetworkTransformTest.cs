using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkTransformTest : NetworkBehaviour
{
    void Update()
    {
        if (IsServer)
        {
            float theta = Time.frameCount / 100.0f;
            transform.position = new Vector2((float)Math.Cos(theta-0.5), (float)Math.Sin(theta));
        }
    }
}