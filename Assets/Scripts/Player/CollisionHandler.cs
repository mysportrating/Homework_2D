using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> BridgeReached;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IInteractable bridgeBehaviour))
        {
            BridgeReached?.Invoke(bridgeBehaviour);
        }
    }
}
