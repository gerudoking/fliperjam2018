using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invert : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Inverter("camp2");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Inverter(string playerTarget)
    {
        if(playerTarget == this.tag) //camp1 ou camp2
        {
            var ca = GetComponent<Camera>();
            Matrix4x4 mat = ca.projectionMatrix;
            mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
            ca.projectionMatrix = mat;
        }
    }

}
