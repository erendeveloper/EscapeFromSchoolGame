using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on floor Prefab Box Collider
//Floor Object Pooling

public class FloorPoolTrigger : MonoBehaviour
{
    //Access the controller
    private RoadsController roadsControllerScript;

    private void Awake()
    {
        roadsControllerScript = GameObject.FindGameObjectWithTag("Roads").GetComponent<RoadsController>();
    }

    private void OnTriggerEnter(Collider other)//Player ('Collider Swerve' is inside of that) triggers
    {
        roadsControllerScript.MoveFloorToPool(this.transform.parent);
    }
}
