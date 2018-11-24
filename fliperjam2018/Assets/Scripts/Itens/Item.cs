using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	[SerializeField]
	private string itemType;

	private float speed;
	private Rigidbody2D rb;
	private int lane;

	public string ItemType{
		get{
			return itemType;
		}
	}

	public int Lane{
		get{
			return lane;
		}
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		//Pega a velocidade(TO DO). O valor abaixo é um teste
		speed = -4;

		rb.velocity = new Vector2(speed, 0);
	}
}
