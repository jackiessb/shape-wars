using UnityEngine;

public class WeaponUtility : MonoBehaviour
{
    public GameObject player;
    public bool hideWeaponToggle;
    public bool hasRotated = false;

    private PlayerMovement pm;
    private SpriteRenderer[] parts; // weapon parts
    private int amtParts = 0;

    // Start is called before the first frame update
    void Start()
    {
        parts = GetComponentsInChildren<SpriteRenderer>();
        pm = player.GetComponent<PlayerMovement>();

        amtParts = parts.Length;
    }

    void Update()
    {
        // some condition here so that we don't waste memory
        if (hideWeaponToggle)
        {
            hideWeapon();
        }
    }

    void hideWeapon()
    {
        for (int i = 0; i < amtParts; i++)
        {
            if (pm.isDashing)
            {
                // make weapon disappear
                parts[i].gameObject.SetActive(false);
            }
            else if (!pm.isDashing)
            {
                // make it show back up
                parts[i].gameObject.SetActive(true);
            }
        }
    }
}
