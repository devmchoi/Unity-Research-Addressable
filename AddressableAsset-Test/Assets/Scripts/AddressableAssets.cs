using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
{
    public AssetReferenceAudioClip(string guid) : base(guid) { }
}

[Serializable]
public class AssetReferenceGameObject : AssetReferenceT<GameObject>
{
    public AssetReferenceGameObject(string guid) : base(guid) { }
}
