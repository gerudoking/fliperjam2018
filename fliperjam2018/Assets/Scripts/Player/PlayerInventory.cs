using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	private string slot0 = null;
	private string slot1 = null;

	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerEnter2D(Collider2D c){
		Debug.Log("colidiu");
		if(c.gameObject.tag == "isItem"){
			Debug.Log("Entrou aqui");
			if(slot0 == null){
				Debug.Log("slot0 eh nulo");
				slot0 = c.gameObject.GetComponent<Item>().ItemType;
				GameObject.Destroy(c.gameObject);
				return;
			}
			if(slot1 == null){
				Debug.Log("slot1 eh nulo");
				slot1 = c.gameObject.GetComponent<Item>().ItemType;
				GameObject.Destroy(c.gameObject);
			}
		}
	}
}
