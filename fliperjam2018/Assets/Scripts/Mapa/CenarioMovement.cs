using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenarioMovement : MonoBehaviour {

    private Rigidbody2D rigid;
    private BoxCollider2D box;
    private float xSize;

    public void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        xSize = box.bounds.extents.x;
    }

    public void LateUpdate()
    {
        rigid.velocity = new Vector2( -CenarioManager.velocity,0);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "end")
            BackToBegin();
    }

    public void BackToBegin()
    {
        var lastPos = CenarioManager.lastPlat;

        this.transform.position = new Vector3(lastPos.position.x + (xSize*2), this.transform.position.y, this.transform.position.z);
        CenarioManager.lastPlat = this.transform;
    }


}
