using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour {

	public static Transform[] enemyTarget;

	void Start () {
		enemyTarget = new Transform[transform.childCount];

		for (int i = 0; i < enemyTarget.Length; i++) {
			enemyTarget [i] = transform.GetChild (i);
		}
	}

}
