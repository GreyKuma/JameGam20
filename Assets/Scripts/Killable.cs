using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public Action<GameObject> OnKilled;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponents<Killer>().Any())
        {
            OnKilled.Invoke(gameObject);
            Destroy(gameObject);
        }
    }
}
