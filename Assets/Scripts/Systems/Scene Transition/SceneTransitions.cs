using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneTransitions : Singleton<SceneTransitions>
{
    GameObject player;
    Coroutine transitionCoroutine;
    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator SceneTransition(AssetReference sceneReference, string entryWay)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperationHandle<SceneInstance> newLevelLoad = Addressables.LoadSceneAsync(sceneReference, LoadSceneMode.Additive);
        while (!newLevelLoad.IsDone)
        {
            yield return null;
        }
        
        SceneManager.SetActiveScene(newLevelLoad.Result.Scene);

        AsyncOperation oldLevelUnload = SceneManager.UnloadSceneAsync(currentScene);
        while (!oldLevelUnload.isDone)
        {
            yield return null;
        }
        SetPlayerSpawn(entryWay);
    }

    void SetPlayerSpawn(string entryWayString)
    {
        if (player.TryGetComponent(out RespawnOwner respawnOwner))
        {
            EntryWay entryWayObject = FindObjectsByType<EntryWay>(FindObjectsSortMode.None).FirstOrDefault(
                entryWay => entryWay.EntryWayName == entryWayString);
            if(!entryWayObject)
            {
                Debug.LogError($"No entryway found with name {entryWayString}");
                return;
            }
            respawnOwner.SetRespawnerTransform(entryWayObject.transform, entryWayObject.Offset);
            respawnOwner.Respawn(false);
        }
        transitionCoroutine = null;
    }

    public void StartTransitionCoroutine(AssetReference sceneReference, string entryWay)
    {
        if(transitionCoroutine != null)
            return;

        transitionCoroutine = StartCoroutine(SceneTransition(sceneReference, entryWay));
    }
}
