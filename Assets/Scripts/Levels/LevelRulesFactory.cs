using static Level.Rules.LevelRulesData;

namespace Level.Rules
{
    public static class LevelRulesFactory
    {
        public static LevelRulesData CreateCustomRules(LevelRulesData.AllowedFeatures allowedFeatures,
            ModifyMovementStruct modifyMovement,
            int checkPointAmount, int completionAmount)
        {
            return CreateRules(
                 LevelRulesData.LevelModeType.Custom,
                 allowedFeatures,
                modifyMovement,
                checkPointAmount,
                completionAmount);
        }

        public static LevelRulesData CreateChallengeRules(LevelRulesData.AllowedFeatures allowedFeatures,
            ModifyMovementStruct modifyMovement,
            int checkPointAmount, int completionAmount)
        {
            return CreateRules(
                 LevelRulesData.LevelModeType.Challenge,
                 allowedFeatures,
                modifyMovement,
                checkPointAmount,
                completionAmount);
        }

        public static LevelRulesData CreateVanillaRules(LevelRulesData.AllowedFeatures allowedFeatures,
            ModifyMovementStruct modifyMovement,
            int checkPointAmount, int completionAmount)
        {
            return CreateRules(
                 LevelRulesData.LevelModeType.Vanilla,
                 allowedFeatures,
                modifyMovement,
                checkPointAmount,
                completionAmount);
        }
        static LevelRulesData CreateRules(LevelRulesData.LevelModeType mode, LevelRulesData.AllowedFeatures _allowedFeatures,
            ModifyMovementStruct modifyMovement,
            int checkPointAmount, int completionAmount)
        {
            return new LevelRulesData
            {
                levelMode = mode,
                allowedFeatures = _allowedFeatures,
                modifyMovementStruct = modifyMovement,
                checkPointCurrencyAmount = checkPointAmount,
                completionCurrencyAmount = completionAmount
            };
        }
    }
}