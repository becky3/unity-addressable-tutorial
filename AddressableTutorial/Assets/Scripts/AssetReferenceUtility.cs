using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetReferenceUtility : MonoBehaviour
{
    public AssetReference objectToLoad;
    public AssetReference accessoryObjectToLoad;
    private GameObject instantiatedObject;
    private AsyncOperationHandle<GameObject> objectOperation;
    private AsyncOperationHandle<GameObject> accessoryObjectOperation;

    // Start is called before the first frame update
    void Start()
    {
        objectOperation = Addressables.LoadAssetAsync<GameObject>(objectToLoad);
        objectOperation.Completed += ObjectLoadDone;
    }

    private void ObjectLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            var loadedObject = obj.Result;
            Debug.Log("Successfully loaded object.");

            instantiatedObject = Instantiate(loadedObject);
            Debug.Log("Successfully instantiated object.");

            if (accessoryObjectToLoad != null)
            {
                accessoryObjectOperation = accessoryObjectToLoad.InstantiateAsync(instantiatedObject.transform);
                accessoryObjectOperation.Completed += op =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        Debug.Log("Successfully loaded and instantiated accessory object.");
                    }
                };
            }
        }
    }

    private void OnDestroy()
    {
        if (accessoryObjectOperation.IsValid())
        {
            Addressables.ReleaseInstance(accessoryObjectOperation);
            Debug.Log("Successfully released accessory object load operation");
        }

        if (objectOperation.IsValid())
        {
            Addressables.Release(objectOperation);
            Debug.Log("Successfully released object load operation");
        }

        Destroy(instantiatedObject);
        Debug.Log("Successfully destroyed object.");

    }

}
