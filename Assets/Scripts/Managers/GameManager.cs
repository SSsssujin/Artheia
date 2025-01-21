using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Artheia
{
    public class GameManager : Singleton<GameManager>
    {
        private readonly ResourceManager _resource = new();
        private readonly BattleManager _battle = new();
    
        private void Start()
        {
            //TurnController.Instance.Initialize();
        }

        public static ResourceManager Resource => Instance._resource;
        public static BattleManager Battle => Instance._battle;
    }
}
