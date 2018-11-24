using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerInventory : MonoBehaviour {

    [SerializeField]
	private string slot0 = null;

    [SerializeField]
    private string slot1 = null;

    [SerializeField]
	private AudioClip castSound;

    [SerializeField]
    private GameObject basicBullet;

    [SerializeField]
    private float cooldownTime;

    private GameObject enemyPlayer;
    private Timer attackCooldown;

    public int life;

    public void Start()
    {
        attackCooldown = new Timer(Timer.TYPE.DECRESCENTE, cooldownTime);

        if (this.tag == "Player1")  //Procura o outro player com base na tag
            enemyPlayer = GameObject.FindGameObjectWithTag("Player2");
        else
            enemyPlayer = GameObject.FindGameObjectWithTag("Player1");
    }

    private void Update() //Temporario apenas para testar as teclas
    {
        attackCooldown.Update();

        if (Input.GetKeyDown(KeyCode.F) && this.tag == "Player1")
            UseItem(1);

        if(Input.GetKeyDown(KeyCode.G) && this.tag == "Player1")
            UseItem(2);

        if(Input.GetKeyDown(KeyCode.R) && this.tag == "Player1" && attackCooldown.Finished())
            BasicAttack(true);

        if (Input.GetKeyDown(KeyCode.Keypad1) && this.tag == "Player2")
            UseItem(1);

        if(Input.GetKeyDown(KeyCode.Keypad2) && this.tag == "Player2")
            UseItem(2);

        if(Input.GetKeyDown(KeyCode.Keypad4) && this.tag == "Player2" && attackCooldown.Finished())
            BasicAttack(false);
    }


    void UseItem(int buttonNumber) //passar numero do botão que tá sendo apertado, tipo 1 ou 2
    {
        if (buttonNumber == 1 & slot0 == null) //verificar se o slot não está vazio , senão ele sai
            return;

        if (buttonNumber == 2 && slot1 == null)
            return;

        GetComponent<AudioSource>().clip = castSound;
        GetComponent<AudioSource>().Play();

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

        if(objToSpawn == "bigRock" || objToSpawn == "bigSpike"){
            inst.layer = 0;
        }

        var clear = (buttonNumber == 1) ? slot0 = null : slot1 = null; //limpar o slot usado

    }

    private void BasicAttack(bool type){
        var atualLane = GetComponent<PlayerMove>().lane;
        var spawnPos = enemyPlayer.GetComponent<PlayerMove>().GetLanePos(atualLane);

        GameObject inst = Instantiate(basicBullet, new Vector3(20, spawnPos), Quaternion.identity);

        inst.GetComponent<Bullet>().stunOrDamage = type;
        attackCooldown.Reset();
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
