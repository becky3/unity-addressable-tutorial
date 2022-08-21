using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

public class AddressableSpriteLoader : MonoBehaviour
{
    //public AssetReferenceSprite newSprite;
    //public string newSpriteAddress;
    private AsyncOperationHandle<Sprite> spriteOperation;

    public AssetReferenceAtlasedSprite newAtlasedSprite;
    //public AssetReferenceT<SpriteAtlas> newAtlas;
    public string spriteAtlasAddress;
    public string atlasedSpriteName;
    public string newAtlasedSpriteAddress;
    private SpriteRenderer spriteRenderer;
    private AsyncOperationHandle<SpriteAtlas> atlasOperation;

    public bool useAddress;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (useAddress)
        {
            //spriteOperation = Addressables.LoadAssetAsync<Sprite>(newSpriteAddress);
            //spriteOperation.Completed += SpriteLoaded;

            spriteOperation = Addressables.LoadAssetAsync<Sprite>(newAtlasedSpriteAddress);
            spriteOperation.Completed += SpriteLoaded;

            //atlasOperation = Addressables.LoadAssetAsync<SpriteAtlas>(spriteAtlasAddress);
            //atlasOperation.Completed += SpriteAtlasLoaded;

        }
        else
        {
            //spriteOperation = newSprite.LoadAssetAsync();
            //spriteOperation.Completed += SpriteLoaded;

            spriteOperation = newAtlasedSprite.LoadAssetAsync();
            spriteOperation.Completed += SpriteLoaded;

            //atlasOperation = newAtlas.LoadAssetAsync();
            //atlasOperation.Completed += SpriteAtlasLoaded;

        }

    }

    private void SpriteAtlasLoaded(AsyncOperationHandle<SpriteAtlas> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.None:
                break;
            case AsyncOperationStatus.Succeeded:
                spriteRenderer.sprite = obj.Result.GetSprite(atlasedSpriteName); ;
                break;
            case AsyncOperationStatus.Failed:
                Debug.LogError("Sprite load failed. Using default sprite.");
                break;
        }
    }

    private void SpriteLoaded(AsyncOperationHandle<Sprite> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.None:
                break;
            case AsyncOperationStatus.Succeeded:
                spriteRenderer.sprite = obj.Result;
                break;
            case AsyncOperationStatus.Failed:
                Debug.LogError("Sprite load failed.");
                break;
        }
    }

    private void OnDestroy()
    {
        //if (spriteOperation.IsValid())
        //{
        //    Addressables.Release(spriteOperation);
        //    Debug.Log("Successfully released sprite load operation.");
        //}

        if (atlasOperation.IsValid())
        {
            Addressables.Release(atlasOperation);
            Debug.Log("Successfully released atlas load operation.");
        }
    }

}