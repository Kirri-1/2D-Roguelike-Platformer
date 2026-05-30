using UnityEngine;
using Player.Movement.Structs;

namespace Level.Rules
{
    [CreateAssetMenu(fileName = "LevelRules", menuName = "Levels/LevelRules")]
    public class LevelRules : ScriptableObject
    {
        [SerializeField]
        LevelRulesData _levelData;

        public LevelRulesData LevelData => _levelData;
        public void SetLevelData(LevelRulesData levelData)
        {
            _levelData = levelData;
        }
    }
    [System.Serializable]
    public struct LevelRulesData
    {
        public enum LevelModeType
        {
            Vanilla,
            Challenge,
            Custom
        }
        [System.Flags]
        public enum AllowedFeatures
        {
            None = 0,
            AllowTempBuffs = 1 << 0,
            AllowPermanentBuffs = 1 << 1,
            AllowPowerUps = 1 << 2,
            AllowDash = 1 << 3,
            AllowBlink = 1 << 4,
            AllowJump = 1 << 5,
            All = ~0
        }
        public ModifyMovementStruct modifyMovementStruct;
        public LevelModeType levelMode;
        public AllowedFeatures allowedFeatures;
        public int checkPointCurrencyAmount;
        public int completionCurrencyAmount;
    }

    [System.Serializable]
    public struct ModifyMovementStruct
    {
        public BlinkData blinkData;
        public DashData dashData;
        public JumpData jumpData;
        public MovementData movementData;
    }
}
