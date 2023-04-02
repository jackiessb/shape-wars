/*
TO DO:
when we collide with a player's weapon, recoil back, do not spin around until death or near death
 */
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class OnEnemyCollision : MonoBehaviour
{
    private Animator control;
    private AudioSource onHit;
    private new CircleCollider2D collider;
    private EnemyAttributes stats;

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
        if (CompareTag("Enemy"))
        {
            if (col.CompareTag("SwordTip") || col.CompareTag("AxeTip"))
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

        GameObject access = GameObject.Find("DropManager");
        access.GetComponent<DropUtility>().determineDrop(enemy.transform.position.x, enemy.transform.position.y);

        Destroy(enemy);
    }
}
