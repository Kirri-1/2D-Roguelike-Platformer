using UnityEngine;
using Singleton.SingletonN;
using Level.Rules;

namespace Level.RunState
{
    public class LevelRunState : Singleton<LevelRunState>
    {
        public string currentLevelId;
        public LevelRulesData.LevelModeType currentMode;
        public LevelRulesData customData;
    }
}
