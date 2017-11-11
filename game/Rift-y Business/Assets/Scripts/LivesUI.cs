using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

	public Text deathText;

	// Update is called once per frame
	void Update () {
		deathText.text = "Deaths = " + PlayerStats.DeathCount.ToString ();
	}
}
