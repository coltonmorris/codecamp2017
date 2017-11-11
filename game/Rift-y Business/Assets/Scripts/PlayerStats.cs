using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public static int DeathCount;
	public int startDeathCount = 0;
	
	// Update is called once per frame
	void Start () {
		DeathCount = startDeathCount;
	}
}
