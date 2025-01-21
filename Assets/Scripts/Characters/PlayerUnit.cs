using Artheia.CombatUnit;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Artheia
{
    public abstract class PlayerUnit : CombatUnitBase
    {
        private void Start()
        {
            _Initialize();
        }

        
        // [수정] 이후 ResourceManager로
        private void _Initialize()
        {
            var key = $"Magicians/{GetType().Name}.asset";
            Addressables.LoadAssetAsync<CombatUnitData>(key).Completed += OnDataLoaded;
        }
        
        
        private void OnDataLoaded(AsyncOperationHandle<CombatUnitData> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _data = handle.Result;
                Debug.Log($"Loaded data: {_data.name}");
            }
            else
            {
                Debug.LogError("Failed to load the ScriptableObject.");
            }
        }

        //protected abstract void BaseAttack();
        //protected abstract void PowerAttack();
        //protected abstract void GaugeAttack();
        //protected abstract void UltimateAttack();

        public CombatUnitData Data => _data;
    }
}