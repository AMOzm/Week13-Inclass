using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]private GameManager gm;

    //Control for player movement
    //private Vector2 PlayerInput;
    [SerializeField]private Vector2 playerMove;
    [SerializeField] private float moveSpd;

    //reference to the player and the position of the player object
    private GameObject player;
    private Transform playerTransform;
    private Vector2 playerPos;

    //Bounds of map
    [SerializeField]private float leftBarrier;
    [SerializeField]private float rightBarrier;
    [SerializeField]private float upBarrier;
    [SerializeField]private float downBarrier;

    //to animate playermovement and idle stage
    [SerializeField]private Animator playerAnimator;
    private float HorizontalMovement;
    private float VerticalMovement;
    private bool mUp;
    private bool mDown;
    private bool mLeft;
    private bool mRight;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform;
        playerPos = gameObject.transform.position;
        //Debug.Log(playerPos);
    }

    // Update is called once per frame
    // DeltaTime
    void Update()
    {
        GetPlayerInput();
        UpdateAnimator();
        //get playerpos + get player movement direction
        gameObject.transform.position = playerPos;
        // HorizontalMovement = Input.GetAxis("Horizontal");
        // VerticalMovement = Input.GetAxis("Vertical");
        // animator.SetFloat("XDirection", HorizontalMovement);
        // animator.SetFloat("YDirection", VerticalMovement);
    }

    void GetPlayerInput(){
          playerMove = Vector2.zero;
            bool moveLeft = Input.GetKey(KeyCode.A) && playerTransform.position.x > leftBarrier;
            bool moveRight = Input.GetKey(KeyCode.D) && playerTransform.position.x < rightBarrier;
            bool moveUp = Input.GetKey(KeyCode.W) && playerTransform.position.y < upBarrier;
            bool moveDown = Input.GetKey(KeyCode.S) && playerTransform.position.y > downBarrier;

            if (moveLeft && moveRight) moveLeft = moveRight = false; //cancels horizontal movement
            if (moveUp && moveDown) moveUp = moveDown = false; //cancels vertical movement



        // Check for input and update movement vector
        if (moveLeft)
        {
            playerMove.x = -1;
            if(moveUp){
                playerAnimator.Play("KiUp");
                mUp = true;
            }
            else if(moveDown){
                playerAnimator.Play("KiDown");
                mDown = true;
            }
            else{
                playerAnimator.Play("KiLeft");
                mLeft = true;
            }
        }
        if (moveRight)
        {
            playerMove.x = 1;
            if(moveUp){
                playerAnimator.Play("KiUp");
                mUp = true;
            }
            else if(moveDown){
                playerAnimator.Play("KiDown");
                mDown = true;
            }
            else{
            playerAnimator.Play("KiRight");
            mRight = true;
            }
        }
        if (moveUp)
        {
            playerMove.y = 1;
            playerAnimator.Play("KiUp");
            mUp = true;
        }
        if (moveDown)
        {
            playerMove.y = -1;
            playerAnimator.Play("KiDown");
            mDown = true;
        }
        // else{
        //     playerMove.x = 0;
        //     playerMove.y = 0;
        // }
        // if(playerMove == Vector2.zero) playerAnimator.Play("KiIdle");

        // Normalize the movement vector to avoid faster diagonal movement
        playerMove.Normalize();

        // Apply the movement
        playerPos = playerTransform.position + (Vector3)(playerMove * moveSpd * Time.deltaTime);
        playerTransform.position = playerPos;

    }
    void UpdateAnimator()
    {
        if (playerMove == Vector2.zero && mDown)
        {
        // No movement input, set to idle with down sprite
            playerAnimator.Play("KiIdle");
            mDown = false;
        }
        if (playerMove == Vector2.zero && mUp)
        {
        // No movement input, set to idle with up sprite
            playerAnimator.Play("KiIdleUp");
            mUp = false;
        }
        if (playerMove == Vector2.zero && mLeft)
        {
        // No movement input, set to idle with left sprite
            playerAnimator.Play("KiIdleLeft");
            mLeft = false;
        }
        if (playerMove == Vector2.zero && mRight)
        {
        // No movement input, set to idle with right sprite
            playerAnimator.Play("KiIdleRight");
            mRight = false;
        }
    }
}
