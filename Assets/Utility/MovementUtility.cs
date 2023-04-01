// this is a utility script for specific things we may need to do with all objects in the game.
// REUSABLE CODE BABY!
using System.Collections;
using UnityEngine;
 
public class MovementUtility : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 0.5f;

    public IEnumerator rotateUpright(Rigidbody2D rb, float rotationSpeed, bool skipAnimation)
    {
        float startRotation = rb.rotation;
        float endRotation = 0f;
        float t = 0f;

        // NOT SURE IF THE SKIP ANIMATION ROTATION WORKS YET!!!
        if (skipAnimation)
        {
            Debug.Log("Trying to skip the animation");

            float newRotation = Mathf.LerpAngle(startRotation, endRotation, 0);
            rb.MoveRotation(newRotation);
            yield return null;
        } 
        else
        {
            while (t < 1f)
            {
                t += Time.deltaTime * rotationSpeed;
                float newRotation = Mathf.LerpAngle(startRotation, endRotation, t);
                rb.MoveRotation(newRotation);
                yield return null;
            }
        }
    }

    public float faceDirection(float horizontalPos, float verticalPos)
    {
        // find the tangent angle
        float playerAngle = Mathf.Atan2(verticalPos, horizontalPos) * Mathf.Rad2Deg - 90; // adjust for Unity
        Debug.Log("The angle calculated was " + playerAngle + " and the player's position was " + verticalPos + ", " + horizontalPos);

        // axis for the angle to rotate around
        Quaternion angleA = Quaternion.AngleAxis(playerAngle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, angleA, Time.deltaTime * 50);

        return playerAngle;
    }

    public void orientationFix(string direction, bool isDashing)
    {
        if (!isDashing)
        {
            if (direction == "left" && transform.localScale.x !> 0)
            {
                Debug.Log("go left");
                transform.localScale = new Vector3(-transform.localScale.x,
                    transform.localScale.y, transform.localScale.z);
            }
            else if (direction == "right" && transform.localScale.x !< 0)
            {
                Debug.Log("go right");
                transform.localScale = new Vector3(-transform.localScale.x,
                    transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
