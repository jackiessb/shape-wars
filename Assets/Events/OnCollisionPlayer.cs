using UnityEngine;

public class OnCollisionPlayer : MonoBehaviour
{
    private PlayerAttributes player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerAttributes>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Enemy (Clone)")
        {
            player.EnemiesHit++;
        }
    }
}
