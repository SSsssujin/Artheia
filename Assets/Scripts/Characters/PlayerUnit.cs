using Unity.Jobs;
using UnityEngine;

public abstract class PlayerUnit : UnitBase
{
    protected abstract void BaseAttack();
    protected abstract void PowerAttack();
    protected abstract void GaugeAttack();
    protected abstract void UltimateAttack();
}
