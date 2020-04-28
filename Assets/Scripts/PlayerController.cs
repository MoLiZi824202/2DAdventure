using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    

    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private BoxCollider2D playerFeet;
    private bool isGround;
    private bool JumpStar;
    private bool canDoubleJump;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerFeet = GetComponent<BoxCollider2D>();
        JumpStar = false;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();
        Jump();
        CheckGround();
        SwitchAnimation();
        //Skill();
    }
    void CheckGround()
    {
        isGround = playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (playerRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (playerRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed,playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVel;
        bool playerHasXAxisSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("Run", playerHasXAxisSpeed);
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                playerRigidbody.velocity = Vector2.up * jumpVel;
                playerAnimator.SetBool("Jump", true);
                JumpStar = true;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed);
                    playerRigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
    }

    //void Skill()
    //{
    //    if (Input.GetButtonDown("Skill1"))
    //    {
    //        playerAnimator.SetTrigger("Skill1");
    //    }
    //}

    void SwitchAnimation()
    {
        if (!isGround)
        {
            JumpStar = false;
        }
        if(isGround&& !JumpStar)
        {
            playerAnimator.SetBool("Jump", false);
            playerAnimator.SetBool("Idle", true);
        }
    }
}
