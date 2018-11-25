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

    //FLow de jogo
	[SerializeField]
	private GameflowController flow;

    //Vidas
    [SerializeField]
    private Transform lifeSymbols;

    private GameObject enemyPlayer;
    private Timer attackCooldown;

    public int life;

    private Animator anim;
    private AudioSource audioS;

    public void Start()
    {
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
        attackCooldown = new Timer(Timer.TYPE.DECRESCENTE, cooldownTime);

        if (this.tag == "Player1")  //Procura o outro player com base na tag
            enemyPlayer = GameObject.FindGameObjectWithTag("Player2");
        else
            enemyPlayer = GameObject.FindGameObjectWithTag("Player1");
    }

    private void Update() 
    {
        if (flow.gameStarted)
        {
            //Controle dos corações
            if (life == 1)
            {
                lifeSymbols.GetChild(0).gameObject.SetActive(true);
                lifeSymbols.GetChild(1).gameObject.SetActive(false);
                lifeSymbols.GetChild(2).gameObject.SetActive(false);
            }
            else if (life == 2)
            {
                lifeSymbols.GetChild(0).gameObject.SetActive(true);
                lifeSymbols.GetChild(1).gameObject.SetActive(true);
                lifeSymbols.GetChild(2).gameObject.SetActive(false);
            }
            else if (life == 3)
            {
                lifeSymbols.GetChild(0).gameObject.SetActive(true);
                lifeSymbols.GetChild(1).gameObject.SetActive(true);
                lifeSymbols.GetChild(2).gameObject.SetActive(true);
            }

            if (Input.GetAxisRaw("FireP1")  > 0 && this.tag == "Player1")
                UseItem(1);

            if (Input.GetAxisRaw("FireP1") < 0 && this.tag == "Player1")
                UseItem(2);

            if (Input.GetButtonDown("AttackP1") && this.tag == "Player1" && attackCooldown.Finished())
                BasicAttack(true);

            if (Input.GetAxisRaw("FireP2") > 0 && this.tag == "Player2")
                UseItem(1);

            if (Input.GetAxisRaw("FireP2") < 0 && this.tag == "Player2")
                UseItem(2);

            if (Input.GetButtonDown("AttackP2") && this.tag == "Player2" && attackCooldown.Finished())
                BasicAttack(false);
        }
    }


    void UseItem(int buttonNumber) //passar numero do botão que tá sendo apertado, tipo 1 ou 2
    {
        if (buttonNumber == 1 & slot0 == null) //verificar se o slot não está vazio , senão ele sai
            return;

        if (buttonNumber == 2 && slot1 == null)
            return;
        
        audioS.clip = castSound;
        audioS.Play();

        var atualLane = GetComponent<PlayerMove>().lane;
        var spawnPos = enemyPlayer.GetComponent<PlayerMove>().GetLanePos(atualLane);

        string objToSpawn = (buttonNumber == 1) ? objToSpawn = slot0 : objToSpawn = slot1; //pegando o nome do item dependendo do button recebido

        //Caso da big rock, que ocupa todas as lanes
        if(objToSpawn == "bigRock" || objToSpawn == "bigSpike"){
            spawnPos = enemyPlayer.GetComponent<PlayerMove>().GetLanePos(0);
        }

        GameObject obj = (GameObject) Resources.Load(Path.Combine("Prefabs", objToSpawn)); //Busca e Carregando o obj pelo nome

        if (obj == null)
        {
            Debug.Log("Não foi possivel retornar o objeto");
            return;
        }
        anim.SetTrigger("isCasting");

        GameObject inst = Instantiate(obj, new Vector3(20, spawnPos), Quaternion.identity);
        inst.GetComponent<MapObject>().isPlayer1 = (this.tag == "Player1") ? false : true;
        switch (atualLane){
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

    public void takeDamage()
    {
        Debug.Log("damaging");
        life--;
        anim.SetTrigger("isDamage");
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
