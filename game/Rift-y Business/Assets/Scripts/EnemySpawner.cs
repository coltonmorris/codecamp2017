﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Transform enemyPrefab;

	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;

	private int waveNumber = 1;
		
	// Update is called once per frame
	void Update () {
		if (countdown <= 0f) {
			StartCoroutine(SpawnWave());

			countdown = timeBetweenWaves;
		}

		countdown -= Time.deltaTime;
	}

	IEnumerator SpawnWave () {
		waveNumber++;

		for (int i = 0; i < waveNumber; i++) {
			SpawnEnemy ();
			yield return new WaitForSeconds (0.5f);
		}
	}

	void SpawnEnemy () {



		Instantiate (enemyPrefab, spawnPoint.position, spawnPoint.rotation);
//////
		//Instantiate (enemyPrefab, spawnPoint.position, spawnPoint.rotation);
///// 956e994788777cd6ca43ec448f4b95316068df33
	}
}
