using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesManager : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    AssetReference assetReference;

    [SerializeField]
    AssetReferenceGameObject assetReferenceGameObject;

    [SerializeField]
    AssetReferenceSprite assetReferenceSprite;

    // Start is called before the first frame update
    void Start()
    {
        AddressablesPrefab();
        AddressablesSprite();
        AddressablesScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddressablesPrefab()
    {
        Addressables.InstantiateAsync(assetReferenceGameObject);
    }

    public void AddressablesScene()
    {
        assetReference.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void AddressablesSprite()
    {
        assetReferenceSprite.LoadAssetAsync().Completed += OnSpriteLoaded;
    }

    private void OnSpriteLoaded(AsyncOperationHandle<Sprite> obj)
    {
        image.sprite = obj.Result;
    }
}
