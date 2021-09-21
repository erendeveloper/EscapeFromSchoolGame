using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Player prefab
//Works with Swipe.cs and Swerve.cs
public class PlayerController : MonoBehaviour
{
    //Access other scripts
    GameManager gameManagerScript;

    private bool isPlayerAlive = true;
    private bool isRotating = false; //surf effect

    public Transform bodyHorizontal; //player body for horizontal movement

    public Transform characterPrefab;
    public Animator playerAnimator;

    private float skiingRotationDegree=50f;
    private Quaternion skiingRotation;
    private float rotationSpeed = 0.4f;

    //for movements of body parts of the player
    private Vector3 targetPositionX = Vector3.zero;

    private float speedForward = 1f;
    public float SpeedForward { set { speedForward = value; } }

    private float smoothDampTimeX = 0.1f;
    public float SmoothDampTimeX { set { smoothDampTimeX /= value; } }

    //reference velocities for SmoothDamp
    private Vector3 smoothDampVelocityX = Vector3.zero;
    private Vector3 smoothDampVelocityY = Vector3.zero;

    private const float MaxHorizontalDistance = 0.3f;//Leftest and rightest distance from the middle //burasi ile roads kontroldeki distance ayni


    private void Awake()
    {
        gameManagerScript = Camera.main.GetComponent<GameManager>();
        AssignSkiingRotationWay();
    }

    void FixedUpdate()
    {
        if (isPlayerAlive)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speedForward);
            MoveHorizontally();
        }
        if (isRotating)
        {
            characterPrefab.localRotation = Quaternion.Lerp(Quaternion.identity, skiingRotation, Time.time * rotationSpeed);
            if (Quaternion.Dot(characterPrefab.localRotation, skiingRotation) >= 1f)
            {
                isRotating = false;
            }
        }
    }

    private void MoveHorizontally()
    {
        bodyHorizontal.localPosition = Vector3.SmoothDamp(bodyHorizontal.localPosition, targetPositionX, ref smoothDampVelocityX, smoothDampTimeX);

        if (Vector3.Distance(bodyHorizontal.localPosition, targetPositionX) < 0.01f)
        {
            bodyHorizontal.localPosition = targetPositionX;
        }
    }
    public void AssignTargetPositionX(float targetVectorX)//input comes from Serve.cs
    {
        targetPositionX.x = Mathf.Clamp(targetVectorX, -MaxHorizontalDistance, MaxHorizontalDistance);
    }

    public void EnableSkiingAnimation()
    {
        playerAnimator.SetTrigger("Turn");
    }
    public void EnableRotation()
    {
        isRotating = true;
    }
    
    private void AssignSkiingRotationWay()
    {
        int random = Random.Range(1, 3);
        if (random == 1)
        {
            skiingRotationDegree *= -1f;
        }
        skiingRotation= Quaternion.Euler(0, skiingRotationDegree, 0);
    }
    public void KillCharacter()//character falls down
    {
        isPlayerAlive = false;
        this.GetComponent<Swerve>().enabled = false;
        Camera.main.transform.parent = null;
        playerAnimator.transform.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        playerAnimator.SetTrigger("Die");
        Invoke("CharacterKilled", 1.0f);//waiting for fallin  down
    }
    private void CharacterKilled()
    {
        gameManagerScript.Die();
    }
}
