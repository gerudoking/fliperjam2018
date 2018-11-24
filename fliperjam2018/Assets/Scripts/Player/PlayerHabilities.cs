using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHabilities : MonoBehaviour {

    private Rigidbody2D rigid;
    public float dashForce;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxisRaw("Horizontal") > 0)
            Dash(true);

        if (Input.GetAxisRaw("Horizontal") < 0)
           Dash(false);
	}

    public void Dash(bool isRight)
    {
        float force = (isRight == true) ? force = dashForce : force = -dashForce;

        rigid.AddForce(new Vector2(force, rigid.velocity.y), ForceMode2D.Impulse);
    }

}
