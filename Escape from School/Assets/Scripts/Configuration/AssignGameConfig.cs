using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Main Camera in Game Scene
//Assigns Debug configurations
public class AssignGameConfig : MonoBehaviour
{
    void Awake()
    {
        AssignValues();
    }

    public void AssignValues()
    {
        GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>().SpeedForward=GameConfig.characterForwardSpeed;
        GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>().SmoothDampTimeX = GameConfig.characterHorizontalSpeed;
        Camera.main.transform.localPosition = GameConfig.cameraLocalPosition;
        GameObject.FindGameObjectWithTag("Character").GetComponent<StackManager>().StackProbability = GameConfig.stackProbability;

    }
}
