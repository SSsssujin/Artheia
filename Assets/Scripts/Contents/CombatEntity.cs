using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using Progress = UnityEditor.Progress;

namespace Artheia
{
    public /*abstract*/ class CombatEntity
    {
        private const float _maxActionGauge = 100f;

        public string Id;
        public int Priority;
        public int Speed;
        public ActionGauge ActionGauge;
        
        private bool _isMyTurn;

        public CombatEntity(string id, int priority, int speed)
        {
            Id = id;
            Priority = priority;
            Speed = speed;
            ActionGauge = new(Speed);
        }

        public void StartTurn()
        {
            _isMyTurn = true;
        }

        public void TakeTurn()
        {
            if (_isMyTurn)
                EndTurn();
            else
                PassTurn();
        }

        public void EndTurn()
        {
            ActionGauge = Speed;
            _isMyTurn = false;
        }

        public void PassTurn()
        {
            ActionGauge++;
        }

        public async UniTask ProcessTurn()
        {
            
        }
    }
}