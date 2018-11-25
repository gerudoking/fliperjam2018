using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("LateDestroy", 1.01f);
	}
	
	void LateDestroy()
    {
        Destroy(this.gameObject);
    }

}
