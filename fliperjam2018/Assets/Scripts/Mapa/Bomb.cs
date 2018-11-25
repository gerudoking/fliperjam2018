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
		if(isPlayer1 == true){
            obj = CenarioManager.p1;
		}
		else{
            obj = CenarioManager.p2;
		}
		if(obj.GetComponent<PlayerMove>().lane == lane)
        {
            Debug.Log("É player1? : " +isPlayer1);
            obj.GetComponent<PlayerInventory>().takeDamage();
        }
        GameObject.Destroy(gameObject);
	}
}
