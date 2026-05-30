using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Level.RulesEntry
{
    [System.Serializable]
    public struct LevelRulesEntry
    {
        public string levelID;
        public AssetReference vanillaRules;
        public AssetReference challengeRules;
        public AssetReference customRules; //blank slate, just easier to do this
    }
}
