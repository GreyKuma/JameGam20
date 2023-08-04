using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
	private void Update()
	{
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.GetComponents<Destroyer>().Any())
		{
			Destroy(gameObject);
		}
	}
}
