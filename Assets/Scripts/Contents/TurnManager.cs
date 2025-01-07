using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Jobs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Artheia
{
    // Turn System
    public partial class TurnManager : Singleton<TurnManager>
    {
        private bool _isBattleOngoing => !Input.GetKeyDown(KeyCode.Escape);
        private bool _isTurnEnd => Input.GetKeyDown(KeyCode.Space);

        private void Start()
        {
 
        }

        public void Initialize()
        {
            _combatQueue = new SortedSet<CombatEntity>(new CombatEntityComparer());
            
            // Tester
            for (int i = 0; i < 5; i++)
            {
                CombatEntity ce = new(i.ToString(), i, 100 + i);
                Debug.Log(ce.Id + ", " + 
                          ce.Priority + ", " + 
                          ce.ActionGauge.Gauge + 
                          " Add : " + _combatQueue.Add(ce));
            }
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
                {
                    StringBuilder debug = new();
                    debug.AppendLine(
                        $"{entity.Id} Battle started. " +
                        $"Speed : [{entity.Speed}], " +
                        $"ActionGauge : [{entity.ActionGauge.Gauge}]");
                    debug.AppendLine("===== Remained list =====");
                    foreach (var e in _combatQueue)
                    {
                        debug.AppendLine($"{e.Id}, Speed : [{e.Speed}], ActionGauge : [{e.ActionGauge.Gauge}]");
                    }
                    Debug.Log(debug);
                }
                
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                
                // End
                entity.EndTurn();
                _Enqueue(entity);
            }
            
            Debug.Log("End");
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
    public partial class TurnManager
    {
        private SortedSet<CombatEntity> _combatQueue;

        private void _Enqueue(CombatEntity item)
        {
            _combatQueue.Add(item);
        }

        private CombatEntity _Dequeue()
        {
            if (_combatQueue.Count == 0) 
                throw new InvalidOperationException("Queue is empty");

            CombatEntity nextEntity = _combatQueue.Min;
            _combatQueue.Remove(nextEntity);
            return nextEntity;
        }

        private class CombatEntityComparer : IComparer<CombatEntity>
        {
            public int Compare(CombatEntity x, CombatEntity y)
            {
                if (x?.ActionGauge > y?.ActionGauge) return -1;
                if (x?.ActionGauge < y?.ActionGauge) return 1;
                return x.Priority.CompareTo(y.Priority);
            }
        }
    }
}
