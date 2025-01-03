using System;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour // , ICombatEntity
{
    protected int _hp;
    protected int _speed;
    protected float _actionGauge;

    public abstract void TakeTurn();
    
    public event Action OnTurnStart;
    public event Action OnTurnEnd;
}
