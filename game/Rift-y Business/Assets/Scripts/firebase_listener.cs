using UnityEngine;

using SimpleFirebaseUnity;
using SimpleFirebaseUnity.MiniJSON;

using System.Collections.Generic;
using System.Collections;
using System;

public class FirebaseEnemy
{
    public float x, z;
    public string username;

    public FirebaseEnemy(Int64 X, Int64 Z, string Username)
    {
        x = (float)X;
        z = (float)Z;
        username = Username;
    }

    public override string ToString()
    {
        return "Position: (" + x.ToString() + ", " + z.ToString() + "). Username: " + username;
    }
}
public class firebase_listener : MonoBehaviour
{
    public Transform enemyPrefab;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Tests());
    }

    IEnumerator Tests()
    {
        // Inits Firebase using Firebase Secret Key as Auth
        // The current provided implementation not yet including Auth Token Generation
        // If you're using this sample Firebase End, 
        // there's a possibility that your request conflicts with other simple-firebase-c# user's request
        Firebase firebase = Firebase.CreateNew("testing-a2aa2.firebaseio.com");

        Firebase badguys = firebase.Child("badguys");

        FirebaseQueue firebaseQueue = new FirebaseQueue();

        // Make observer on badguys.
        FirebaseObserver badguyObserver = new FirebaseObserver(badguys, .5f);
        // This code is executed whenever an update to firebase is done
        badguyObserver.OnChange += (Firebase sender, DataSnapshot snapshot) =>
        {
            Debug.Log("[BADGUY] Bady Guy was updated");

            Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>>();
            List<string> keys = snapshot.Keys;

            if (keys != null)
            {
                foreach (string key in keys)
                {
                    // Spawn this enemy, then delete it from firebase
                    Dictionary<string, object> newEnemy = (Dictionary<string, object>)dict[key];
                    FirebaseEnemy tmp = new FirebaseEnemy((Int64)newEnemy["x"], (Int64)newEnemy["z"], (string)newEnemy["user"]);

                    Vector3 spawn = new Vector3((Int64)newEnemy["x"], 1.0f, (Int64)newEnemy["z"]);
                    Quaternion rotate = new Quaternion();

                    Instantiate(enemyPrefab, spawn, rotate);

                    Debug.Log("[BADGUY KEYS] " + tmp.ToString());
                    Debug.Log("***************");

                    // now deleting
                    firebaseQueue.AddQueueDelete(firebase.Child("badguys/" + key));
                }
            }
            else
            {
                Debug.Log("************ THE BADGUY OBJECT IS EMPTY **********");
            }

        };
        badguyObserver.Start();

        firebaseQueue.AddQueuePush(badguys, "{\"y\": 44, \"x\": 44, \"z\": 44, \"user\": \"stevvvv\"}}", true);
        firebaseQueue.AddQueuePush(badguys, "{\"y\": 21, \"x\": 22, \"z\": 23, \"user\": \"colton\"}}", true);

        yield return null;
    }
}