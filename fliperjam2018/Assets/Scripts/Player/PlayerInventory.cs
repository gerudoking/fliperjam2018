using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject itemText;

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

    [SerializeField]
    private Camera cam;

    public Image skill1;
    public Sprite ImagemVazia;
    public Image skill2;

    public Sprite[] objs;
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
            attackCooldown.Update();

            if (Input.GetButtonDown("Fire4-1")&& this.tag == "Player1")
                UseItem(1);

            if (Input.GetButtonDown("Fire1-1")&& this.tag == "Player1")
                UseItem(2);

            if (Input.GetButtonDown("Fire2-1") && this.tag == "Player1" && attackCooldown.Finished())
                BasicAttack(true);

            if (Input.GetButtonDown("Fire4-2")&& this.tag == "Player2")
                UseItem(1);

            if (Input.GetButtonDown("Fire1-2") && this.tag == "Player2")
                UseItem(2);

            if (Input.GetButtonDown("Fire2-2") && this.tag == "Player2" && attackCooldown.Finished())
                BasicAttack(false);
        }
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
        if(objToSpawn == "bigRock" || objToSpawn == "bigSpike"){
            spawnPos = enemyPlayer.GetComponent<PlayerMove>().GetLanePos(0);
        }

        GameObject obj = (GameObject) Resources.Load(Path.Combine("Prefabs", objToSpawn)); //Busca e Carregando o obj pelo nome

        if (obj == null)
        {
            Debug.Log("Não foi possivel retornar o objeto");
            return;
        }

        if (buttonNumber == 1)
            skill1.sprite = ImagemVazia;
       if (buttonNumber == 2)
            skill2.sprite = ImagemVazia;

        anim.SetTrigger("isCasting");
        audioS.clip = soundsPlayer.cast_sound;
        audioS.Play();
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

        audioS.clip = soundsPlayer.attack_sound;
        audioS.Play();

        anim.SetTrigger("isCasting");

        var atualLane = GetComponent<PlayerMove>().lane;
        var spawnPos = enemyPlayer.GetComponent<PlayerMove>().GetLanePos(atualLane);

        GameObject inst = Instantiate(basicBullet, new Vector3(20, spawnPos), Quaternion.identity);

        inst.GetComponent<Bullet>().stunOrDamage = type;
        attackCooldown.Reset();
    }

    public void takeDamage()
    {
        audioS.clip = soundsPlayer.damage_sound;
        audioS.Play();
        life--;
        anim.SetTrigger("isDamage");
    }

    void atualizarIcone(string itemType,int slot)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            Debug.Log(objs[i].name + "  " + itemType);
            if(objs[i].name == itemType)
            {
                if (slot == 1)
                    skill1.sprite = objs[i];
                if(slot == 2)
                    skill2.sprite = objs[i];

                break;
            }
        }
        
    }

	private void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag == "isItem" && GetComponent<PlayerMove>().lane == c.GetComponent<Item>().Lane){
			if(string.IsNullOrEmpty(slot0)){
				slot0 = c.gameObject.GetComponent<Item>().ItemType;
                Destroy(c.gameObject);
                GameObject obj = Instantiate(itemText, cam.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 2)), Quaternion.identity, flow.transform);
                var nome = c.gameObject.GetComponent<Item>().ItemType;
                obj.GetComponent<Text>().text = nome;
                atualizarIcone(nome, 1);
				return;
			}
			if(string.IsNullOrEmpty(slot1)){
				slot1 = c.gameObject.GetComponent<Item>().ItemType;
                GameObject obj = Instantiate(itemText, cam.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 2)), Quaternion.identity, flow.transform);
                var nome = c.gameObject.GetComponent<Item>().ItemType;
                obj.GetComponent<Text>().text = nome;
                atualizarIcone(nome, 2);
            }
            Destroy(c.gameObject); // destruir mesmo que não possa pegar, para não dar a sensação de "ue pq não pegou" ao jogador
        }
    }
}
