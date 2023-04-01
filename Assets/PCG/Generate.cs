// SURFACE GENERATION (NO PCG JUST YET!)

// 1. Set the resulting object to the default item of type

// 2. Pick a value from each array and set the value to the component of the new result object

// 3. Make sure that details are set to visible!

// 4. Instantiate
using System.Collections.Generic;
using UnityEngine;

public struct GuardChild {
    public GameObject GUARDDETAIL_LEFT;
    public GameObject GUARDDETAIL_RIGHT;
    public GameObject GUARDDETAIL_CENTER;
}

public struct TipChild {
    public GameObject SLASH_1;
    public GameObject SLASH_2;
    public GameObject SLASH_3;
}

public class Generate : MonoBehaviour
{
    public GameObject player;

    private SwordPCGData swordPCG;
    private AxePCGData axePCG;
    private SkatePCGData skatePCG;

    private void Start()
    {
        swordPCG = GetComponent<SwordPCGData>();
        axePCG = GetComponent<AxePCGData>();
        skatePCG = GetComponent<SkatePCGData>();
    }

    // Utility function to generate a random weapon--could be a rollerskate, axe, or sword. Merely for testing.
    public void accessDebug()
    {
        generateRandomSwordDebug();
    }

    /// <summary>
    /// Simply used for testing assets. DO NOT USE IN OFFICIAL RELEASE!
    /// </summary>
    /// <returns>void</returns>
    public void generateRandomSwordDebug()
    {
        GameObject currentSword = getSword();

        // TIP
        TipChild childrenTip = getTipChildren(currentSword.transform.Find("TIP").gameObject);
        
        SpriteRenderer SLASH_1 = childrenTip.SLASH_1.GetComponent<SpriteRenderer>();
        SpriteRenderer SLASH_2 = childrenTip.SLASH_2.GetComponent<SpriteRenderer>();
        SpriteRenderer SLASH_3 = childrenTip.SLASH_3.GetComponent<SpriteRenderer>();

        SLASH_1.sprite = swordPCG.tipSlashesStandard[Random.Range(0, 5)];
        SLASH_2.sprite = swordPCG.tipSlashesStandard[Random.Range(5, 10)];
        SLASH_3.sprite = swordPCG.tipSlashesStandard[Random.Range(10, 15)];

        SLASH_1.enabled = true;
        SLASH_2.enabled = true;
        SLASH_3.enabled = true;

        // GUARD
        GuardChild childrenGuard = getGuardChildren(currentSword.transform.Find("GUARD").gameObject);
        List<Sprite> blocks = new();
        List<Sprite> crosses = new();

        // ignore this shit bruh its ugly as hell
        blocks.Add(swordPCG.guardPatterns[0]);
        blocks.Add(swordPCG.guardPatterns[2]);
        blocks.Add(swordPCG.guardPatterns[4]);
        blocks.Add(swordPCG.guardPatterns[6]);
        blocks.Add(swordPCG.guardPatterns[8]);

        crosses.Add(swordPCG.guardPatterns[1]);
        crosses.Add(swordPCG.guardPatterns[3]);
        crosses.Add(swordPCG.guardPatterns[5]);
        crosses.Add(swordPCG.guardPatterns[7]);
        crosses.Add(swordPCG.guardPatterns[9]);

        SpriteRenderer GUARDDETAIL_LEFT = childrenGuard.GUARDDETAIL_LEFT.GetComponent<SpriteRenderer>();
        SpriteRenderer GUARDDETAIL_RIGHT = childrenGuard.GUARDDETAIL_RIGHT.GetComponent<SpriteRenderer>();
        SpriteRenderer GUARDDETAIL_CENTER = childrenGuard.GUARDDETAIL_CENTER.GetComponent<SpriteRenderer>();

        GUARDDETAIL_LEFT.sprite = blocks[Random.Range(0, 5)];
        GUARDDETAIL_RIGHT.sprite = blocks[Random.Range(0, 5)];
        GUARDDETAIL_CENTER.sprite = crosses[Random.Range(0, 5)];

        GUARDDETAIL_LEFT.enabled = true;
        GUARDDETAIL_RIGHT.enabled = true;
        GUARDDETAIL_CENTER.enabled = true;

        // HANDLE
        SpriteRenderer HANDLE = currentSword.transform.Find("HANDLE").gameObject.GetComponent<SpriteRenderer>();
        HANDLE.sprite = swordPCG.handles[Random.Range(0, 4)];
        HANDLE.enabled = true;
    }

    /// <summary>
    /// Simply used for testing assets. DO NOT USE IN OFFICIAL RELEASE!
    /// </summary>
    /// <returns>void</returns>
    public void generateRandomAxeDebug()
    {
        
    }

    /// <summary>
    /// Simply used for testing assets. DO NOT USE IN OFFICIAL RELEASE!
    /// </summary>
    /// <returns>void</returns>
    public void generateRandomSkateDebug()
    {

    }

    public GameObject getSword()
    {
        return player.transform.Find("Sword").gameObject;
    }

    public GameObject getAxe()
    {
        return player.transform.Find("Axe").gameObject;
    }

    public GameObject getSkate()
    {
        return player.transform.Find("Rollerskate").gameObject;
    }

    public GuardChild getGuardChildren(GameObject guard)
    {
        GuardChild children = new()
        {
            GUARDDETAIL_RIGHT = guard.transform.Find("GUARDDETAIL_RIGHT").gameObject,
            GUARDDETAIL_LEFT = guard.transform.Find("GUARDDETAIL_LEFT").gameObject,
            GUARDDETAIL_CENTER = guard.transform.Find("GUARDDETAIL_CENTER").gameObject
        };

        return children;
    }

    public TipChild getTipChildren(GameObject tip)
    {
        TipChild children = new()
        {
            SLASH_1 = tip.transform.Find("SLASH_1").gameObject,
            SLASH_2 = tip.transform.Find("SLASH_2").gameObject,
            SLASH_3 = tip.transform.Find("SLASH_3").gameObject
        };

        return children;
    }
}
