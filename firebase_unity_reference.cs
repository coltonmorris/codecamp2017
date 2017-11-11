using UnityEngine;

using SimpleFirebaseUnity;
using SimpleFirebaseUnity.MiniJSON;

using System.Collections.Generic;
using System.Collections;
using System;

public class Enemy
{
    public float x, z;
    public string username;
    
    public Enemy(Int64 X, Int64 Z, string Username)
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

public class SampleScript : MonoBehaviour
{

    static int debug_idx = 0;

    [SerializeField]
    TextMesh textMesh;


    // Use this for initialization
    void Start()
    {
        textMesh.text = "";
		StartCoroutine (Tests ());
    }

    void DoDebug(string str)
    {
        Debug.Log(str);
        if (textMesh != null)
        {
            textMesh.text += (++debug_idx + ". " + str) + "\n";
        }
    }

	IEnumerator Tests()
	{
		// Inits Firebase using Firebase Secret Key as Auth
		// The current provided implementation not yet including Auth Token Generation
		// If you're using this sample Firebase End, 
		// there's a possibility that your request conflicts with other simple-firebase-c# user's request
		Firebase firebase = Firebase.CreateNew ("testing-a2aa2.firebaseio.com");

        Firebase badguys = firebase.Child("badguys");

        FirebaseQueue firebaseQueue = new FirebaseQueue();

        // Make observer on badguys.
        FirebaseObserver badguyObserver = new FirebaseObserver(badguys, .5f);
        // This code is executed whenever an update to firebase is done
        badguyObserver.OnChange += (Firebase sender, DataSnapshot snapshot) =>
        {
            DoDebug("[BADGUY] Bady Guy was updated");

            Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>>();
            List<string> keys = snapshot.Keys;

            if (keys != null)
            {
                foreach (string key in keys)
                {
                    // Spawn this enemy, then delete it from firebase
                    Dictionary<string, object> newEnemy = (Dictionary<string, object>)dict[key];
                    Enemy tmp = new Enemy((Int64)newEnemy["x"], (Int64)newEnemy["z"], (string)newEnemy["user"]);

                    DoDebug("[BADGUY KEYS] " + tmp.ToString());
                    DoDebug("***************");

                    // now deleting
                    firebaseQueue.AddQueueDelete (firebase.Child ("badguys/" + key)); 
                }
            }
            else
            {
                DoDebug("************ THE BADGUY OBJECT IS EMPTY **********");
            }
            
        };
        badguyObserver.Start();

        firebaseQueue.AddQueuePush(badguys, "{\"y\": 10, \"x\": 11, \"z\": 12, \"user\": \"codecamp2017\"}}", true);
        firebaseQueue.AddQueuePush(badguys, "{\"y\": 21, \"x\": 22, \"z\": 23, \"user\": \"colton\"}}", true);

		yield return null;
	}
}
