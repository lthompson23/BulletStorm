using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    public Rigidbody2D reggiebody;

    //The force of the jump
    public float jumpForce;

    //The movement speed of this character
    public GameObject shootPoint;

     

    public Animator animator;

    //How far the player must be from the floor to jump
    public float minFloorDistance;
    //
    public Vector3 raycastOriginOffset;
    //
    public float distanceBetweenRays;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar.health = 100f; 
    }

    void Update()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move the player
        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;

        // Flip the sprite based on the direction
        FlipSprite(horizontalInput);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        {
            //Ray2D floorDetection = new Ray2D(this.transform.position, -Vector2.up);
            Debug.DrawRay(this.transform.position + raycastOriginOffset,
                -Vector2.down * minFloorDistance, Color.red);

            bool middleRay = Physics2D.Raycast(this.transform.position + raycastOriginOffset,
                -Vector2.down, minFloorDistance);
            bool leftRay = Physics2D.Raycast(
                this.transform.position + raycastOriginOffset - Vector3.right * distanceBetweenRays,
                -Vector2.down, minFloorDistance);
            bool rightRay = Physics2D.Raycast(
                this.transform.position + raycastOriginOffset + Vector3.right * distanceBetweenRays,
                -Vector2.down, minFloorDistance);

            //If the player presses the jump button
            if (Input.GetButtonDown("Jump")
                //We can cast the position to a vector2 for this operation, since the function
                //takes in a vector2 and our libraries already know how to convert Vector3 to Vector2
                //&& Physics2D.Raycast((Vector2)this.transform.position + raycastOriginOffset,
                //In this case, we simply changed the type of raycastOriginOffset to match
                && (leftRay || middleRay || rightRay))
            {
                //Add force to the reggiebody upwards
                reggiebody.AddForce(Vector2.up * jumpForce);

                animator.SetBool("isJumping", true);
            }

            if (healthBar.health <= 0)
            { 
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    void FlipSprite(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            // Moving right, flip the sprite to face right
            spriteRenderer.flipX = false;
            
        }
        else if (horizontalInput < 0)
        {
            // Moving left, flip the sprite to face left
            spriteRenderer.flipX = true;

            
        }
        // If horizontalInput is 0, do not change the sprite orientation
    }

   
}
