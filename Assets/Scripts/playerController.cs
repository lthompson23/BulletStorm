using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //The rigidbody that controls the physics of this object
    public Rigidbody2D reggiebody;
    //The force of the jump
    public float jumpForce;
    //The movement speed of this character
    public float speed;

    public Animator animator;

    //How far the player must be from the floor to jump
    public float minFloorDistance;
    //
    public Vector3 raycastOriginOffset;
    //
    public float distanceBetweenRays;

    [SerializeField]
    private bool physicsMovement = true;

    [SerializeField]
    bool raw;

    // Start is called before the first frame update
    void Start()
    {
        //Find a rigidbody2d on this object and assign it to reggiebody
        reggiebody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (physicsMovement)
            PhysicsMovement();
        else
            KinematicMovement();

        
    }

    void KinematicMovement()
    {

    }

    void PhysicsMovement()
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
        }

        float xMov;

        //Get the input of the player (represented as (-1,1))
        if (raw)
            xMov = Input.GetAxisRaw("Horizontal");
        else
            xMov = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(xMov));

    
        //We can add force to the right based on that, if we want an "icey" movement
        //reggiebody.AddForce(Vector2.right * xMov * speed * Time.deltaTime);

        //Or we can change the velocity directly. Notice we're not changing the
        //velocity in y.
        reggiebody.velocity = new Vector2(xMov * speed, reggiebody.velocity.y);
    }
}
