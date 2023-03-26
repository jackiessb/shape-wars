/*
TO DO:
when we collide with a player's weapon, recoil back, do not spin around until death or near death
 */
using System.Collections;
using UnityEngine;

public class OnCollisionEvent : MonoBehaviour
{
    private Animator control;
    private new CircleCollider2D collider;
    private EnemyAttributes stats;
    private AudioSource onHit;

    private void Start()
    {
        control = GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
        stats = GetComponent<EnemyAttributes>();
        onHit = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;

        // if the ENEMY collided with a PLAYER'S weapon tip
        // subtract health
        if (CompareTag("Enemy"))
        {
            if (col.CompareTag("SwordTip"))
            {
                // remove one health point for each hit
                stats.health--;

                // play a sound!
                onHit.Play();

                // sends message to this function and then calls it to
                // let the movement script know that a collision occured
                SendMessage("universalHitNotify");

                // if enemy health is 0, trigger death
                if (stats.health <= 0)
                {
                    control.SetTrigger("deathTrigger");
                    collider.enabled = false;

                    StartCoroutine(triggerDeath(gameObject));
                }
            }
        }
    }

    // function for killing off evil things
    IEnumerator triggerDeath(GameObject enemy)
    {
        yield return new WaitForSeconds(1.7f);

        Destroy(enemy);
    }
}
