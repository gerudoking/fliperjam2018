using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenarioManager : MonoBehaviour {

    public static float velocity;
    public float _velocity;
    public float velocityGain;
    public float maxVelocity = 20;
    public float TimeToGainSpeed = 0.2f;

    public Transform _lastPlat;
    public static Transform lastTopPlat;

    public Transform _lastBotPlat;
    public static Transform lastBotPlat;

    public static GameObject p1; //player 1 
    public static GameObject p2; //player 2

    public void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");

        lastTopPlat = _lastPlat;
        lastBotPlat = _lastBotPlat;

        velocity = _velocity;
        StartCoroutine(AddVelocity());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
           SwapPlayers();
    }

    public void SwapPlayers()
    {
        StartCoroutine(coSwapPlayers());
    }

    IEnumerator coSwapPlayers()
    {
        var p1Move = p1.GetComponent<PlayerMove>();
        var p2Move = p2.GetComponent<PlayerMove>();

        var tempPos0_p2 = p2Move.GetLaneTransform(1);
        var tempPos1_p2 = p2Move.GetLaneTransform(0);
        var tempPos2_p2 = p2Move.GetLaneTransform(-1);

        p2Move.SetLaneTransform(p1Move.GetLaneTransform(1), 1);
        p2Move.SetLaneTransform(p1Move.GetLaneTransform(0), 0);
        p2Move.SetLaneTransform(p1Move.GetLaneTransform(-1), -1);

        yield return null;

        p1Move.SetLaneTransform(tempPos0_p2, 1);
        p1Move.SetLaneTransform(tempPos1_p2, 0);
        p1Move.SetLaneTransform(tempPos2_p2, -1);

        p1.transform.position = new Vector3(p1.transform.position.x, p1Move.GetLanePos(0));
        p2.transform.position = new Vector3(p2.transform.position.x, p2Move.GetLanePos(0));

        p1Move.thisItemManager.AtualizeValues();
        p2Move.thisItemManager.AtualizeValues();
    }

    public IEnumerator AddVelocity()
    {
        yield return new WaitForSeconds(TimeToGainSpeed);
        velocity += velocityGain;
        _velocity = velocity; //apenas para ver

        if (velocity<maxVelocity)
            StartCoroutine(AddVelocity());
    }



}
