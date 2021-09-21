using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on space box collider
//Space Prefab Object Pooling
public class SpacePoolTrigger : MonoBehaviour
{
    //Access the controller
    private RoadsController roadsControllerScript;

    private void Awake()
    {
        roadsControllerScript = GameObject.FindGameObjectWithTag("Roads").GetComponent<RoadsController>();
    }

    private void OnTriggerEnter(Collider other)//Player ('Collider Swerve' is inside of that) triggers
    {
        roadsControllerScript.MoveSpaceToPool(this.transform.parent);
    }
}
