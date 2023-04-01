using UnityEngine;

public class WeaponUtility : MonoBehaviour
{
    public bool hideWeaponToggle;
    public bool hasRotated = false;

    public bool playerObjectDashingStatus = false;
    public bool playerObjectCrouchingStatus = false;
    public bool playerObjectBusrtStatus = false;

    private PlayerMovement pm;
    private GameObject[] options;
    private GameObject currentActiveWeapon;
    private string attachedWeaponName = "";

    // Start is called before the first frame update
    void Start()
    {
        options = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        pm = GetComponent<PlayerMovement>();
    }

    // WARNING: THIS IS NOT AT ALL OPTIMAL.
    // a redesign is in order.
    void Update()
    {
        playerObjectDashingStatus = pm.isDashing;
        playerObjectCrouchingStatus = pm.isCrouching;
        playerObjectBusrtStatus = pm.isBursting;

        foreach (GameObject obj in options)
        {
            if (obj.activeSelf)
            {
                if (obj.name == "Sword" || obj.name == "Axe" || obj.name == "Rollerskate")
                {
                    attachedWeaponName = obj.name;
                    currentActiveWeapon = obj;
                }
            }
        }

        // some condition here so that we don't waste memory
        if (hideWeaponToggle)
        {
            hideWeapon();
        }
    }

    void hideWeapon()
    {
        if (pm.isDashing || pm.isCrouching || pm.isBursting)
        {
            Debug.Log("SetActiveFalse called");
            currentActiveWeapon.SetActive(false);
            // make weapon disappear

        }
        else if (!pm.isDashing || !pm.isCrouching || !pm.isBursting)
        {
            Debug.Log("SetActiveTrue called");
            currentActiveWeapon.SetActive(true);
            // make it show back up
        }
    }
}
