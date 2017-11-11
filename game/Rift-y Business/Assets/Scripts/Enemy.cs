using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float speed = 10f;

	private Transform target;
	private int wavepointIndex = 0;

	void Start () {
		target = Players.points[wavepointIndex];
	}

	void Update () {
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

		if (OVRTouchpad.Equals (target, transform)) {
			Debug.Log ("cool dude!");
		}

		// if enemy gets to player
		if (Vector3.Distance (transform.position, target.position) <= 0.4f) {
			// TODO: this is where the player gets hit by enemy

			PlayerStats.DeathCount++;

			Destroy(gameObject);
			return;
		}
	}
}
