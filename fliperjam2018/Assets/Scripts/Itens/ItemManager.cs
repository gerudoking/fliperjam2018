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
	private GameObject bSpike;
	[SerializeField]
	private GameObject spike;
	[SerializeField]
	private GameObject bBlock;

    [SerializeField]
    private bool isPlayer1;

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

	//FLow de jogo
	[SerializeField]
	private GameflowController flow;

	private Timer timer;
    PlayerMove p1;
    PlayerMove p2;


    // Use this for initialization
    void Start () {
		timer = new Timer(Timer.TYPE.DECRESCENTE, spawnTime);
        p1 = CenarioManager.p1.GetComponent<PlayerMove>();
        p2 = CenarioManager.p2.GetComponent<PlayerMove>();

        AtualizeValues();

    }

    public void AtualizeValues()
    {
        if (isPlayer1) //Apenas para eu não precisar ficar ajeitando na mão, dai ele busca no player
        {
            posLine0 = p1.GetLanePos(1);
            posLine1 = p1.GetLanePos(0);
            posLine2 = p1.GetLanePos(-1);
        }
        else
        {
            posLine0 = p2.GetLanePos(1);
            posLine1 = p2.GetLanePos(0);
            posLine2 = p2.GetLanePos(-1);
        }
    }

	// Update is called once per frame
	void Update () {
		if(flow.gameStarted){
			timer.Update();
			if(timer.Finished()){
				SpawnItem();
				timer.Reset();
			}
		}
	}

	private void SpawnItem(){
		//Debug.Log("Item spawned");
		int lane = UnityEngine.Random.Range(-1,2);
		float lanePos = 0;
		switch(lane)
        {
			case 1:
				lanePos = posLine0;
			break;
			case 0:
				lanePos = posLine1;
			break;
			case -1:
				lanePos = posLine2;
			break;
		}

        GameObject obj = new GameObject();
        Destroy(obj);
		var random = UnityEngine.Random.Range(0,5);
		switch(random){
			case 0:
			    obj = GameObject.Instantiate(block, new Vector3(xLimit, lanePos, -1), Quaternion.identity);
			break;
			case 1:
                obj = GameObject.Instantiate(bomb, new Vector3(xLimit, lanePos, -1), Quaternion.identity);
			break;
			case 2:
			    obj =GameObject.Instantiate(bSpike, new Vector3(xLimit, lanePos, -1), Quaternion.identity);
			break;
			case 3:
			    obj =GameObject.Instantiate(spike, new Vector3(xLimit, lanePos, -1), Quaternion.identity);
			break;
			case 4:
			    obj =GameObject.Instantiate(bBlock, new Vector3(xLimit, lanePos, -1), Quaternion.identity);
			break;
		}
        obj.GetComponent<Item>().setLane(lane);
	}
}
