using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Stack Prefab
//Stack Prefab Object Pooling
public class StackPoolTrigger : MonoBehaviour
{
    private StackManager stackManagerScript;

    private void Awake()
    {
        stackManagerScript = GameObject.FindGameObjectWithTag("Character").GetComponent<StackManager>();
    }
    private void OnTriggerEnter(Collider other)//Player ('Collider Swerve' is inside of that) triggers
    {
        stackManagerScript.MoveStacktoPool(this.transform.parent);
    }
}
