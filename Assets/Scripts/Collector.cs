using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public Action<GameObject, GameObject> OnCollected;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponents<Collectable>().Any())
        {
            OnCollected.Invoke(collision.otherCollider.gameObject, collision.collider.gameObject);

        }
    }
}
