using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 10f;

	private Transform target;
	private int wavepointIndex = 0;

	void Start () {
		target = Players.points[0];
	}

	void Update () {
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

		// if enemy gets to player
		if (Vector3.Distance (transform.position, target.position) <= 0.4f) {
			// TODO: this is where the player gets hit by enemy


			Destroy(gameObject);
			return;
		}
	}
}
