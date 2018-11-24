using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHabilities : MonoBehaviour {

    private Rigidbody2D rigid;
    public float dashForce;
    public float dashMaxDistance;

    private bool isDashing = false;
    public float dashCoolDown = 1f;
    private bool isOnCoolDown = false;

    private  Vector2 originalPos;
    public float backVelocity = 0.5f;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (!isOnCoolDown && !isDashing)
                originalPos = transform.position;
            StartCoroutine(Dash(true));
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (!isOnCoolDown && !isDashing)
                originalPos = transform.position;
            StartCoroutine(Dash(false));
        }
    }

    public IEnumerator Dash(bool isRight)
    {
        if (!isDashing && !isOnCoolDown)
        {
            Debug.Log("Dashing");
            isDashing = true;

            Vector2 originalV = rigid.velocity;
            float force = (isRight) ? force = dashForce : force = -dashForce;
            rigid.AddForce(new Vector2(force, rigid.velocity.y), ForceMode2D.Impulse);

            while (Mathf.Abs(transform.position.x - originalPos.x) < dashMaxDistance)
            {
                yield return null;
            }

            rigid.velocity = originalV;

            StartCoroutine(BackToOriginalPos());
            StartCoroutine(RunCoolDown());
            isDashing = false;
        }
        else
            yield break;
    }

    public IEnumerator BackToOriginalPos()
    {
        while (Mathf.Abs(transform.position.x - originalPos.x) > 0.05f)
        {
            var newPos = Mathf.Lerp(transform.position.x, originalPos.x, backVelocity );
            yield return null;
            transform.position = new Vector2(newPos, transform.position.y);
        }
        transform.position = new Vector2(originalPos.x, transform.position.y);
    }

    public IEnumerator RunCoolDown()
    {
        isOnCoolDown = true;
        yield return new WaitForSeconds(dashCoolDown);
        isOnCoolDown = false;
    }


}
