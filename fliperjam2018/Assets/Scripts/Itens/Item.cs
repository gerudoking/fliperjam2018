using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MapObject {

	[SerializeField]
	private string itemType;
	[SerializeField]
	private Sprite imageToShow;

	private void Start(){
		base.Start();

		transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = imageToShow;
	}

	public string ItemType{
		get{
			return itemType;
		}
	}
}
