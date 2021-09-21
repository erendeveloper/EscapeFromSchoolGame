using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//UI Controller in Home Scene
//Added on Canvas
public class HomeUIController : MonoBehaviour
{
    public GameObject settingsPanel;

    public InputField forwardSpeedField;
    public InputField horizontalSpeedField;
    public InputField cameraPositionXField;
    public InputField cameraPositionYField;
    public InputField cameraPositionZField;
    public InputField stackProbability0Field;
    public InputField stackProbability1Field;
    public InputField stackProbability2Field;
    public InputField stackProbability3Field;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Back()
    {
        settingsPanel.SetActive(false);
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void ApplySettings()
    {
        string forwardSpeed=forwardSpeedField.text;
        if (forwardSpeed != "")
        {
            GameConfig.characterForwardSpeed = float.Parse(forwardSpeed);
        }

        string horizontalSpeed = horizontalSpeedField.text;
        if (horizontalSpeed != "")
        {
            GameConfig.characterHorizontalSpeed = float.Parse(horizontalSpeed);
        }

        string cameraX = cameraPositionXField.text;//BUNLARIN EKSISINE BAK
        if (cameraX != "")
        {
            float posX;
            if (cameraX.Contains("-"))
            {
                posX = float.Parse(cameraX.Substring(1)) *-1;
;            }
            else
            {
                posX = float.Parse(cameraX);
            }
            GameConfig.cameraLocalPosition.x = posX;
        }
        string cameraY = cameraPositionYField.text;
        if (cameraY != "")
        {
            float posY;
            if (cameraY.Contains("-"))
            {
                posY = float.Parse(cameraY.Substring(1)) * -1;                
            }
            else
            {
                posY = float.Parse(cameraY);
            }
            GameConfig.cameraLocalPosition.y = posY;
        }
        string cameraZ = cameraPositionZField.text;
        if (cameraZ != "")
        {
            float posZ;
            if (cameraZ.Contains("-"))
            {
                posZ = float.Parse(cameraZ.Substring(1)) *-1;
;            }
            else
            {
                posZ = float.Parse(cameraZ);
            }
            GameConfig.cameraLocalPosition.z = posZ;
        }

        string probability0 = stackProbability0Field.text;
        string probability1 = stackProbability1Field.text;
        string probability2 = stackProbability2Field.text;
        string probability3 = stackProbability3Field.text;
        if (probability0!="" && probability1 != "" && probability2 != "" && probability3 != "")
        {
            GameConfig.stackProbability = new int[] { 
                int.Parse(probability0),
                int.Parse(probability1),
                int.Parse(probability2),
                int.Parse(probability3),
            };
        }
        
    }
    public void ResetSettings()
    {
        GameConfig.DefaultValues();
    }
}
