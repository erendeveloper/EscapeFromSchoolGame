using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enable player to rotate for surfing effect
public class RotatePlayer : MonoBehaviour
{
    PlayerController playerControllerScript;

    private void Awake()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerControllerScript.EnableSkiingAnimation();
    }
}
