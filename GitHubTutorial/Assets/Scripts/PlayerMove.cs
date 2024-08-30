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

    private GameObject player;
    private Transform playerTransform;
    private Vector2 playerPos;

    //Bounds of map
    [SerializeField]private float leftBarrier;
    [SerializeField]private float rightBarrier;
    [SerializeField]private float upBarrier;
    [SerializeField]private float downBarrier;

    //to animate playermovement
    [SerializeField]private Animator playerAnimator;
    private float HorizontalMovement;
    private float VerticalMovement;
    
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

        // Check for input and update movement vector
        if (Input.GetKey(KeyCode.A) && playerTransform.position.x > leftBarrier)
        {
            playerMove.x = -1;
            playerAnimator.Play("KiLeft");
        }
        if (Input.GetKey(KeyCode.D) && playerTransform.position.x < rightBarrier)
        {
            playerMove.x = 1;
            playerAnimator.Play("KiRight");
        }
        if (Input.GetKey(KeyCode.W) && playerTransform.position.y < upBarrier)
        {
            playerMove.y = 1;
            playerAnimator.Play("KiUp");
        }
        if (Input.GetKey(KeyCode.S) && playerTransform.position.y > downBarrier)
        {
            playerMove.y = -1;
            playerAnimator.Play("KiDown");
        }
        // else{
        //     playerMove.x = 0;
        //     playerMove.y = 0;
        // }
        // if(playerMove == Vector2.zero) playerAnimator.Play("KiIdle");

        // Normalize the movement vector to avoid faster diagonal movement
        playerMove.Normalize();

        // Apply movement
        playerPos = playerTransform.position + (Vector3)(playerMove * moveSpd * Time.deltaTime);
        playerTransform.position = playerPos;

    }
    void UpdateAnimator()
    {
        if (playerMove == Vector2.zero)
        {
        // No movement input, set to idle
            playerAnimator.Play("KiIdle");
        }
    }
}
