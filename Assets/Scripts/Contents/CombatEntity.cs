using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class ActionGauge
{
    private const int _baseSpeed = 120;
    private const int _baseIncrement = 10;

    public ActionGauge(int speed)
    {
        Gauge = _CalculateInitialGauge(speed);
    }
    
    private int _CalculateInitialGauge(int speed)
    {
        double speedRatio = (double)speed / _baseSpeed;
        int initialGauge = (int)(speedRatio * 50);
        return Math.Clamp(initialGauge, 0, 100);
    }

    // Operator overloading
    public static ActionGauge operator +(ActionGauge gauge, int speed)
    {
        float speedRatio = (float)speed / _baseSpeed;

        int increment = (int)(speedRatio * _baseIncrement);
        gauge.Gauge += increment;
        
        return gauge;
    }
    
    public static implicit operator ActionGauge(int speed)
    {
        return new ActionGauge(speed);
    }
    
    public int Gauge { get; private set; }
}

public /*abstract*/ class CombatEntity
{
    public int Speed;
    public ActionGauge ActionGauge;

    public CombatEntity(int speed)
    {
        Speed = speed;
        ActionGauge = new(Speed);
        
        Debug.Log($"Create : {Speed}, {ActionGauge.Gauge}");
    }
    
    // public abstract UniTask TakeTurn();
}

