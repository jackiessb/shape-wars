using UnityEditor;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public GameObject defaultSword;

    public SwordPCGData swordPCG;

    private void Start()
    {
        swordPCG = GetComponent<SwordPCGData>();
    }

    // ideally this is where we add weights and stuff
    public void generateEnemyDrop()
    {

    }
}
