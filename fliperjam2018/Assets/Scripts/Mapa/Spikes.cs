using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MapObject {
	private void OnTriggerEnter2D(Collider2D c){
		base.OnTriggerEnter2D(c);
		if(c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2"){
            if (!c.GetComponent<PlayerMove>().IsJumping)
                c.GetComponent<PlayerInventory>().takeDamage();
		}
	}
}
