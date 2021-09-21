using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on floor prefab
public class Floor : MonoBehaviour
{
    //Access other script
    private StackManager stackManagerScript;

    private void Awake()
    {
        stackManagerScript = GameObject.FindGameObjectWithTag("Character").GetComponent<StackManager>();
    }
    private void Start()
    {
        GenerateStackOnFloor();
    }
    public void GenerateStackOnFloor()
    {
        stackManagerScript.GenerateStacks(this.transform);
    }
}
