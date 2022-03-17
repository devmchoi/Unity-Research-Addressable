using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


/**
    // Reference (Example)
    using System;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    [Serializable]
    public class AssetReferenceT<TObject> : AssetReference where TObject : Object
    {
	    public new TObject editorAsset {
		    get;
	    }

	    public AssetReferenceT (string guid)
		    : base (guid);

	    [Obsolete]
	    public AsyncOperationHandle<TObject> LoadAsset ();

	    public virtual AsyncOperationHandle<TObject> LoadAssetAsync ();

	    public override bool ValidateAsset (Object obj);

	    public override bool ValidateAsset (string path);
    }

			

/**
    // Reference (Example)
    using System;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public class AssetReferenceTexture : AssetReferenceT<Texture>
    {
	    public AssetReferenceTexture (string guid)
		    : base (guid);
    }
 **/


/**
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.U2D;

public interface IKeyEvaluator
{
	object RuntimeKey {
		get;
	}

	bool RuntimeKeyIsValid ();
}


[Serializable]
public class AssetReference : IKeyEvaluator
{
	[FormerlySerializedAs ("m_assetGUID")]
	[SerializeField]
	private string m_AssetGUID = "";

	[SerializeField]
	private string m_SubObjectName;

	[SerializeField]
	private string m_SubObjectType = null;

	private AsyncOperationHandle m_Operation;

	private Object m_CachedAsset;

	private string m_CachedGUID = "";

	public AsyncOperationHandle OperationHandle => m_Operation;

	public virtual object RuntimeKey {
		get;
	}

	public virtual string AssetGUID => m_AssetGUID;

	public virtual string SubObjectName {
		get;
		set;
	}

	internal virtual Type SubOjbectType {
		get;
	}

	public bool IsDone => m_Operation.IsDone;

	public virtual Object Asset {
		get;
	}

	protected Object CachedAsset {
		get;
		set;
	}

	public virtual Object editorAsset {
		get;
	}

	public bool IsValid ();

	public AssetReference ();

	public AssetReference (string guid);

	public override string ToString ();

	private static AsyncOperationHandle<T> CreateFailedOperation<T> ();

	[Obsolete]
	public AsyncOperationHandle<TObject> LoadAsset<TObject> ();

	[Obsolete]
	public AsyncOperationHandle<SceneInstance> LoadScene ();

	[Obsolete]
	public AsyncOperationHandle<GameObject> Instantiate (Vector3 position, Quaternion rotation, Transform parent = null);

	[Obsolete]
	public AsyncOperationHandle<GameObject> Instantiate (Transform parent = null, bool instantiateInWorldSpace = false);


	public virtual AsyncOperationHandle<TObject> LoadAssetAsync<TObject> ();

	public virtual AsyncOperationHandle<SceneInstance> LoadSceneAsync (LoadSceneMode loadMode = 0, bool activateOnLoad = true, int priority = 100);

	public virtual AsyncOperationHandle<SceneInstance> UnLoadScene ();

	public virtual AsyncOperationHandle<GameObject> InstantiateAsync (Vector3 position, Quaternion rotation, Transform parent = null);

	public virtual AsyncOperationHandle<GameObject> InstantiateAsync (Transform parent = null, bool instantiateInWorldSpace = false);

	public virtual bool RuntimeKeyIsValid ();

	public virtual void ReleaseAsset ();

	public virtual void ReleaseInstance (GameObject obj);

	public virtual bool ValidateAsset (Object obj);

	public virtual bool ValidateAsset (string path);

	public virtual bool SetEditorAsset (Object value);

	public virtual bool SetEditorSubObject (Object value);
}
**/

/**
    public void LoadPrefab()
    {
        // 1안, 어드레서블 명으로 경로 얻기.
        Addressables.LoadResourceLocationsAsync("SomeAddrsaalbeName").Completed +=
            (handle) =>
            {
               var locations = handle.Result;

               Addressables.InstantiateAsync(locations[0]);
            };

        // 2안, 레이블로 경로 얻기
        Addressables.LoadResourceLocationsAsync("SomeLabel").Completed +=
            (handle) =>
            {
               var locations = handle.Result;

               Addressables.InstantiateAsync(locations[0]);
            };
    }

    // Addressalbes.InstantiateAsync로 생성된 함수는 반드시 Addressables.ReleaseInstance로 해제해준다.
    // GameObject.Instantiate(...)로 생성된 GameObject는 어드레서블로 해제할 수 없으니 GameObject.Destroy('object')를 사용한다.

	// Addressables.LoadAssetsAsync 		<-> Addressables.ReleaseAsset('AsyncOperationHandle' 또는 'object')
	// Addressalbes.InstantiateAsync 		<-> Addressables.ReleaseInstance('AsyncOperationHandle' 또는 'object')
	// Addressables.LoadSceneAysnc 			<-> Addressables.UnloadSceneAsync('AsyncOperationHandle' 또는 'SceneIsntance')
	// AssetReference.LoadAssetAsync() 		<-> AssetReference.ReleaseAsset()
	// AssetReference.InstantiateAsync() 	<-> AssetReference.ReleaseInstance()
	// AssetReference.LoadSceneAsync() 		<-> AssetReference.UnLoadScene()

	// 에셋번들의 파일명. 에셋번들 파일의 이름은 '에셋그룹 명 + hash값'으로 구성된다. 
	// hash값으로 변경을 감지하고 버전을 체크한다.
	// 디폴트 : [UnityEditor.EditorUserBuildSettings.activeBuildTarget]

	// [SerializeField]		
    // private AssetReference assetReference;
	// [SerializeField]	
    // private AssetReferenceAudioClip assetReferenceAudioClip;
	// [SerializeField]	
    // private AssetReferenceGameObject assetReferenceGameObject;
	// [SerializeField]	
    // private AssetReferenceSprite assetReferenceSprite;
	// [SerializeField]	
    // private AssetReferenceAtlasedSprite assetReferenceAtlasedSprite;
	// [SerializeField]	
    // private AssetLabelReference assetLabelReference;
	// [SerializeField]	
    // private AssetReferenceTexture assetReferenceTexture;
	// [SerializeField]	
    // private AssetReferenceTexture2D assetReferenceTexture2D;
 */ 

public class AddressableAssetManager : MonoBehaviour
{
    private bool m_bIsInitialized = false;

    public void Initialize()
    {
        if (m_bIsInitialized)
            return;

        // UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator> asyncOperationHandle;
		// asyncOperationHandle.Destroyed;
		// asyncOperationHandle.CompletedTypeless;
		// asyncOperationHandle.PercentComplete;
		Addressables.InitializeAsync().Completed += OnAsyncOperationHandleCompleted;
    }

    private void OnAsyncOperationHandleCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator> obj)
    {
        m_bIsInitialized = true;
	}



	public void Test()
    {
    	/*
		UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator
		UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation

		Addressables.LoadAssetAsync<TObject> (IResourceLocation location);
		Addressables.LoadAssetAsync<TObject> (object key);

		Addressables.LoadResourceLocationsAsync (IEnumerable keys, MergeMode mode, Type type = null);
		Addressables.LoadResourceLocationsAsync (object key, Type type = null);

		Addressables.Release<TObject> (TObject obj);
		Addressables.Release<TObject> (AsyncOperationHandle<TObject> handle);
		Addressables.Release (AsyncOperationHandle handle);
		Addressables.ReleaseInstance (GameObject instance);
		Addressables.ReleaseInstance (AsyncOperationHandle handle);
		Addressables.ReleaseInstance (AsyncOperationHandle<GameObject> handle);

		Addressables.GetDownloadSizeAsync(...string);
		Addressables.GetDownloadSizeAsync(...object);
		Addressables.GetDownloadSizeAsync(...IEnumerable);

		Addressables.DownloadDependenciesAsync(object, bool);
		Addressables.ClearDependencyCacheAsync(object);

		Addressables.AsyncOperationHandle<GameObject> InstantiateAsync (IResourceLocation location, Transform parent = null, bool instantiateInWorldSpace = false, bool trackHandle = true);
		Addressables.AsyncOperationHandle<GameObject> InstantiateAsync (IResourceLocation location, Vector3 position, Quaternion rotation, Transform parent = null, bool trackHandle = true);
		Addressables.AsyncOperationHandle<GameObject> InstantiateAsync (object key, Transform parent = null, bool instantiateInWorldSpace = false, bool trackHandle = true);
		Addressables.AsyncOperationHandle<GameObject> InstantiateAsync (object key, Vector3 position, Quaternion rotation, Transform parent = null, bool trackHandle = true);
		Addressables.AsyncOperationHandle<GameObject> InstantiateAsync (object key, InstantiationParameters instantiateParameters, bool trackHandle = true);
		Addressables.AsyncOperationHandle<GameObject> InstantiateAsync (IResourceLocation location, InstantiationParameters instantiateParameters, bool trackHandle = true);

		Addressables.AsyncOperationHandle<SceneInstance> LoadSceneAsync (object key, LoadSceneMode loadMode = 0, bool activateOnLoad = true, int priority = 100);
		Addressables.AsyncOperationHandle<SceneInstance> LoadSceneAsync (IResourceLocation location, LoadSceneMode loadMode = 0, bool activateOnLoad = true, int priority = 100);

		Addressables.AsyncOperationHandle<SceneInstance> UnloadSceneAsync (SceneInstance scene, bool autoReleaseHandle = true);
		Addressables.AsyncOperationHandle<SceneInstance> UnloadSceneAsync (AsyncOperationHandle handle, bool autoReleaseHandle = true);
		Addressables.AsyncOperationHandle<SceneInstance> UnloadSceneAsync (AsyncOperationHandle<SceneInstance> handle, bool autoReleaseHandle = true);
		*/
	}
}
