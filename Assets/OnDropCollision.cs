using UnityEngine;

public class OnDropCollision : MonoBehaviour
{
    private GameObject generator;

    void Start()
    {
        generator = GameObject.Find("Generator");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected!");

        // get access to generator
        Generate triggerDebugTest = generator.GetComponent<Generate>();
        triggerDebugTest.accessDebug();

        // destroy after test
        Destroy(gameObject);
    }
}
