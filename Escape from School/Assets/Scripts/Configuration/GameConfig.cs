using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Game configuration for Debug on mobile
public class GameConfig : MonoBehaviour
{


    public static float characterForwardSpeed = 1f;
    public static float characterHorizontalSpeed = 1f;
    public static Vector3 cameraLocalPosition = new Vector3(0, 0.57f, -1.71f);
    public static int[] stackProbability = { 70, 15, 10, 5 }; //none,one,two,three number of stacks

    public static void DefaultValues()
    {
        characterForwardSpeed = 1f;
        characterHorizontalSpeed = 1f;
        cameraLocalPosition = new Vector3(0, 0.57f, -1.71f);
        stackProbability = new int[]{70, 15, 10, 5 }; //none,one,two,three number of stacks
    }

}
