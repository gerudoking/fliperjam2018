using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour {

	    private float speed;
		private Rigidbody2D rb;
	    [SerializeField]
	    protected int lane;
		public bool player;

		public int Lane{
			get{
				return lane;
			}
		}

		// Use this for initialization
		void Start () {
			rb = GetComponent<Rigidbody2D>();

	        //Agora tá pegando a velocidade do cenario
	        speed = -CenarioManager.velocity; //-4;

			rb.velocity = new Vector2(speed, 0);
		}

	    public void setLane(int lane)
	    {
	        this.lane = lane;
	    }

	    private void OnTriggerEnter2D(Collider2D collision)
	    {
	        if (collision.tag == "end")
	            Destroy(this.gameObject);
	    }
}
