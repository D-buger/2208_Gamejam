using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamgeSystem
{
    void DamageToPlayer();
}

public class JustDamage : IDamgeSystem
{
    public void DamageToPlayer()
    {
        if (!SystemManager.Instance.IsUseBucket)
        {
            SystemManager.Instance.ReminedHP--;
        }
    }
}

public class InstantDeath : IDamgeSystem
{
    public void DamageToPlayer()
    {
        if (!SystemManager.Instance.IsUseBucket)
        {
            SystemManager.Instance.GameOver();
        }
    }
}

public class NothingHappened : IDamgeSystem
{
    public void DamageToPlayer()
    {

    }
}