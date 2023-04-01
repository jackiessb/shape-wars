using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool debug = true;

    public GameObject enemyPlaceholder;
    public GameObject player;

    public int numberOfEnemiesOnStart = 7;

    public int xMin = -9;
    public int xMax = 9;
    public int yMin = -4;
    public int yMax = 4;

    // Start is called before the first frame update
    void Start()
    {
        // starts randomness engine
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);

        // spawn enemies
        if (debug)
        {
            for (int i = 0; i < numberOfEnemiesOnStart; i++)
            {
                // create the enemy
                createEnemy();
            }
        }   
    }

    public GameObject createEnemy()
    {
        // sets range for enemy spawning
        int hLimit = UnityEngine.Random.Range(xMin, xMax);
        int vLimit = UnityEngine.Random.Range(yMin, yMax);

        // changes name in the hierarchy
        enemyPlaceholder.name = "Enemy ";

        GameObject temp = Instantiate(enemyPlaceholder, new Vector3(hLimit, vLimit, 0), Quaternion.identity);

        float distance = Vector3.Distance(temp.transform.position, player.transform.position);

        if (distance < 1.5f)
        {
            // RECURSION!!!
            temp = createEnemy();
        }

        return temp;
    }
}
