/*
TO DO 
1. Add different animations for different directions of dashing (*)
2. Polish speed and movement on keyboard inputs
    - make keyboard inputs apply angluar velocity during dash animation.
3. Crouch charge -- depending on how long you charge, you can change your horizontal and vertical speed for short
amount of time
 */
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // movement speed and behavior
    public bool rigid = false;
    public float HorizontalSpeedRigid = 0.0f;
    public float VerticalSpeedRigid = 0.0f;
    public float spinSpeed = 2.0f;
    public Vector2 velocity;
    public float velMag;

    // dashing
    public AnimationClip endOfDash;
    public int dashLength = 1;
    public bool isDashing = false;
    public bool isDashingClipStart = false; // this is public so that things can be adjusted globally
    public string direction = "left";
    public float EndOfDashBuffer = 0.0f; // changes when you can act out of a dash.
    public float dashMag = 2.0f;
    public float dashAngle = 0.0f;

    // crouch
    public AnimationClip crouchStages;
    public bool isCrouching = false;
    public bool isBursting = false;
    public float crouchLevel = 0;
    public float burstPowerLow = 1.0f;
    public float burstPowerHigh = 2.0f;
    public float burstFinal = 0.0f;
    public float burstAngle = 0.0f;
    
    // various private fields
    private Animator animations;
    private Rigidbody2D rb;
    private MovementUtility movement;
    private float horizontalPos;
    private float verticalPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
        movement = this.AddComponent<MovementUtility>();

        HorizontalSpeedRigid = 3.0f;
        VerticalSpeedRigid = 2.5f;
    }

    void Update()
    {
        // get input EVERY FRAME!
        horizontalPos = Input.GetAxis("Horizontal");
        verticalPos = Input.GetAxis("Vertical");

        // get velocity every frame
        velocity = rb.velocity;
        velMag = velocity.magnitude;

        // what direction am I walking? (IE--what was our latest input?)
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = "left";
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = "right";
        }

        // movement state machine
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) == true && isDashingClipStart == false)
            {
                isDashing = true;

                // ensure player is facing correct way during dash
                dashAngle = movement.faceDirection(horizontalPos, verticalPos);

                // THIS MAY NOT WORK YET, DEBUG
                // movement.rotateUpright(rb, spinSpeed, true);

                // start dash routine
                StartCoroutine(DashRigid());
            }

            // direction logic
            movement.orientationFix(direction, isDashing);

            // rigid body movement
            Move2DRigid();
        }

        // crouch charge
        if (Input.GetKeyDown(KeyCode.Q) && isDashing == false && isCrouching == false)
        {
            Crouch();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            OnCrouchOver();
        }

        // transition from finished burst input
        if (rb.velocity.magnitude <= 1.0f && !isCrouching && isBursting)
        {
            Debug.Log("Starting burstOver");
            animations.SetTrigger("isBurstingOver");
            isBursting = false;
        }
    }

    void Move2DRigid()
    {
        // as long as our dash and crouch charge is over, we can move.
        if (isDashingClipStart == false)
        {
            if (!isCrouching && !isBursting)
            {
                Vector2 WASDForce = new Vector2(horizontalPos * HorizontalSpeedRigid, verticalPos * VerticalSpeedRigid);
                rb.AddForce(WASDForce);
            }
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
        animations.SetTrigger("isDashing");

        // we need to find the direction the player is facing and then apply that as a force.
        // instead of finding direction and adjusting the sprite, let's just use a plain circle honestly...
        // JUST CHANGING THE LINEAR DRAG FOR NOW
        rb.drag = (rb.drag / dashMag);

        // how long dash lasts
        yield return new WaitForSeconds(dashLength);

        rb.drag = (rb.drag * dashMag);

        Debug.Log("DashRigid characteristics removed");

        // changing dash control for global movement
        isDashingClipStart = false;
        animations.SetTrigger("isDashingOver");
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

    IEnumerator Burst(float intensity)
    {
        // now we need to make sure the direction is random.
        // we can also do adjustments by range to make sure that its more unique often. (ex--if it falls into a range we have already
        // used, we can roll again)

        burstAngle = Random.Range(0f, 360f);
        float toRadians = burstAngle * Mathf.Deg2Rad;

        Vector2 force = new Vector2(intensity * Mathf.Cos(toRadians), intensity * Mathf.Sin(toRadians));

        rb.AddForce(force, ForceMode2D.Impulse);

        animations.SetTrigger("isBursting");
        isBursting = true;

        yield return null;
    }

    void Crouch()
    {
        animations.SetTrigger("isCrouching");
        isCrouching = true;
    }

    void OnCrouchOver()
    {
        isCrouching = false;

        StartCoroutine(Burst(burstFinal));
    }

    // Used as an AnimationEvent.
    void crouchChargeState(float level)
    {
        crouchLevel = level;
        Debug.Log("Charge Level: " + level);

        switch (crouchLevel)
        {
            case 0:
                burstFinal = Random.Range(burstPowerLow * Mathf.Pow(2, 2), burstPowerHigh * Mathf.Pow(2, 2)); 
                break;
            case 1:
                burstFinal = Random.Range(burstPowerLow * Mathf.Pow(10, 3), burstPowerHigh * Mathf.Pow(10, 3));
                break;
            case 3:
                burstFinal = Random.Range(burstPowerLow * Mathf.Pow(10, 4), burstPowerHigh * Mathf.Pow(10, 4));
                break;
            case 4:
                burstFinal = Random.Range(burstPowerLow * Mathf.Pow(10, 5), burstPowerHigh * Mathf.Pow(10, 5));
                break;
            case 5:
                // I want to have some kind of effect here for the last tier in this chain
                burstFinal = Random.Range(burstPowerLow * Mathf.Pow(10, 6), burstPowerHigh * Mathf.Pow(10, 6));
                break;
        }
    }

    // sets the status of the Dash to false. Used as an AnimationEvent.
    void noLongerDashing()
    {
        isDashing = false;
    }
}
