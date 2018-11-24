using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenarioMovement : MonoBehaviour {

    private Rigidbody2D rigid;
    private BoxCollider2D box;
    private float xSize;
    private bool isTopSide;

    public void Start()
    {
        isTopSide = (transform.parent.tag == "TopSide") ? true : false;

        box = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        xSize = box.bounds.extents.x;
    }

    public void LateUpdate()
    {
        rigid.velocity = new Vector2( -CenarioManager.velocity,0);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "end")
            BackToBegin();
    }

    public void BackToBegin()
    {
       

        if (isTopSide)
        {
            var lastPos = CenarioManager.lastTopPlat;
            this.transform.position = new Vector3(lastPos.position.x + (xSize * 2), this.transform.position.y, this.transform.position.z);
            CenarioManager.lastTopPlat = this.transform;

        }
        else
        {
            var lastPos = CenarioManager.lastBotPlat;
            this.transform.position = new Vector3(lastPos.position.x + (xSize * 2), this.transform.position.y, this.transform.position.z);
            CenarioManager.lastBotPlat = this.transform;
        }

    }


}
