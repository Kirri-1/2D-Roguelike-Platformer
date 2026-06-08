namespace DebugN
{
    public static class DebugMode
    {
        public static bool DebugModeActive =
#if UNITY_EDITOR
            true;
#else
        false;
#endif
    }
}
