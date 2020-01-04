using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMgr : MonoBehaviour
{
    private float forceX = 15f;
    public static bool isDead;
    public static bool isStand;
    Rigidbody2D playRigidbody2D;
    CircleCollider2D circleCollider2D;
    private SpriteRenderer mySpriteRenderer;
    public static Animator animator;
    readonly float toLeft = -1;
    readonly float toRight = 1;
    readonly float stop = 0;
    readonly float down = 2;
    float directionX;
    private bool FirstLanding = true;
    private bool FirstInTheAir = true;
    private bool DeadYet = false;
    private bool FirstMove = true;
    private bool FirstSquat = true;



    [Header("感應地板的距離")]
    [Range(0, 0.5f)]
    public float distance;
    [Header("地面感測射線起點")]
    public Transform groundCheck;
    [Header("地面圖層")]
    public LayerMask groundLayer;
    public bool grounded;
    public AudioMgr audioMgr;

    void Start()
    {
        isDead = false;
        playRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    bool IsGround
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            grounded = Physics2D.Linecast(start, end, groundLayer);
            return grounded;
        }
    }


    // Update is called once per frame
    void Update()
    {
        int STATUS = 5;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            directionX = toLeft;
            STATUS = (int)toLeft;
            Debug.Log("getLeft");
            mySpriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            directionX = toRight;
            STATUS = (int)toRight;
            Debug.Log("getRight");
            mySpriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ResetAnimation();

            Debug.Log("123");
            STATUS = (int)down;
            StartCoroutine("delayIsTrigger");
        }
        else
        {
            directionX = stop;
            STATUS = (int)stop;
        }
        AnimationStyle(STATUS);
        Vector2 newDirection = new Vector2(directionX, 0);
        playRigidbody2D.AddForce(newDirection * forceX);
        getV();
        if (isDead)
        {
            if (!DeadYet)
            {
                DeadYet = true;
                audioMgr.Play(AudioMgr.eType.DEAD);
            }

        }
    }
    void getV()
    {

        string msg =
      $"{animator.GetBool("Dead")}  {animator.GetBool("Stand")}  {animator.GetBool("InTheAir")}{animator.GetBool("Move")}{animator.GetBool("Squat")}";
        Debug.Log(msg);
    }
    IEnumerator delayIsTrigger()
    {

        animator.SetBool("Squat", true);
        yield return new WaitForSeconds(0.25f);
        if ((Input.GetKey(KeyCode.DownArrow)))
        {
            circleCollider2D.isTrigger = true;
        }
        yield return new WaitForSeconds(0.5f);
        circleCollider2D.isTrigger = false;
        FirstSquat = true;
    }
    IEnumerator delayStartUpMoveSound()
    {


        yield return new WaitForSeconds(0.3f);
        FirstMove = true;
    }
    void AnimationStyle(int STATUS)
    {
        if (!DeadYet)
        {
            if (IsGround)
            {
                FirstInTheAir = true;
                if (FirstLanding)
                {
                    audioMgr.Play(AudioMgr.eType.LANDING);
                    FirstLanding = false;
                }
                if (STATUS == stop)
                {
                    ResetAnimation();
                    animator.SetBool("Stand", true);
                }
                else if (STATUS == down)
                {
                    ResetAnimation();
                    animator.SetBool("Squat", true);
                    if (FirstSquat)
                    {
                        FirstSquat = false;
                        audioMgr.Play(AudioMgr.eType.SQUAT);
                    }
                }
                else
                {
                    ResetAnimation();
                    animator.SetBool("Move", true);
                    if (FirstMove)
                    {
                        FirstMove = false;
                        audioMgr.Play(AudioMgr.eType.MOVE);
                        StartCoroutine("delayStartUpMoveSound");
                    }

                }
            }

            else
            {
                FirstLanding = true;
                ResetAnimation();
                animator.SetBool("InTheAir", true);
                if (FirstInTheAir)
                {
                    audioMgr.Play(AudioMgr.eType.INTHEAIR);
                    FirstInTheAir = false;
                }

                Debug.Log("InTheAir");
            }
        }
    }
    public static void ResetAnimation()
    {
        animator.SetBool("Stand", false);
        animator.SetBool("Move", false);
        animator.SetBool("InTheAir", false);
        animator.SetBool("Squat", false);
        animator.SetBool("Dead", false);
    }

}
