namespace Level.Rules
{
    public static class LevelRulesFactory
    {
        public static LevelRulesData CreateCustomRules(LevelRulesData.AllowedFeatures _allowedFeatures,
            ModifyMovementStruct modifyMovement,
            int checkPointAmount, int completionAmount)
        {
            return new LevelRulesData
            {
                levelMode = LevelRulesData.LevelModeType.Custom,
                allowedFeatures = _allowedFeatures,
                modifyMovementStruct = modifyMovement,
                checkPointCurrencyAmount = checkPointAmount,
                completionCurrencyAmount = completionAmount
            };
        }

        public static LevelRulesData CreateChallengeRules(LevelRulesData.AllowedFeatures _allowedFeatures,
            ModifyMovementStruct modifyMovement,
            int checkPointAmount, int completionAmount)
        {
            return new LevelRulesData
            {
                levelMode = LevelRulesData.LevelModeType.Challenge,
                allowedFeatures = _allowedFeatures,
                modifyMovementStruct = modifyMovement,
                checkPointCurrencyAmount = checkPointAmount,
                completionCurrencyAmount = completionAmount
            };
        }

        public static LevelRulesData CreateVanillaRules(LevelRulesData.AllowedFeatures _allowedFeatures,
            ModifyMovementStruct modifyMovement,
            int checkPointAmount, int completionAmount)
        {
            return new LevelRulesData
            {
                levelMode = LevelRulesData.LevelModeType.Vanilla,
                allowedFeatures = _allowedFeatures,
                modifyMovementStruct = modifyMovement,
                checkPointCurrencyAmount = checkPointAmount,
                completionCurrencyAmount = completionAmount
            };
        }
    }
}