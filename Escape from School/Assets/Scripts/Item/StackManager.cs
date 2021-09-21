using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates and moves the stacks
//Added on animated character
public class StackManager : MonoBehaviour
{
    //Access other scripts
    GameManager gameManagerScript;
    PlayerController playerControllerScript;

    //public Transform stacksPrefab;
    public Transform characterPrefab;
    public GameObject stackPrefab;
    private float stackHeight;

    private float maxStackPositionX = 0.3f;//Leftest and rightest position

    private List<Transform> stackList = new List<Transform>();
    private float lastStackpositionY = 0f;

    private List<Transform> stackObjectPool = new List<Transform>();

    //stack generation probabilities per cent
    int[] stackProbability = { 70, 15, 10, 5 }; //none,one,two,three number of stacks
    public int[] StackProbability { set { stackProbability = value; } }

    private void Awake()
    {
        gameManagerScript = Camera.main.GetComponent<GameManager>();
        playerControllerScript = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        stackHeight= stackPrefab.transform.localScale.y;
    }
    private void OnTriggerEnter(Collider other)  //bunun altini ayri fonsiyon yazabilirsin
    {
        if (other.gameObject.CompareTag("Stack"))//buna gerek varmi,
        {
            stackList.Add(other.transform);

            other.GetComponent<BoxCollider>().enabled = false;

            //float stackHeight= other.transform.localScale.y;
            if (lastStackpositionY == 0)
            {
                lastStackpositionY += stackHeight/2;
            }
            else
            {
                lastStackpositionY += stackHeight;
            }
            


            characterPrefab.localPosition = new Vector3(characterPrefab.localPosition.x, characterPrefab.localPosition.y + stackHeight, characterPrefab.localPosition.z);

            other.transform.parent = characterPrefab;
            //other.transform.localPosition = new Vector3(characterPrefab.transform.localPosition.x, lastStackpositionY, characterPrefab.localPosition.z);
            other.transform.localPosition = new Vector3(characterPrefab.transform.localPosition.x, other.transform.localPosition.y, characterPrefab.localPosition.z);

            gameManagerScript.IncreaseScore(1);
        }
    }

    public void SpaceTriggered(float posX, float posZ)
    {
        if (stackList.Count == 0)
        {
            //burada ol, eller havada utuma duserken animasyon ekleyebilirsin
            playerControllerScript.KillCharacter();

        }
        else
        {
            Transform lastStack = stackList[stackList.Count - 1];
            lastStack.transform.parent = null;
            stackList.RemoveAt(stackList.Count - 1);

            //float stackHeight = lastStack.localScale.y;
            lastStack.position = new Vector3(posX,-(stackHeight/2),posZ);
            characterPrefab.localPosition = new Vector3(characterPrefab.localPosition.x, characterPrefab.localPosition.y - stackHeight, characterPrefab.localPosition.z);
        }
    }

    public void MoveStacktoPool(Transform stack)
    {
        stackObjectPool.Add(stack);
    }
    public void GenerateStacks(Transform floor)
    {
        int numberOfStacks = RandomNumberOfStacks();

        List<int> ways = new List<int>() { -1, 0, 1 };
        int totalWay = ways.Count;

        //eliminating
        for (int i = 0; i < (totalWay - numberOfStacks); i++)
        {
            int eliminateIndex = Random.Range(0, ways.Count);
            ways.RemoveAt(eliminateIndex);
        }

        //generating
        for (int i = 0; i < ways.Count; i++)
        {
            SelectGeneratingType(ways[i] * maxStackPositionX, floor.position.z);
        }

    }
    private void SelectGeneratingType(float posX, float posZ)
    {
        if (stackObjectPool.Count == 0)
        {
            SpawnStack(posX, posZ);
        }
        else
        {
            MoveStack(posX, posZ);
        }
    }
  
    private void SpawnStack(float posX,float posZ)
    {
        Vector3 stackPosition = new Vector3(posX, stackHeight / 2f, posZ);
        GameObject stack = Instantiate(stackPrefab, stackPosition, Quaternion.identity);
    }
    private void MoveStack(float posX, float posZ)
    {
        Transform stack = stackObjectPool[0];
        stackObjectPool.RemoveAt(0);
        stack.position = new Vector3(posX, stackHeight / 2f, posZ);
        stack.GetComponent<BoxCollider>().enabled = true;
    }
    private int RandomNumberOfStacks()
    {
        int numberOfStackProbability = Random.Range(1, 101);
        if (numberOfStackProbability <= stackProbability[0])
        {
            return 0;
        }
        else if (numberOfStackProbability <= stackProbability[0]+stackProbability[1])
        {
            return 1;
        }
        else if (numberOfStackProbability <= stackProbability[0]+ stackProbability[1]+stackProbability[2])
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
