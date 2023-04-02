/*
To load assets:
1. Use LoadAssetAsync to get access to the reference
2. Pass reference in as parameter to function
3. Profit
 */
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AxePCGData : MonoBehaviour
{
    public Sprite axeHandleDefault;
    public List<Sprite> axeHandles;
    public List<Sprite> axeShafts;
    public List<Sprite> axeHeads;

    private void Start()
    {
        Addressables.LoadAssetAsync<Sprite>("Textures/axe/axeHandleDefault").Completed += loadDefaultAxeHandle; // default handle
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/axe/axeHandle").Completed += loadAxeHandles; // handle variations (axe)
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/axe/axeShaft").Completed += loadAxeShafts; // shaft variations
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/axe/axeHeads2").Completed += loadAxeHeads; // head variations
    }

    public void loadDefaultAxeHandle(AsyncOperationHandle<Sprite> toLoad)
    {
        Sprite temp;
        temp = toLoad.Result;

        axeHandleDefault = temp as Sprite;
    }

    public void loadAxeHandles(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp;
        temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            axeHandles.Add(sprite);
        }
    }

    public void loadAxeShafts(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp;
        temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            axeShafts.Add(sprite);
        }
    }

    public void loadAxeHeads(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp;
        temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            axeHeads.Add(sprite);
        }
    }
}
