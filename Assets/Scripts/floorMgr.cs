using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floorMgr : MonoBehaviour
{

    readonly float LeftBorfer = -3;
    readonly float RightBorfer = 3;
    readonly float initPosY = 0;
    readonly int MAX_FLOOR_COUNT = 10;
    readonly int MAX_TREE_COUNT = 2;
    readonly int MIN_FLOOR_COUNT_UNDER_PLAYER = 3;
    readonly int MIN_TREE_COUNT_UNDER_PLAYER = 1;
    readonly float TREE_DISAPPEAR_CORRECTION_LONG = 1;
    static int floorNum = -1;
    [Range(2, 6)] public float spacingY;
     float spacingTreeY=17f;
    [Range(1, 20)] public float singleFloorHight;
    public List<Transform> floors;
    public List<Transform> trees;
    public Transform player;
    public Text displayCountFloor;
    void Start()
    {
        floors = new List<Transform>();
        trees = new List<Transform>();
        for (int i = 0; i < MAX_FLOOR_COUNT; i++)
        {
            SpawnFloor();
        }
        for (int i = 0; i < MAX_TREE_COUNT; i++)
        {
            SpawnTree();
        }
        
    }
    public void ControlSpawnFloor()
    {
        int floorsCountUnder = 0;
        foreach (Transform floor in floors)
        {
            if (floor.position.y < player.transform.position.y)
            {
                floorsCountUnder++;
            }
        }
        if (floorsCountUnder < MIN_FLOOR_COUNT_UNDER_PLAYER)
        {
            SpawnFloor();
            ControlFloorCount();
        }
    }
    void ControlFloorCount()
    {
        if (floors.Count > MAX_FLOOR_COUNT)
        {
            Destroy(floors[0].gameObject);
            floors.RemoveAt(0);
        }
    }

    public void ControlSpawnTree()
    {
        int treesCountUnder = 0;
        foreach (Transform tree in trees)
        {
            if (tree.position.y- TREE_DISAPPEAR_CORRECTION_LONG < Camera.main.transform.position.y)
            {
                treesCountUnder++;
            }
        }
        if (treesCountUnder < MIN_TREE_COUNT_UNDER_PLAYER)
        {
            SpawnTree();
            ControlTreeCount();
        }
    }
    void ControlTreeCount()
    {
        if (trees.Count > MAX_TREE_COUNT)
        {
            Destroy(trees[0].gameObject);
            trees.RemoveAt(0);
        }
    }


    float NewFloorPosX()
    {
        if (floors.Count == 0)
        {
            return 0;
        }
        return Random.Range(LeftBorfer, RightBorfer);
    }

    float NewFloorPosY()
    {
        if (floors.Count == 0)
        {
            return initPosY;
        }
        int lowerIndex = floors.Count - 1;
        return floors[lowerIndex].transform.position.y - spacingY;
    }


    float NewTreePosX()
    {
        return -0.65f;
    }

    float NewTreePosY()
    {
        if (trees.Count == 0)
        {
            return 2f;
        }
        int lowerIndex = trees.Count - 1;
        return trees[lowerIndex].transform.position.y - spacingTreeY;
    }
    void SpawnFloor()
    {
        GameObject newFloor = Instantiate(Resources.Load<GameObject>("floor"));
        newFloor.transform.position = new Vector2(NewFloorPosX(), NewFloorPosY());
        if (newFloor.transform.position.x < 0)
        {
            newFloor.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        floors.Add(newFloor.transform);
    }
    void SpawnTree()
    {
        GameObject newTree = Instantiate(Resources.Load<GameObject>("tree"));
        newTree.transform.position = new Vector2(NewTreePosX(), NewTreePosY());
        trees.Add(newTree.transform);
    }
    float CountLoeweFloor() {
        float playerPosY = player.transform.position.y;
        float deep = Mathf.Abs(initPosY - playerPosY);
        return (deep / spacingY)+1;
    }
    void DisplayCountFloor() {
        displayCountFloor.text = "往下了" + CountLoeweFloor().ToString("0000") + "尺";
    }
    void Update()
    {
        ControlSpawnFloor();
        ControlSpawnTree();
        DisplayCountFloor();
    }
}
