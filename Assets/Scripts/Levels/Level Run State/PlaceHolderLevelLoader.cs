using Level.Registry;
using Level.Rules;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Level.RunState;

namespace Level.Placeholder
{
    public class PlaceHolderLevelLoader : MonoBehaviour
    {
        [SerializeField]
        LevelRegistry LevelRegistry;
        void Start()
        {
            LevelRunState.Instance.currentLevelId = "Level 1";
            LevelRunState.Instance.currentMode = Level.Rules.LevelRulesData.LevelModeType.Vanilla;

            var rulesRef = LevelRegistry.GetRulesReference(LevelRunState.Instance.currentLevelId, LevelRunState.Instance.currentMode);
            Addressables.LoadAssetAsync<LevelRules>(rulesRef).Completed += OnRulesLoaded;
        }

        void OnRulesLoaded(AsyncOperationHandle<LevelRules> handle)
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError("Failed to load LevelRules asset.");
                return;
            }
            LevelRulesScript.Instance.LevelRules.SetLevelData(handle.Result.LevelData);
            Addressables.Release(handle);
        }
    }
}