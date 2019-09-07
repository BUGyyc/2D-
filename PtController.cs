using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtController : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {
        this.Invoke ("setDisable", 0.5f);
    }

    void setDisable () {
        GameObject.Destroy (gameObject);
    }
}