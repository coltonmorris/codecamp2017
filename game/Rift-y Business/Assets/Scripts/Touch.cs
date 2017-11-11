using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {

	public float grabRadius;
	public LayerMask grabMask;

	private GameObject grabbedObject;
	private bool grabbing;

	void StopObject () {
		grabbing = true;

		RaycastHit[] hits;
		hits = Physics.SphereCastAll (transform.position, grabRadius, transform.forward, 0f, grabMask);

		if (hits.Length > 0) {
			int closestHit = 0;

			for (int i = 0; i < hits.Length; i++) {
				if (hits [i].distance < hits [closestHit].distance) {
					closestHit = i;
				}
			}

			grabbedObject = hits [closestHit].transform.gameObject;
			grabbedObject.GetComponent<Rigidbody> ().isKinematic = true;
			grabbedObject.transform.position = transform.position;
			grabbedObject.transform.parent = transform;
		}

	}

	void StopObjectDone () {
		grabbing = false;
		Debug.Log ("stopped object done!");
	}

	// Update is called once per frame
	void Update () {
		if (!grabbing && (Input.GetAxis("RHandTrigger") == 1 || Input.GetAxis("LHandTrigger") == 1)) {
			StopObject ();
		}

		if (grabbing && (Input.GetAxis("RHandTrigger") < 1 || Input.GetAxis("LHandTrigger") < 1)) {
			StopObjectDone ();
		}
	}
}
