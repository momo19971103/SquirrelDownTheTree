using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMoveMgr : MonoBehaviour
{
    // Start is called before the first frame update
    readonly float LeftBorfer = -3;
    readonly float RightBorfer = 3;
    float UpBorfer = 6.5f;
    float DownBorfer = 5.5f;
    public float downSpeed = 1;
    public float dragonSpeed = 1;
    private SpriteRenderer mySpriteRenderer;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        if (mySpriteRenderer.flipX == true)
        {
            dragonSpeed = 1;
        }
        else {
            dragonSpeed = -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x > RightBorfer)
        {
            dragonSpeed = -1;
            mySpriteRenderer.flipX = false;
        }
        if (transform.position.x < LeftBorfer)
        {
            dragonSpeed = 1;
            mySpriteRenderer.flipX = true;
        }
        /*
        if (transform.position.y > UpBorfer)
        {
            downSpeed = -15;
        }
        if (transform.position.y < DownBorfer)
        {
            downSpeed = 15;
        }
        UpBorfer -= Time.deltaTime;
        DownBorfer -= Time.deltaTime;
        */
        transform.Translate(dragonSpeed * Time.deltaTime, -downSpeed * Time.deltaTime, 0);

    }
}
