using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	//Prefabs dos itens
	[SerializeField]
	private GameObject block;
	[SerializeField]
	private GameObject bomb;
	[SerializeField]
	private GameObject slime;

	//Posição das lanes desse item manager
	[SerializeField]
	private float posLine0;	//topo
	[SerializeField]
	private float posLine1;	//mid
	[SerializeField]
	private float posLine2;	//bot

	//Limite direito da tela
	[SerializeField]
	private float xLimit;

	[SerializeField]
	private float spawnTime;

	private Timer timer;

	// Use this for initialization
	void Start () {
		timer = new Timer(Timer.TYPE.DECRESCENTE, spawnTime);
	}

	// Update is called once per frame
	void Update () {
		timer.Update();
		if(timer.Finished()){
			SpawnItem();
			timer.Reset();
		}
	}

	private void SpawnItem(){
		Debug.Log("Item spawned");
		int random = UnityEngine.Random.Range(0,3);
		float lanePos = 0;
		switch(random){
			case 0:
				lanePos = posLine0;
			break;
			case 1:
				lanePos = posLine1;
			break;
			case 2:
				lanePos = posLine2;
			break;
		}

		random = UnityEngine.Random.Range(0,3);
		switch(random){
			case 0:
				GameObject.Instantiate(block, new Vector3(xLimit, lanePos, 0), Quaternion.identity);
			break;
			case 1:
				GameObject.Instantiate(bomb, new Vector3(xLimit, lanePos, 0), Quaternion.identity);
			break;
			case 2:
				GameObject.Instantiate(slime, new Vector3(xLimit, lanePos, 0), Quaternion.identity);
			break;
		}
	}
}
