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

public class SkatePCGData : MonoBehaviour
{
    public List<Sprite> skatesLeft;
    public List<Sprite> skatesRight;

    private void Start()
    {
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/rollerskate/rollerSkatesLeft").Completed += loadLeftSkate; // left skate
        Addressables.LoadAssetAsync<IList<Sprite>>("Textures/rollerskate/rollerSkatesRight").Completed += loadRightSkate; // right skate
    }

    public void loadLeftSkate(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp;
        temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            skatesLeft.Add(sprite);
        }
    }

    public void loadRightSkate(AsyncOperationHandle<IList<Sprite>> toLoad)
    {
        IList<Sprite> temp;
        temp = toLoad.Result;

        foreach (Sprite sprite in temp)
        {
            skatesRight.Add(sprite);
        }
    }
}
