using UnityEngine;

namespace CodeBase.Options
{
    [CreateAssetMenu(menuName = nameof(CodeBase) + "/" + nameof(Options) + "/" + nameof(GameplayOptionsSO))]
    public class GameplayOptionsSO : ScriptableObject
    {
        [SerializeField] private int _enemyCount; 
        
        public int EnemyCount 
        { 
            get => _enemyCount; 
            set
            {
                _enemyCount = Mathf.Max(value, 1);
            }
        }
    }
}