using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	[SerializeField]
	private bool playerNum;

    [SerializeField]
    private bool isJumping;

    [SerializeField]
    private float jumpForce = 3;

    [SerializeField]
    private Transform posLine0;    //topo

    [SerializeField]
    private Transform posLine1;    //mid

    [SerializeField]
    private Transform posLine2;	//bot

	[SerializeField]
	private float changeLaneSpeed;

	[SerializeField]
	private float valor = .5f; // se ele ficar em valor muito pequeno ele passa e continua

	[SerializeField]
	private float catchupSpeed;

	[SerializeField]
	private AudioClip jumpSound;

    public ItemManager thisItemManager;

    public AudioSource audioS;

	public int lane = 0;	//1 = topo, -1 = bot, 0 = mid , public pq preciso pegar fora
    private Rigidbody2D rigid;
	private float lossX;
	private float standardX = 0;
	private int collisionCounter = 0;
	private bool catchingUp = false;
	private bool lost = false;
    private bool isStun = false;
    public float stunedTime = 0.5f;
	private Animator anim;

    public Sprite winRoxo;
    public Sprite loseRoxo;
    public Sprite winVerde;
    public Sprite loseVerde;


    public bool PlayerNum{
		get{
			return playerNum;
		}
	}

	public bool IsJumping{
		get{
			return isJumping;
		}
	}

	//FLow de jogo
	[SerializeField]
	private GameflowController flow;

    public void Start()
    {
		anim = GetComponent<Animator>();

        Camera cam;

        if (playerNum)
            cam = GameObject.FindGameObjectWithTag("camp1").gameObject.GetComponent<Camera>();
        else
            cam = GameObject.FindGameObjectWithTag("camp2").GetComponent<Camera>();

        audioS = GetComponent<AudioSource>();

		lossX = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x / 2;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
		if(flow.gameStarted){
			float xValue = transform.position.x;

			//Seta os valores x para movimentação
			posLine0.transform.position = new Vector3(xValue, posLine0.transform.position.y);
			posLine1.transform.position = new Vector3(xValue, posLine1.transform.position.y);
			posLine2.transform.position = new Vector3(xValue, posLine2.transform.position.y);

        	//Movimento em si
        	if(!isJumping && !isStun)
            	Move(); //Coloquei em uma função para ter controle de quando pode executar as movimentações ou não

        	//Pulando
        	if(playerNum)
        	{
            	//Debug.Log(Input.GetAxisRaw("Jump_1"));
            	if (Input.GetButtonDown("Fire3-1")){
                	StartCoroutine( Jump());
				}
        	}
			else{
				if (Input.GetButtonDown("Fire3-2"))
                {
                	StartCoroutine( Jump());
				}
			}

			//Catchup!
			if(transform.position.x < standardX && collisionCounter == 0){
				rigid.velocity = new Vector3(catchupSpeed, rigid.velocity.y);
				//Debug.Log("Catching Up!");
				catchingUp = true;
			}
			else if(transform.position.x >= standardX && catchingUp){
				rigid.velocity = new Vector3(0, rigid.velocity.y);
				transform.position = new Vector3(standardX, transform.position.y, 0);
				catchingUp = false;
			}

			//Loss condition
			if(lost || GetComponent<PlayerInventory>().life <= 0){
				LoseGame();
			}
		}
    }

    public IEnumerator Jump()
    {
        if(!isJumping)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            isJumping = true;
			anim.SetBool("isJumping", true);
            float initialYPos = GetLanePos(lane);
            rigid.gravityScale = 2;
            rigid.AddForce(new Vector2(rigid.velocity.x, jumpForce), ForceMode2D.Impulse);
            GetComponent<CircleCollider2D>().enabled = false;

			audioS.clip = soundsPlayer.jump_sound;
	        audioS.Play();

            yield return new WaitForSeconds(0.1f);

            while (Mathf.Abs(initialYPos - transform.position.y) > .3f)
                yield return null;

            rigid.gravityScale = 0;
            GetComponent<CircleCollider2D>().enabled = true;
            isJumping = false;
			anim.SetBool("isJumping", false);
        }
    }

    public void Move()
    {
        //Movimento por axis do primeiro jogador
        if (playerNum)
        {
            if (Input.GetAxisRaw("Vertical-1") == 1)
            {
                lane = 1;
            }
            else if (Input.GetAxisRaw("Vertical-1") == -1)
            {
                lane = -1;
            }
            else
            {
                lane = 0;
            }
        }
        //Movimento por axis do segundo jogador
        else
        {
            if (Input.GetAxisRaw("Vertical-2") == 1)
            {
                lane = 1;
            }
            else if (Input.GetAxisRaw("Vertical-2") == -1)
            {
                lane = -1;
            }
            else
            {
                lane = 0;
            }
        }

        //---------//

        float velX = rigid.velocity.x;

        if (lane == 1)
        {
			//Seta o layer para a física da lane
			gameObject.layer = 8;

            if (Vector3.Distance(transform.position, posLine0.transform.position) > valor)
            {
                rigid.velocity = new Vector2(velX, changeLaneSpeed);
            }
            else
            {
                rigid.velocity = new Vector2(velX, 0);
                transform.position = new Vector2(transform.position.x, posLine0.transform.position.y);
            }
        }


        if (lane == -1)
        {
			//Seta o layer para a física da lane
			gameObject.layer = 10;

            if (Vector3.Distance(transform.position, posLine2.transform.position) > valor)
            {
                rigid.velocity = new Vector2(velX, -changeLaneSpeed);
            }
            else
            {
                rigid.velocity = new Vector2(velX, 0);
                transform.position = new Vector2(transform.position.x, posLine2.transform.position.y);
            }
        }


        if (lane == 0)
        {
			//Seta o layer para a física da lane
			gameObject.layer = 9;

            if (Vector3.Distance(transform.position, posLine1.transform.position) > valor)
            {
                if (transform.position.y - posLine1.transform.position.y > 0)
                {
                    rigid.velocity = new Vector3(velX, -changeLaneSpeed);
                }
                else
                {
                    rigid.velocity = new Vector3(velX, changeLaneSpeed);
                }
            }
            else
            {
                rigid.velocity = new Vector3(velX, 0);
                transform.position = new Vector2(transform.position.x, posLine1.transform.position.y);
            }
        }
    }

    public float GetLanePos(int laneNumber) //apenas uma maneira para eu retornar o valor da lane requisitada por fora
    {
        switch (laneNumber)
        {
            case 1:
               return posLine0.transform.position.y;

            case 0:
                return posLine1.transform.position.y;

            case -1:
                return posLine2.transform.position.y;

            default:
                return 404; //apenas para retornar algo e não ter problema na função

        }
    }

    public Transform GetLaneTransform(int lane)
    {
        switch (lane)
        {
            case 1:
                return posLine0;

            case 0:
                return posLine1;

            case -1:
                return posLine2;

            default: return null;
        }
    }

    public void SetLaneTransform(Transform t , int lane)
    {
        switch (lane)
        {
            case 1:
                posLine0 = t;
                break;

            case 0:
                posLine1 = t;
                break;

            case -1:
                posLine2 = t;
                break;
        }

    }

	//Chamada quando este jogador perde o jogo
	private void LoseGame(){
        if(CenarioManager.winObj.activeInHierarchy == false)
        {
            if (playerNum)
            {
                CenarioManager.winObj.GetComponent<UnityEngine.UI.Image>().sprite = CenarioManager.winP2;
                CenarioManager.winObj.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = winVerde;
                CenarioManager.winObj.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = loseRoxo;
                
            }
            else
            {
                CenarioManager.winObj.GetComponent<UnityEngine.UI.Image>().sprite = CenarioManager.winP1;
                CenarioManager.winObj.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = winRoxo;
                CenarioManager.winObj.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = loseVerde;
            }
        }
        CenarioManager.winObj.SetActive(true);
	}

	private void OnCollisionEnter2D(Collision2D c){
		collisionCounter++;
        StartCoroutine(RecoverStun(stunedTime));
	}

	private void OnCollisionExit2D(Collision2D c){
		collisionCounter--;
		rigid.velocity = new Vector2(0, rigid.velocity.y);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "end")
			lost = true;
	}

    public IEnumerator RecoverStun(float timeStun)
    {
        audioS.clip = soundsPlayer.stun_sound;
        audioS.Play();
        isStun = true;
        yield return new WaitForSeconds(timeStun);
        isStun = false;
    }

	public void CallStun(float timeStun){
		StartCoroutine(RecoverStun(timeStun));
	}

}
