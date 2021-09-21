using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves floors and spaces
public class RoadMove : MonoBehaviour
{
    //Access the controller
    private RoadsController roadsControllerScript;

    private void Awake()
    {
        roadsControllerScript = GameObject.FindGameObjectWithTag("Roads").GetComponent<RoadsController>();
    }

    private void OnTriggerEnter(Collider other)//Player ('Collider Swerve' is inside of that) triggers
    {
        roadsControllerScript.MovePlatformEndToPool(this.transform.parent);
        roadsControllerScript.GeneratePlatform();       
    }
}
