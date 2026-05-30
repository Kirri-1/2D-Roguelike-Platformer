using Singleton.HalfSingleton;
using UnityEngine;

namespace Level.Rules
{
    public class LevelRulesScript : HalfSingleton<LevelRulesScript>
    {
        [SerializeField]
        LevelRules _levelRules;
        public LevelRules LevelRules => _levelRules;
        protected override void Awake()
        {
            base.Awake();
            _levelRules = Instantiate(_levelRules);
        }
    }
}
