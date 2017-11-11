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

		if (transform.gameObject.GetComponent<Rigidbody> ().isKinematic) {
			PlayerStats.EnemyDeathCount++;
			Destroy (gameObject);
			return;
		}

		// if enemy gets to player
		if (Vector3.Distance (transform.position, target.position) <= 0.4f) {
			PlayerStats.PlayerDeathCount++;
			Destroy(gameObject);
			return;
		}
	}
}
