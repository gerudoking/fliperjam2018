using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTextController : MonoBehaviour {

	private Timer t;

	// Use this for initialization
	void Start () {
		t = new Timer(Timer.TYPE.DECRESCENTE, 1);
	}

	// Update is called once per frame
	void Update () {
		t.Update();
		if(t.Finished())
			GameObject.Destroy(gameObject);
	}
}
