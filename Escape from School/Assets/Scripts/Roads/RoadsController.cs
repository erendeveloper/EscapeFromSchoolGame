using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Roads Prefab
//After player pass on the road, it moves to the end
public class RoadsController : MonoBehaviour
{
    //parents
    public Transform floors;
    public Transform spaces;
    public Transform platformEnds;

    private float floorSizeY;  
    private float floorSizeZ;
    private float spaceSizeY;
    private float spaceSizeZ;

    public GameObject floorPrefab;
    public GameObject spacePrefab;
    public GameObject platformEndPrefab;//Trigger collider,after passing, road moves
    public Transform stackPrefab;
    public Transform startLine;

    private float lastPositionZ;
    
    //platform specifications
    private int numberOfPlatforms = 5;//each platform consist of floors and spaces
    private int minFloors = 3;
    private int maxFloors = 5;
    private int minSpaces = 1;
    private int maxSpaces = 3;

    //object pools
    private List<Transform> floorObjectPool = new List<Transform>();
    private List<Transform> spaceObjectPool = new List<Transform>();
    private List<Transform> platformEndObjectPool = new List<Transform>();

    //properties
    private Vector3 FloorPosition { get => new Vector3(0, -(floorSizeY / 2f), lastPositionZ + (floorSizeZ / 2f)); }
    private Vector3 SpacePosition { get => new Vector3(0, -(spaceSizeY / 2f), lastPositionZ + (spaceSizeZ / 2f)); }
    private Vector3 PlatformEndPosition { get => new Vector3(0, 0, lastPositionZ); }

    private void Awake()
    {
        AssignTransformValues();//floor and space sizes
        GenerateFirstPlarforms();
    }

    #region spawn
    private void SpawnFloor()
    {    
        GameObject floor = Instantiate(floorPrefab, FloorPosition,Quaternion.identity);
        floor.transform.parent = floors.transform;
        //lastPositionZ += floorSizeZ;
    }
    private void SpawnSpace()
    {
        GameObject space = Instantiate(spacePrefab, SpacePosition, Quaternion.identity); ;
        space.transform.parent = spaces.transform;
        //lastPositionZ += spaceSizeZ;
    }
    private void SpawnPlatformEnd()
    {
        GameObject platformEnd = Instantiate(platformEndPrefab, PlatformEndPosition, Quaternion.identity);
        platformEnd.transform.parent = platformEnds;
    }
    #endregion

    #region random
    private int RandomNumberOfFloors()
    {
        return Random.Range(minFloors,maxFloors+1);
    }
    private int RandomNumberOfSpaces()
    {
        return Random.Range(minSpaces, maxSpaces + 1);
    }
    #endregion
    private void GenerateFirstPlarforms()
    {       
        for (int platform=0; platform<numberOfPlatforms;platform++)
        {
            GeneratePlatform();
        }
    }
    #region generating
    public void GeneratePlatform()//one platform
    {
        int numberOfFloors = RandomNumberOfFloors();
        for (int floor = 0; floor < numberOfFloors; floor++)
        {
            GenerateFloor();
        }
        int numberOfSpaces = RandomNumberOfSpaces();
        for (int space = 0; space < numberOfSpaces; space++)
        {
            GenerateSpace();
        }
        GeneratePlatformEnd();
    }
    private void GenerateFloor()
    {
        if (floorObjectPool.Count == 0)
        {
            SpawnFloor();
        }
        else
        {
            MoveFloor();
        }
        lastPositionZ += floorSizeZ;
    }

    private void GenerateSpace()
    {
        if (spaceObjectPool.Count == 0)
        {
            SpawnSpace();
        }
        else
        {
            MoveSpace();
        }
        lastPositionZ += spaceSizeZ;
    }
    private void GeneratePlatformEnd()
    {
        if (platformEndObjectPool.Count == 0)
        {
            SpawnPlatformEnd();
        }
        else
        {
            MovePlatformEnd();
        }
    }
    #endregion

    #region moving
    private void MoveFloor()
    {
        Transform floor = floorObjectPool[0];
        floorObjectPool.RemoveAt(0);
        floor.position = FloorPosition;
        floor.GetComponent<Floor>().GenerateStackOnFloor();
        //lastPositionZ += floorSizeZ;
    }
    private void MoveSpace()
    {
        Transform space = spaceObjectPool[0];
        spaceObjectPool.RemoveAt(0);
        space.position = SpacePosition;
        //lastPositionZ += spaceSizeZ;
    }
    private void MovePlatformEnd()
    {
        Transform platformEnd = platformEndObjectPool[0];
        platformEndObjectPool.RemoveAt(0);
        platformEnd.localPosition = PlatformEndPosition;
    }
    #endregion

    #region object pooling
    public void MoveFloorToPool(Transform floor)
    {
        floorObjectPool.Add(floor);
    }
    public void MoveSpaceToPool(Transform space)
    {
        spaceObjectPool.Add(space);
    }
    public void MovePlatformEndToPool(Transform platformEnd)
    {
        platformEndObjectPool.Add(platformEnd);
    }
    #endregion
    private void AssignTransformValues()//floor and space sizes
    {
        lastPositionZ = startLine.position.z + startLine.localScale.z * 5;// 10/2=5, plane

        floorSizeY = floorPrefab.transform.localScale.y;
        floorSizeZ = floorPrefab.transform.localScale.z;
        spaceSizeY = stackPrefab.transform.localScale.y;
        spaceSizeZ = stackPrefab.transform.localScale.z;
    }
}
