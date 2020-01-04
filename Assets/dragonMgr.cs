using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonMgr : MonoBehaviour
{
    readonly float LeftBorfer = -3;
    readonly float RightBorfer = 3;
    readonly float UpBorfer = 6.25f;
    readonly float DownBorfer = 5.5f;
    readonly float initPosX = 0.5f;
    readonly int MAX_DRAGON_COUNT = 25;
    readonly Color[] colors={Color.white,Color.green,Color.red,Color.yellow,Color.gray};
    private SpriteRenderer mySpriteRenderer;

    public List<Transform> dragons;
    // Start is called before the first frame update
    void Start()
    {
        
        dragons = new List<Transform>();
        for (int i = 0; i < MAX_DRAGON_COUNT; i++)
        {
            SpawnDragon();
        }
    }

    private void SpawnDragon()
    {
        GameObject newDragon = Instantiate(Resources.Load<GameObject>("dragon"));
        newDragon.transform.position = new Vector2(NewDragonPosX(), NewDragonPosY());
        mySpriteRenderer = newDragon.GetComponent<SpriteRenderer>();
        mySpriteRenderer.flipX = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
        mySpriteRenderer.color = colors[UnityEngine.Random.Range(0,5)];
        dragons.Add(newDragon.transform);
    }
    private float NewDragonPosX()
    {
        if (dragons.Count == 0)
        {
            return initPosX;
        }
        return UnityEngine.Random.Range(LeftBorfer, RightBorfer);
    }
    private float NewDragonPosY()
    {
        if (dragons.Count == 0)
        {
            return 5.5f;
        }
        int lowerIndex = dragons.Count - 1;
        return UnityEngine.Random.Range(DownBorfer, UpBorfer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
