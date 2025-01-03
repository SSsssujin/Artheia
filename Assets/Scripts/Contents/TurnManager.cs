using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class TurnManager
{
    private TurnQueue _queue;

    private List<TurnJob> _jobs = new();

    public TurnManager()
    {
        // Zenject
        _queue = new();
    }

    private async void 전투진행중()
    {
        // var entity = _queue.Dequeue();
        // var turn = entity.TakeTurn();
        // /* 턴 진행 동안 실행될 것
        //  */
        // await turn;
        // _queue.Enqueue(entity);
    }

    private struct TurnJob : IJobParallelFor
    {
        public void Execute(int index)
        {
            
        }
    }
}
