using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables for speed, jump, and move input
    public float speed;
    public float jumpForce;
    private float moveInput;

    //variable for the rigid body (gravity)
    private Rigidbody2D rb;

    //variable for checking if the player is facing right
    private bool facingRight = true;

    //variables for checking if its touching the ground
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    // variables for double jump
    private int extraJumps;
    public int extraJumpsValue;

    void Start(){
        //get the rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        //check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //allows the player to move left and right
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //flips the sprite to the direction your facing
        if(facingRight == false && moveInput > 0){
            Flip();
        } else if(facingRight == true && moveInput < 0){
            Flip();
        }
    }

    void Flip(){

        //flips the character in the direction they are moving
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Update(){

        //checks if the player is grounded and restores their doubles jumps if they are
        if(isGrounded == true){
            extraJumps = extraJumpsValue;
        }

        //checks if the player should be able to jump
        if(Input.GetKeyDown(KeyCode.W) && extraJumps > 0){
            //makes the player jump in air
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        //checks if the player cant double jump but is on the ground
        } else if(Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true){
            //makes the player jump
            rb.velocity = Vector2.up * jumpForce;           
        }
    }

}
