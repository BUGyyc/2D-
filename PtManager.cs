using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtManager : MonoBehaviour {

    public GameObject ptObject;
    ParticleSystem particleSystem;
    // Queue<GameObject> ptQueue = null;
    // Start is called before the first frame update
    void Start () {
        particleSystem = ptObject.GetComponent<ParticleSystem> ();
        // ptQueue = new Queue<GameObject
    }

    public void OnCreatePt (Transform tf) {
        GameObject gObj = Instantiate (ptObject) as GameObject;
        gObj.transform.position = tf.position;
        // ptQueue.

    }
}