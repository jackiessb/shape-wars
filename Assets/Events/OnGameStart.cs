using System;
using UnityEngine;

public class OnGameStart : MonoBehaviour
{
    public bool debug = true;

    public GameObject enemyPlaceholder;
    public int numberOfEnemies = 3;

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
            for (int i = 0; i < numberOfEnemies; i++)
            {
                // sets range for enemy spawning
                int horizontalLimit = UnityEngine.Random.Range(xMin, xMax);
                int verticalLimit = UnityEngine.Random.Range(yMin, yMax);

                // changes name in the hierarchy
                enemyPlaceholder.name = "Enemy ";

                // create the enemy
                GameObject newEnemy = Instantiate(enemyPlaceholder, new Vector3(horizontalLimit, verticalLimit, 0), Quaternion.identity);
            }
        }   
    }
}
