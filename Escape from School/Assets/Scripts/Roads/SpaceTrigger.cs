using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trigger on space
public class SpaceTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
           other.GetComponent<StackManager>().SpaceTriggered(other.transform.position.x,transform.position.z);
    }
}
