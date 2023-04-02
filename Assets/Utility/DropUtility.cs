using UnityEngine;

public class DropUtility : MonoBehaviour
{
    public GameObject drop;

    // you could probably make this a coroutine (decide this later)
    public void determineDrop(float x, float y)
    {
        float randomValue = Random.value;

        if (randomValue <.5)
        {
            Instantiate(drop, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}
