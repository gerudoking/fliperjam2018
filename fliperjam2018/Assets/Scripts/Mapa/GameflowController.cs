using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameflowController : MonoBehaviour {

	public bool gameStarted = false;
	public float timeValue, presentationValue;
	private Timer timeToStart, presentationTime;

	// Use this for initialization
	void Start () {
		timeToStart = new Timer(Timer.TYPE.DECRESCENTE, timeValue);
		presentationTime = new Timer(Timer.TYPE.DECRESCENTE, presentationValue);

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
					newName += "despair ";
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
					newName = "Pestilence";
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
					newName += " Ghoul";
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
}
