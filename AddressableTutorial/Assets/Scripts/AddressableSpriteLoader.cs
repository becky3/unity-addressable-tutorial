using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

public class AddressableSpriteLoader : MonoBehaviour
{
    public enum SpriteType
    {
        Normal,
        AtlasedSprite,
        AtlasedSpriteWithName,
    }

    [SerializeField]
    private SpriteType spriteType;

    [Header("Sprite")]
    //public AssetReferenceSprite newSprite;
    //public string newSpriteAddress;

    
    [Header("AtlasedSprite")]
    public AssetReferenceAtlasedSprite newAtlasedSprite;
    public string newAtlasedSpriteAddress;

    [Header("atlasedSpriteWithName")]
    //public AssetReferenceT<SpriteAtlas> newAtlas;
    //public string spriteAtlasAddress;
    //public string atlasedSpriteName;
    

    private SpriteRenderer spriteRenderer;
    private AsyncOperationHandle<SpriteAtlas> atlasOperation;
    private AsyncOperationHandle<Sprite> spriteOperation;

    public bool useAddress;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (useAddress)
        {
            switch (spriteType) 
            {
                case SpriteType.Normal:
                    //spriteOperation = Addressables.LoadAssetAsync<Sprite>(newSpriteAddress);
                    //spriteOperation.Completed += SpriteLoaded;
                    break;

                case SpriteType.AtlasedSprite:
                    spriteOperation = Addressables.LoadAssetAsync<Sprite>(newAtlasedSpriteAddress);
                    spriteOperation.Completed += SpriteLoaded;
                    break;

                case SpriteType.AtlasedSpriteWithName:
                    //atlasOperation = Addressables.LoadAssetAsync<SpriteAtlas>(spriteAtlasAddress);
                    //atlasOperation.Completed += SpriteAtlasLoaded;
                    break;
            }

        }
        else
        {
            switch (spriteType)
            {
                case SpriteType.Normal:
                    //spriteOperation = newSprite.LoadAssetAsync();
                    //spriteOperation.Completed += SpriteLoaded;
                    break;

                case SpriteType.AtlasedSprite:
                    spriteOperation = newAtlasedSprite.LoadAssetAsync();
                    spriteOperation.Completed += SpriteLoaded;
                    break;

                case SpriteType.AtlasedSpriteWithName:
                    //atlasOperation = newAtlas.LoadAssetAsync();
                    //atlasOperation.Completed += SpriteAtlasLoaded;
                    break;
            }
        }

    }

    private void SpriteAtlasLoaded(AsyncOperationHandle<SpriteAtlas> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.None:
                break;
            case AsyncOperationStatus.Succeeded:
                //spriteRenderer.sprite = obj.Result.GetSprite(atlasedSpriteName); ;
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