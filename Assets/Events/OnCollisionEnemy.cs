using UnityEngine;

public class OnCollisionEnemy : MonoBehaviour
{
    private Animator control;
    private new CircleCollider2D collider;
    private EnemyAttributes stats;

    private void Start()
    {
        control = GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
        stats = GetComponent<EnemyAttributes>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Object " + this.name + " is colliding with Object " + collision.gameObject.name);
        
        if (collision.gameObject.tag == "Player") 
        {
            PlayerAttributes pHealth = collision.gameObject.GetComponent<PlayerAttributes>();

            Debug.Log("We got the amount of health! It's " + pHealth.health);

            // if enemy health is 0, trigger death
            control.SetTrigger("deathTrigger");
            collider.enabled = false;
        }
    }
}
