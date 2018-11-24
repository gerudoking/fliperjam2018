using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MapObject {

	[SerializeField]
	private float explosionX;

	// Update is called once per frame
	void Update () {
		if(transform.position.x <= explosionX){
			Explode();
		}
	}

	private void Explode(){
		GetComponent<AudioSource>().Play();

		GameObject obj = null;
		if(player == true){
			obj = GameObject.FindGameObjectWithTag("Player1");
		}
		else{
			obj = GameObject.FindGameObjectWithTag("Player2");
		}
		if(obj.GetComponent<PlayerMove>().lane == lane)
			obj.GetComponent<PlayerInventory>().life--;
		GameObject.Destroy(gameObject);
	}
}
