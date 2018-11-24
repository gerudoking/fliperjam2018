using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sombra : MonoBehaviour {

    [SerializeField]
    private bool isPlayer1;
    public Vector2 Offset;
    private GameObject playerTarget;

	// Use this for initialization
	void Start () {
        Invoke("SetPlayer", 1);
	}
	
    void SetPlayer()
    {
        if (isPlayer1)
            playerTarget = CenarioManager.p1;
        else
            playerTarget = CenarioManager.p2;
    }

	// Update is called once per frame
	void Update () {
        LockSombra();
	}

    private void LockSombra()
    {
        if (playerTarget == null)
            return;

        if(playerTarget.GetComponent<PlayerMove>().IsJumping)
            transform.position = new Vector3(playerTarget.transform.position.x + Offset.x, transform.position.y);
        else
            transform.position = new Vector3(playerTarget.transform.position.x + Offset.x, playerTarget.transform.position.y + Offset.y);

    }

}
