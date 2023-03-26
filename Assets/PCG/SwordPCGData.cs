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
    // will eventually replace with respective classes
    public List<Sprite> handles;
    public List<Sprite> tips;
    public List<Sprite> guards;

    private void Start()
    {
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/sword/handles").Completed += loadHandles;
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/sword/guards").Completed += loadGuards;
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/sword/tips").Completed += loadTips;
    }

    public void loadHandles(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp; temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            handles.Add(sprite);
        }

        Debug.Log("Handle loading complete");
    }
    
    public void loadGuards(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp; temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            guards.Add(sprite);
        }

        Debug.Log("Guard loading complete");
    }
    
    public void loadTips(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp; temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            tips.Add(sprite);
        }

        Debug.Log("Tip loading complete");
    }
}
