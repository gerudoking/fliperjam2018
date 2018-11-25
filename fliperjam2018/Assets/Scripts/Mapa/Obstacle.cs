using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MapObject {

    private void Update()
    {
        this.rb.velocity = new Vector2(-CenarioManager.velocity, 0);
    }

}
