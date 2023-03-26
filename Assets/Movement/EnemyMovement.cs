using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool isHit = false;
    public float hitstun = 10.0f;

    private GameObject player;
    private float moveSpeed = 1.0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // move towards the player
        float step = moveSpeed * Time.deltaTime;
        
        // hitstun needs to be in coroutine form
        if (!isHit)
        {
            // if we are not hit, continue to move
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        } 
        else
        {
            // if we are hit, start the coroutine here
            StartCoroutine(enterhitstun());
        }
    }

    IEnumerator enterhitstun()
    {
        yield return new WaitForSeconds(hitstun);

        isHit = false;
    }

    public void universalHitNotify()
    {
        isHit = true;
    }
}
