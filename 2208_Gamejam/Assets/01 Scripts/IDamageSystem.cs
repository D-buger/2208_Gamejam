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
        Debug.Log("Damge 1");
    }
}

public class InstantDeath : IDamgeSystem
{
    public void DamageToPlayer()
    {
        Debug.Log("GameOver");
    }
}