﻿using System.Collections;
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

	public int lane = 0;	//1 = topo, -1 = bot, 0 = mid , public pq preciso pegar fora
    private Rigidbody2D rigid;

    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
		float xValue = transform.position.x;

		//Seta os valores x para movimentação
		posLine0.transform.position = new Vector3(xValue, posLine0.transform.position.y);
		posLine1.transform.position = new Vector3(xValue, posLine1.transform.position.y);
		posLine2.transform.position = new Vector3(xValue, posLine2.transform.position.y);

        //Movimento em si
        if(!isJumping)
            Move(); //Coloquei em uma função para ter controle de quando pode executar as movimentações ou não

        //Pulando 
        if(playerNum)
        {
            Debug.Log(Input.GetAxisRaw("Jump_1"));
            if (Input.GetAxisRaw("Jump_1") > 0)
                StartCoroutine( Jump());
        }

    }

    public IEnumerator Jump()
    {
        if(!isJumping)
        {
            isJumping = true;
            float initialYPos = GetLanePos(lane);
            rigid.gravityScale = 2;
            rigid.AddForce(new Vector2(rigid.velocity.x, jumpForce), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);

            while (Mathf.Abs(initialYPos - transform.position.y) > .3f)
                yield return null;

            rigid.gravityScale = 0;
            isJumping = false;
        }
    }

    public void Move()
    {
        //Movimento por axis do primeiro jogador
        if (playerNum)
        {
            if (Input.GetAxisRaw("Vertical_1") == 1)
            {
                lane = 1;
            }
            else if (Input.GetAxisRaw("Vertical_1") == -1)
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
            if (Input.GetAxisRaw("Vertical_2") == 1)
            {
                lane = 1;
            }
            else if (Input.GetAxisRaw("Vertical_2") == -1)
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
}
