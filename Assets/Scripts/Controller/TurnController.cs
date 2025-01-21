using System;
using System.Collections;
using System.Collections.Generic;
using Artheia.CombatUnit;
using Unity.Jobs;
using UnityEngine;

namespace Artheia
{
    // Turn System
    public partial class TurnController
    {
        private bool _isBattleOngoing => !Input.GetKeyDown(KeyCode.Escape);
        private bool _isTurnEnd => Input.GetKeyDown(KeyCode.Space);

        private void Start()
        {
 
        }

        public void Initialize()
        {
            _combatQueue = new SortedSet<CombatUnitBase>(new CombatEntityComparer());
            
            GameManager.Instance.StartCoroutine(_cProcessBattle());
        }
    
        private IEnumerator _cProcessBattle()
        {
            // Tester
            while (_isBattleOngoing)
            {
                // Start
                var entity = _Dequeue();
                entity.StartTurn();
                OnTurnStarted?.Invoke();
                
                // 추후 수정
                //var turn = entity.TakeTurn();
                //await turn;
                
                // Tester
                // {
                //     StringBuilder debug = new();
                //     debug.AppendLine(
                //         $"{entity.Id} Battle started. " +
                //         $"Speed : [{entity.Speed}], " +
                //         $"ActionGauge : [{entity.ActionGauge.Gauge}]");
                //     debug.AppendLine("===== Remained list =====");
                //     foreach (var e in _combatQueue)
                //     {
                //         debug.AppendLine($"{e.Id}, Speed : [{e.Speed}], ActionGauge : [{e.ActionGauge.Gauge}]");
                //     }
                //     Debug.Log(debug);
                // }
                
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                
                // End
                entity.EndTurn();
                _Enqueue(entity);
            }
        }
        
        public delegate void TurnEventHandler();
        public TurnEventHandler OnTurnStarted;
    
        private struct TurnJob : IJobParallelFor
        {
            public void Execute(int index)
            {
                
            }
        }
    }

    // PriorityQueue
    public partial class TurnController
    {
        private SortedSet<CombatUnitBase> _combatQueue;

        private void _Enqueue(CombatUnitBase item)
        {
            _combatQueue.Add(item);
        }

        private CombatUnitBase _Dequeue()
        {
            if (_combatQueue.Count == 0) 
                throw new InvalidOperationException("Queue is empty");

            CombatUnitBase nextUnitBase = _combatQueue.Min;
            _combatQueue.Remove(nextUnitBase);
            return nextUnitBase;
        }

        private class CombatEntityComparer : IComparer<CombatUnitBase>
        {
            public int Compare(CombatUnitBase x, CombatUnitBase y)
            {
                if (x?.ActionGauge > y?.ActionGauge) return -1;
                if (x?.ActionGauge < y?.ActionGauge) return 1;
                return x.Priority.CompareTo(y.Priority);
            }
        }
    }
}
