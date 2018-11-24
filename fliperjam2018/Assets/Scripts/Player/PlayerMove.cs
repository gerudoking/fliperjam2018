using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	[SerializeField]
	private bool playerNum;

	[SerializeField]
	private Vector2 posLine0;	//topo
	[SerializeField]
	private Vector2 posLine1;	//mid
	[SerializeField]
	private Vector2 posLine2;	//bot

	[SerializeField]
	private float changeLaneSpeed;

	[SerializeField]
	private float valor;

	private int lane = 0;	//1 = topo, -1 = bot, 0 = mid

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float xValue = transform.position.x;

		//Seta os valores x para movimentação
		posLine0 = new Vector3(xValue, posLine0.y);
		posLine1 = new Vector3(xValue, posLine1.y);
		posLine2 = new Vector3(xValue, posLine2.y);

		//Movimento por axis do primeiro jogador
		if(playerNum){
			if(Input.GetAxisRaw("Vertical_1") == 1){
				lane = 1;
			}
			else if (Input.GetAxisRaw("Vertical_1") == -1){
				lane = -1;
			}
			else{
				lane = 0;
			}
		}
		//Movimento por axis do segundo jogador
		else{
			if(Input.GetAxisRaw("Vertical_2") == 1){
				lane = 1;
			}
			else if (Input.GetAxisRaw("Vertical_2") == -1){
				lane = -1;
			}
			else{
				lane = 0;
			}
		}

		//Movimento em si
		float velX = GetComponent<Rigidbody2D>().velocity.x;

		if(lane == 1){
			if(Vector3.Distance(transform.position, posLine0) > valor){
				GetComponent<Rigidbody2D>().velocity = new Vector2(velX, changeLaneSpeed);
			}
			else{
				GetComponent<Rigidbody2D>().velocity = new Vector2(velX, 0);
				transform.position = new Vector2(transform.position.x, posLine0.y);
			}
		}
		else if(lane == -1){
			if(Vector3.Distance(transform.position, posLine2) > valor){
				GetComponent<Rigidbody2D>().velocity = new Vector2(velX, -changeLaneSpeed);
			}
			else{
				GetComponent<Rigidbody2D>().velocity = new Vector2(velX, 0);
				transform.position = new Vector2(transform.position.x, posLine2.y);
			}
		}
		else{
			if(Vector3.Distance(transform.position, posLine1) > valor){
				if(transform.position.y - posLine1.y > 0){
					GetComponent<Rigidbody2D>().velocity = new Vector3(velX, -changeLaneSpeed);
				}
				else{
					GetComponent<Rigidbody2D>().velocity = new Vector3(velX, changeLaneSpeed);
				}
			}
			else{
				GetComponent<Rigidbody2D>().velocity = new Vector3(velX, 0);
				transform.position = new Vector2(transform.position.x, posLine1.y);
			}
		}
	}
}
