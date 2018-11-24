using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenarioManager : MonoBehaviour {

    public static float velocity;
    public float _velocity;
    public float velocityGain;

    public float TimeToGainSpeed = 0.2f;

    public Transform _lastPlat;
    public static Transform lastPlat;

    public void Start()
    {
        lastPlat = _lastPlat;
        velocity = _velocity;
        StartCoroutine(AddVelocity());
    }

    public IEnumerator AddVelocity()
    {
        yield return new WaitForSeconds(TimeToGainSpeed);
        velocity += velocityGain;
        _velocity = velocity; //apenas para ver
        StartCoroutine(AddVelocity());
    }



}
