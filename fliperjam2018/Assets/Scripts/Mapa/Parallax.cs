using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Parallax : MonoBehaviour {

    public Rigidbody2D rigid;
    public enum typeParallax
    {
        BG,
        Medio,
        Perto
    }
    public typeParallax thisParallax;
    public bool isP1 = false;
    private BoxCollider2D box;
    public float xSize;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.isKinematic = true;

        box = GetComponent<BoxCollider2D>();
        xSize = box.bounds.extents.x;
	}

    public void LateUpdate()
    {
        switch (thisParallax)
        {
            case typeParallax.BG:
                rigid.velocity = new Vector2(-CenarioManager.velocity/8,0);
                break;

            case typeParallax.Medio:
                rigid.velocity = new Vector2(-CenarioManager.velocity/5, 0);
                break;

            case typeParallax.Perto:
                rigid.velocity = new Vector2(-CenarioManager.velocity/3, 0);
                break;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "end")
        {
            if(isP1)
            {
                switch (thisParallax)
                {
                    case typeParallax.BG:
                        transform.position = new Vector3(ParallaxManager.BG_top.transform.position.x + (xSize *2 ),transform.position.y);
                        ParallaxManager.BG_top = this.transform;
                        break;

                    case typeParallax.Medio:
                        transform.position = new Vector3(ParallaxManager.Medio_top.transform.position.x + (xSize * 2), transform.position.y);
                        ParallaxManager.Medio_top = this.transform;
                        break;

                    case typeParallax.Perto:
                        transform.position = new Vector3(ParallaxManager.Perto_top.transform.position.x + (xSize * 2), transform.position.y);
                        ParallaxManager.Perto_top = this.transform;
                        break;
                }
            }
            else
                switch (thisParallax)
                {
                    case typeParallax.BG:
                        transform.position = new Vector3(ParallaxManager.BG_bot.transform.position.x + (xSize * 2), transform.position.y);
                        ParallaxManager.BG_bot = this.transform;
                        break;

                    case typeParallax.Medio:
                        transform.position = new Vector3(ParallaxManager.Medio_bot.transform.position.x + (xSize * 2), transform.position.y);
                        ParallaxManager.Medio_bot = this.transform;
                        break;

                    case typeParallax.Perto:
                        transform.position = new Vector3(ParallaxManager.Perto_bot.transform.position.x + (xSize * 2), transform.position.y);
                        ParallaxManager.Perto_bot = this.transform;
                        break;
                }

        }
           
    }

}
