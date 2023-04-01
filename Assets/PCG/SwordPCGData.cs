/*
To load assets:
1. Use LoadAssetAsync to get access to the reference
2. Pass reference in as parameter to function
3. Profit
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SwordPCGData : MonoBehaviour {
    public Sprite guardShell;
    public List<Sprite> handles;
    public List<Sprite> guardPatterns;
    public List<Sprite> tipShells;
    public List<Sprite> tipSlashesStandard;
    public List<Sprite> tipSlashesCurved;

    private void Start()
    {
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/sword/handles").Completed += loadHandles; // handle variations
        Addressables.LoadAssetAsync<Sprite>("Textures/sword/guard_default").Completed += loadGuardShell; // guard shell
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/sword/guardPCG").Completed += loadGuardPatterns; // guard patterns
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/sword/tips").Completed += loadTipShells; // tip shells
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/sword/swordPCG").Completed += loadTipSlashesStandard; // tip slashes standard
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/sword/schimtarPCG").Completed += loadTipSlashesCurved; // tip slashes curved

    }

    public void loadHandles(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp;
        temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            handles.Add(sprite);
        }
    }

    public void loadGuardShell(AsyncOperationHandle<Sprite> toLoad)
    {
        Sprite temp;
        temp = toLoad.Result;

        guardShell = temp as Sprite;
    }
    
    public void loadGuardPatterns(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp; 
        temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            guardPatterns.Add(sprite);
        }
    }
    
    public void loadTipShells(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp; 
        temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            tipShells.Add(sprite);
        }
    }

    public void loadTipSlashesStandard(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp;
        temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            tipSlashesStandard.Add(sprite);
        }
    }

    public void loadTipSlashesCurved(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp;
        temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            tipSlashesCurved.Add(sprite);
        }
    }
}
