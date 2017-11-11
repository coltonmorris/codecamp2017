using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public static int PlayerDeathCount;
	public static int EnemyDeathCount;
	public int startPlayerDeathCount = 0;
	public int startEnemyDeathCount = 0;
	
	// Update is called once per frame
	void Start () {
		PlayerDeathCount = startPlayerDeathCount;
		EnemyDeathCount = startEnemyDeathCount;
	}
}
