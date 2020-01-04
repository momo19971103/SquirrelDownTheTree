using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D playrigidbody2D;
    [Header("目前水平速度")] public float speedX;
    [Header("目前水平方向")] public float horizontalDirection;

    const string HORIZONAL = "Horizontal";

    [Header("水平推力")] [Range(0, 150)] public float xForce;

    float speedY;

    void Start()
    {
        playrigidbody2D = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// 水平速度
    /// </summary>
    void MovementX() {
        horizontalDirection = Input.GetAxis(HORIZONAL);
        playrigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
    }
    // Update is called once per frame
    void Update()
    {
        MovementX();
        speedX = playrigidbody2D.velocity.x;
    }
}
