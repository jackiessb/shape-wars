/*
TO DO 
1. Add different animations for different directions of dashing (*)
2. Polish speed and movement on keyboard inputs
    - make keyboard inputs apply angluar velocity during dash animation.
 */
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // movement speed and behavior
    public float HorizontalSpeedRigid = 0.0f; 
    public float VerticalSpeedRigid = 0.0f;
    public float spinSpeed = 2.0f;
    public bool rigid = false;

    // dashing
    public AnimationClip endOfDash;
    public float EndOfDashBuffer = 0.0f; // changes when you can act out of a dash.
    public int dashLength = 1;
    public float dashMag = 10.0f;
    public float dashAngle = 0.0f;

    public bool isDashing = false;
    public bool isDashingClipStart = false; // this is public so that things can be adjusted globally
    public string direction = "left";

    // various private fields
    private float horizontalPos;
    private float verticalPos;
    private Animator dash;
    private Rigidbody2D rb;
    private MovementUtility movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<Animator>();
        movement = this.AddComponent<MovementUtility>();

        HorizontalSpeedRigid = 3.0f;
        VerticalSpeedRigid = 2.5f;
    }

    void Update()
    {
        // get input EVERY FRAME!
        horizontalPos = Input.GetAxis("Horizontal");
        verticalPos = Input.GetAxis("Vertical");

        // movement state machine
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) == true && isDashingClipStart == false)
            {
                isDashing = true;

                // ensure player is facing correct way during dash
                dashAngle = movement.faceDirection(horizontalPos, verticalPos);

                // THIS MAY NOT WORK YET, DEBUG
                movement.rotateUpright(rb, spinSpeed, true);

                // start dash routine
                StartCoroutine(DashRigid());
            }

            // direction logic
            movement.orientationFix(direction, isDashing);

            // rigid body movement
            Move2DRigid();
        }

        // what direction am I walking? (IE--what was our latest input?)
        if (Input.GetKey(KeyCode.A))
        {
            direction = "left";
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = "right";
        }

        // jitter hehe
        // not yet implemented
    }

    void Move2DRigid()
    {
        // as long as our dash is over, we can move.
        if (isDashingClipStart == false)
        {
            Vector2 WASDForce = new Vector2(horizontalPos * HorizontalSpeedRigid, verticalPos * VerticalSpeedRigid);
            rb.AddForce(WASDForce);
        } 
        else if (isDashingClipStart == true)
        {
            // Debug.Log("Waiting for dash to be over...");
            StartCoroutine(WaitForDash());
        }       
    }

    IEnumerator DashRigid()
    {
        Debug.Log("Starting DashRigid coroutine");

        // dash control to make sure you can't mash dash + dash trigger for animation
        isDashingClipStart = true;
        dash.SetTrigger("isDashing");

        // we need to find the direction the player is facing and then apply that as a force.
        // instead of finding direction and adjusting the sprite, let's just use a plain circle honestly

        // how long dash lasts
        yield return new WaitForSeconds(dashLength);

        Debug.Log("DashRigid characteristics removed");

        // changing dash control for global movement
        isDashingClipStart = false;
        dash.SetTrigger("isDashingOver");

    }

    IEnumerator WaitForDash()
    {
        // find the time it will take for dashClip to finish
        float time = Time.time + endOfDash.length;

        // while the dashClip is not over...
        while ((Time.time - EndOfDashBuffer) < time)
        {
            yield return null;
        }

        // rotate upright
        StartCoroutine(movement.rotateUpright(rb, spinSpeed, false));
        // rotateUpright();
    }

    // sets the status of the Dash to false. Used as an AnimationEvent.
    void noLongerDashing()
    {
        isDashing = false;
    }
}
