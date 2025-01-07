using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Artheia
{
    public class GameManager : Singleton<GameManager>
    {
        private readonly ResourceManager _resource = new();
    
        private void Start()
        {
            TurnManager.Instance.Initialize();
        }

        public static ResourceManager Resource => Instance._resource;
    }
}
