using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerInventory : MonoBehaviour {

    [SerializeField]
	private string slot0 = null;

    [SerializeField]
    private string slot1 = null;

    private GameObject enemyPlayer;

    public int life;

    public void Start()
    {
        if (this.tag == "Player1")  //Procura o outro player com base na tag
            enemyPlayer = GameObject.FindGameObjectWithTag("Player2");
        else
            enemyPlayer = GameObject.FindGameObjectWithTag("Player1");
    }

    private void Update() //Temporario apenas para testar as teclas
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && this.tag == "Player1")
            UseItem(1);

        if(Input.GetKeyDown(KeyCode.Alpha2) && this.tag == "Player1")
            UseItem(2);

        if (Input.GetKeyDown(KeyCode.Alpha9) && this.tag == "Player2")
            UseItem(1);

        if(Input.GetKeyDown(KeyCode.Alpha0) && this.tag == "Player2")
            UseItem(2);
    }


    void UseItem(int buttonNumber) //passar numero do botão que tá sendo apertado, tipo 1 ou 2
    {
        if (buttonNumber == 1 & slot0 == null) //verificar se o slot não está vazio , senão ele sai
            return;

        if (buttonNumber == 2 && slot1 == null)
            return;

        var atualLane = GetComponent<PlayerMove>().lane;
        var spawnPos = enemyPlayer.GetComponent<PlayerMove>().GetLanePos(atualLane);

        string objToSpawn = (buttonNumber == 1) ? objToSpawn = slot0 : objToSpawn = slot1; //pegando o nome do item dependendo do button recebido

        //Caso da big rock, que ocupa todas as lanes
        if(objToSpawn == "bigRock"){
            spawnPos = enemyPlayer.GetComponent<PlayerMove>().GetLanePos(0);
        }

        GameObject obj = (GameObject) Resources.Load(Path.Combine("Prefabs", objToSpawn)); //Busca e Carregando o obj pelo nome

        GameObject inst = Instantiate(obj, new Vector3(20, spawnPos), Quaternion.identity);

        switch(atualLane){
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

        if(objToSpawn == "bigRock"){
            inst.layer = 0;
        }

        var clear = (buttonNumber == 1) ? slot0 = null : slot1 = null; //limpar o slot usado

    }

	private void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag == "isItem" && GetComponent<PlayerMove>().lane == c.GetComponent<Item>().Lane){
			if(string.IsNullOrEmpty(slot0)){
				slot0 = c.gameObject.GetComponent<Item>().ItemType;
                Destroy(c.gameObject);
				return;
			}
			if(string.IsNullOrEmpty(slot1)){
				slot1 = c.gameObject.GetComponent<Item>().ItemType;
            }
            Destroy(c.gameObject); // destruir mesmo que não possa pegar, para não dar a sensação de "ue pq não pegou" ao jogador
        }
    }
}
