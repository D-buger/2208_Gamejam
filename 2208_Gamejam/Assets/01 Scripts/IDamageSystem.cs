using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDamgeSystem
{
    void DamageToPlayer(Vector2 hitPoint);
}

public class DamageSystem
{
    protected UnityAction deathAction;
    public DamageSystem(UnityAction d) => deathAction = d; 
}

public class JustDamage : DamageSystem, IDamgeSystem
{
    public JustDamage(UnityAction d) : base(d) { }
    public void DamageToPlayer(Vector2 hitPoint)
    {
        if (!SystemManager.Instance.IsUseBucket)
        {
            SystemManager.Instance.DisableAllDeathCams();
            deathAction?.Invoke();
            SystemManager.Instance.CastleHit(hitPoint);
            SystemManager.Instance.ReminedHP--;
        }
    }
}

public class InstantDeath : DamageSystem, IDamgeSystem
{
    public InstantDeath(UnityAction d) : base(d) { }
    public void DamageToPlayer(Vector2 hitPoint)
    {
        if (!SystemManager.Instance.IsUseBucket)
        {
            SystemManager.Instance.DisableAllDeathCams();
            deathAction?.Invoke();
            SystemManager.Instance.GameOver();
        }
    }
}

public class NothingHappened : IDamgeSystem
{
    public void DamageToPlayer(Vector2 hitPoint)
    {

    }
}