using UnityEngine;

public class OnPlayerAttack : MonoBehaviour
{
    public float attackStrength = 50f; // default
    public float drag = 3.5f; // default

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // attack logic
        if (Input.GetKey(KeyCode.Space)) {
            attack();
        }
    }

    void attack()
    {
        // maybe sword hitbox should activate here
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(attackStrength);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-attackStrength);
        }
        else
        {
            rb.AddTorque(-attackStrength);
        }

        rb.angularDrag = drag;
    }
}
