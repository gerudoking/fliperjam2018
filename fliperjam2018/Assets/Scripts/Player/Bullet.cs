using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MapObject {
	public bool stunOrDamage;	//True stun, false damage

	private void Start(){
		if(stunOrDamage){
			base.Start();
		}
		else{
			rb = GetComponent<Rigidbody2D>();

	        //Agora tá pegando a velocidade do cenario
	        speed = -CenarioManager.velocity - 2; //-4;

			rb.velocity = new Vector2(speed, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D c){
		base.OnTriggerEnter2D(c);
		if(c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2"){
			if(stunOrDamage){
				c.GetComponent<PlayerMove>().CallStun(0.2f);
			}
			else{
				c.GetComponent<PlayerInventory>().life--;
			}
			GameObject.Destroy(gameObject);
		}
	}
}
