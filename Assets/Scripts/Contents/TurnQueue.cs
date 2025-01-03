using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;

public class TurnQueue
{
    private Queue<CombatEntity> _combatQueue;

    public TurnQueue()
    {
        _UpdateList();
    }

    public void Enqueue(CombatEntity item)
    {
        _combatQueue.Enqueue(item);
    }

    private void _UpdateList()
    {
        // Speed에 따른 Gauge 설정
        
        // Gauge에 따른 정렬
        //_list.Sort();
    }

    public CombatEntity Dequeue()
    {
        return _combatQueue.Dequeue();
    }
}
