// Daniel Nguyen
// 000972335
// movement scripts are scary

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMove : MonoBehaviour
{
    // the usual movement variables
    public float moveSpeed = 5f;
    public float sprintFactor = 1.5f; // multiplier applied to move speed when sprinting
    private float sprintSpeed;  // the actual sprint speed after math is done

    // the usual jumpHeight variable (slightly increased)
    public float jumpHeight = 2;

    public float lowJumpFraction = 0.5f;    // multiplier on tap jumps to make them shorter
    public float fastFallMultiplier = 1.8f; // multiplier on falling so descension is faster than ascension
    private bool isGrounded; // bool to know if player is grounded and thereby able to jump

    // vector3 to store velocity in
    private Vector3 velocity;

    // attach the camera's transform so it follows the transform of the player
    public Transform cameraTransform;

    // mouse/looking variables
    public float mouseSensitivity = 5;
    private float verticalLookRotation = 0; //Helper variable for current rotation

    // add character controller component as variable to use its variables
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        //grab character controller and put in character controller variable
        controller = GetComponent<CharacterController>();

        // math out sprint speed
        sprintSpeed = moveSpeed * sprintFactor;

        // lock cursor and hide so first person works, pressing escape brings it back
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // we need to have the mouse looking and movement working constantly
        // so call both functions every frame
        MouseLook();
        Movement();
    }

    void MouseLook()
    {
        // instead of using the base Mouse X and Y axes for mouse looking, use new floats
        // and multiply the input axes by mouse sensitivity
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // let the camera look up and down by mouse Y
        verticalLookRotation -= mouseY;
        // lock vertical look rotation so you can't spin infinitely
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -30f, 30f);

        // rotation is the axis that is rotating, not the direction rotating
        // so spinning on y axis is like spinning around a pole (that is vertical)
        // gotta euler the rotation since quaternions are freaky scary stuff
        // and then use the new euler'd angle to set vertical look rotation
        cameraTransform.localRotation = Quaternion.Euler
                                        (verticalLookRotation, 0, 0);
        // for the "x" rotation we need to multiply the rotation on the y axis by our mouse X rotation
        // we use Vector3.up which is (0, 1, 0) to do that since it's shorter than writing (0, 1 * mouseX, 0)
        transform.Rotate(Vector3.up * mouseX);
    }

    void Movement()
    {
        // the built-in isGrounded function isn't cool and controllable enough so we use our own
        isGrounded = controller.isGrounded;

        // make them player move down while grounded just to make sure they stay grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        // floats to store x and z movement that are easier to write
        // reminder: horizontal input axis uses A and D and vertical uses W and S
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // calculate L/R and FW/BW movement on their respective axes using our input floats
        // and store as a Vector3 for easy use
        Vector3 move = transform.right * x + transform.forward * z;

        // float for calculating current speed since it can change betwee base and sprint speed
        float currentSpeed = moveSpeed;

        // when the player holds sprint key and is moving
        if (Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(z) > 0.1f)
        {
            // set current speed to sprint speed
            currentSpeed = sprintSpeed;
        }

            // make the player actually move based on speed and time
            controller.Move(move * currentSpeed * Time.deltaTime);

        // if the player is grounded and presses space key
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // do a bunch of scary math to jump using physics and kinematics
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        // if the player is moving vertically but the space key isn't being input,
        // it means they tapped the space key
        // which means they get a short jump
        // and then we gotta do more math
        if (velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            // apply more gravity but multiplied by our low jump multiplier and time
            velocity.y += Physics.gravity.y * lowJumpFraction * Time.deltaTime;
        }

        // if falling, add more gravity to fall faster using fast fall multiplier and time
        if (velocity.y < 0)
        {
            velocity.y += Physics.gravity.y * (fastFallMultiplier - 1) * Time.deltaTime;
        }

        // apply gravity over time to the player's y velocity
        velocity.y += Physics.gravity.y * Time.deltaTime;

        // final calculation for 3D movement using move (our x and z axis movement)
        // multiplied by current speed (which is our speed depending on if we're walking or sprinting)
        // plus Vector3.up (0, 1,0) for y axis movement
        // multiplied by our y velocity which has all the fun physics math applied to it
        Vector3 moveCalculation = move * currentSpeed +
            Vector3.up * velocity.y;

        // then apply time to the final movement calculation and use it to move the character (controller)
        controller.Move(moveCalculation * Time.deltaTime);
    }
}
