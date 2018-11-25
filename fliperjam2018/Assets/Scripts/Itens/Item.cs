using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MapObject {

	[SerializeField]
	private string itemType;

	public string ItemType{
		get{
			return itemType;
		}
	}
}
