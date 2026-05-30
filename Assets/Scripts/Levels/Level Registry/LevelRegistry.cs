using UnityEngine;
using System.Collections.Generic;
using Level.RulesEntry;
using UnityEngine.AddressableAssets;
using System.Linq;
using Level.Rules;

namespace Level.Registry
{
    [CreateAssetMenu(fileName = "LevelRegistry", menuName = "Levels/Registry")]
    public class LevelRegistry : ScriptableObject
    {
        [SerializeField]
        List<LevelRulesEntry> entries;

        public AssetReference GetRulesReference(string id, LevelRulesData.LevelModeType levelMode)
        {
            var entry = entries.FirstOrDefault(e => e.levelID == id);
            return levelMode switch
            {
                LevelRulesData.LevelModeType.Challenge => entry.challengeRules,
                LevelRulesData.LevelModeType.Vanilla => entry.vanillaRules,
                LevelRulesData.LevelModeType.Custom => entry.customRules,
                _ => entry.vanillaRules
            };
        }
    }
}
