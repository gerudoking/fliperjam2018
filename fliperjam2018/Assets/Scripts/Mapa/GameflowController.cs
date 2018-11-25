using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameflowController : MonoBehaviour {

	public bool gameStarted = false;
	public float timeValue, presentationValue, swapValue, swapLesserValue;
	public CenarioManager manager;
	public int maxNumberOfBlinks;
	public float obstacleSpawnTime;
	public PlayerMove p1, p2;
	private Timer timeToStart, presentationTime, swapTime, swapLesserTime,timerObstacle;
	private int numberOfBlinks;

	// Use this for initialization
	void Start () {
		timeToStart = new Timer(Timer.TYPE.DECRESCENTE, timeValue);
		presentationTime = new Timer(Timer.TYPE.DECRESCENTE, presentationValue);
		swapTime = new Timer(Timer.TYPE.DECRESCENTE, swapValue);
		swapLesserTime = new Timer(Timer.TYPE.DECRESCENTE, swapLesserValue);
		timerObstacle = new Timer(Timer.TYPE.DECRESCENTE, obstacleSpawnTime);

		//Random names
		transform.Find("FadeOutPanel").GetChild(0).GetComponent<Text>().text = RandomName(true);
		transform.Find("FadeOutPanel").GetChild(1).GetComponent<Text>().text = RandomName(false);
	}

	// Update is called once per frame
	void Update () {
		if(!gameStarted){
			if(!presentationTime.Finished()){
				presentationTime.Update();
			}
			else{
				timeToStart.Update();
				if(timeToStart.Finished()){
					gameStarted = true;
				}

				transform.Find("FadeOutPanel").GetComponent<Image>().color = new Color32(0, 0, 0, (byte)(255 * timeToStart.GetRatio()));
				transform.Find("FadeOutPanel").GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, (byte)(255 * timeToStart.GetRatio()));
				transform.Find("FadeOutPanel").GetChild(1).GetComponent<Text>().color = new Color32(255, 255, 255, (byte)(255 * timeToStart.GetRatio()));
			}
		}
		else{
			transform.Find("FadeOutPanel").GetComponent<Image>().color = new Color32(0, 0, 0, 0);
			transform.Find("FadeOutPanel").GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 0);
			transform.Find("FadeOutPanel").GetChild(1).GetComponent<Text>().color = new Color32(255, 255, 255, 0);
            //this.enabled = false;

			//Spawn de obstaculos
			timerObstacle.Update();

			if(timerObstacle.Finished()){
				if(p1 != null)
					SpawnObstacle(true);

				if(p2 != null)
					SpawnObstacle(false);

				timerObstacle.Reset();
			}

			//Player swap
			swapTime.Update();

			if(swapTime.Finished()){
				swapLesserTime.Update();
				if(swapLesserTime.Finished()){
					if(numberOfBlinks == maxNumberOfBlinks){
						transform.Find("FadeOutPanel").GetComponent<Image>().color = new Color32(255, 255, 255, 0);
						manager.SwapPlayers();
						swapTime.Reset();
						swapLesserTime.Reset();
						numberOfBlinks = 0;
					}
					else{
						swapLesserTime.Reset();
						numberOfBlinks++;
					}
				}
				transform.Find("FadeOutPanel").GetComponent<Image>().color = new Color32(255, 255, 255, (byte)(255 * swapLesserTime.GetRatio()));
			}
		}
	}

	private string RandomName(bool whatMap){
		string newName = null;

		int rand = UnityEngine.Random.Range(0,5);

		if(whatMap){
			switch(rand){
				case 0:
					newName = "Blood";
				break;
				case 1:
					newName = "Crimson";
				break;
				case 2:
					newName = "Carnage";
				break;
				case 3:
					newName = "Gore";
				break;
				case 4:
					newName = "Flesh";
				break;
			}

			rand = UnityEngine.Random.Range(0,5);

			switch(rand){
				case 0:
					newName += "soaked ";
				break;
				case 1:
					newName += "scream ";
				break;
				case 2:
					newName += "sorrow ";
				break;
				case 3:
					newName += "broken ";
				break;
				case 4:
					newName += "dread ";
				break;
			}

			rand = UnityEngine.Random.Range(0,3);

			switch(rand){
				case 0:
					newName += "Caves";
				break;
				case 1:
					newName += "Dungeons";
				break;
				case 2:
					newName += "Tomb";
				break;
			}
		}
		else{
			switch(rand){
				case 0:
					newName = "Pestilent";
				break;
				case 1:
					newName = "Putrid";
				break;
				case 2:
					newName = "Cursed";
				break;
				case 3:
					newName = "Mold";
				break;
				case 4:
					newName = "Moss";
				break;
			}

			rand = UnityEngine.Random.Range(0,5);

			switch(rand){
				case 0:
					newName += "-tainted ";
				break;
				case 1:
					newName += " Skull ";
				break;
				case 2:
					newName += "-infested ";
				break;
				case 3:
					newName += " and Unholy ";
				break;
				case 4:
					newName += " Ghoul ";
				break;
			}

			rand = UnityEngine.Random.Range(0,3);

			switch(rand){
				case 0:
					newName += "Grave";
				break;
				case 1:
					newName += "Den";
				break;
				case 2:
					newName += "Tomb";
				break;
			}
		}

		return newName;
	}

	private void SpawnObstacle(bool who){
		int lane = UnityEngine.Random.Range(-1,2);
		int whatObj = UnityEngine.Random.Range(0,2);
		string objToSpawn = null;
		float spawnPos;

		switch(whatObj){
			case 0:
				objToSpawn = "rock";
			break;
			case 1:
				objToSpawn = "spike";
			break;
		}

		if(who){
			spawnPos = p1.GetLanePos(lane);
		}
		else{
			spawnPos = p2.GetLanePos(lane);
		}

		GameObject obj = (GameObject) Resources.Load(Path.Combine("Prefabs", objToSpawn)); //Busca e Carregando o obj pelo nome

        if (obj == null)
        {
            Debug.Log("Não foi possivel retornar o objeto");
            return;
        }

		GameObject inst = Instantiate(obj, new Vector3(20, spawnPos), Quaternion.identity);
		if(who)
        	inst.GetComponent<MapObject>().isPlayer1 = (p1.tag == "Player1") ? false : true;
		else
			inst.GetComponent<MapObject>().isPlayer1 = (p2.tag == "Player1") ? false : true;

        switch (lane){
            case -1:
                inst.layer = 10;
            break;
            case 0:
                inst.layer = 9;
            break;
            case 1:
                inst.layer = 10;
            break;
        }
	}
}
