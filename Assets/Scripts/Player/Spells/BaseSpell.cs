using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{
    [SerializeField] public SpellStats baseStats;

    public List<SpellStats> alterations = new List<SpellStats>();

    [Header("Multipliars")]
    [SerializeField] private float damageMult;
    [SerializeField] private float chargeTimeMult;
    [SerializeField] private float rangeMult;
    [SerializeField] private float projectileSizeMult;
    [SerializeField] private float projectileSpeedMult;
    [SerializeField] private float projectilesMult;
    [SerializeField] private int selfDamageMult;
    [SerializeField] private float manaCostMult;

    [Header("Stats")]
    [SerializeField] protected float damage;
    public float chargeTime;
    [SerializeField] protected float range;
    [SerializeField] public float projectileSize;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] public float projectiles;
    public int selfDamage;
    public float manaCost;

    public PlayerScript playerScript;

    public float slow = 0;
    public void GetAlterations()
    {
        ApplyAlteration(baseStats);
        for (int i = 0; i < alterations.Count; ++i)
        {
            ApplyAlteration(alterations[i]);
        }
    }

    public void ApplyAlteration(SpellStats curAlt)
    {
        damage += curAlt.damage * damageMult;
        chargeTime += curAlt.chargeTime * chargeTimeMult;
        range += curAlt.range * rangeMult;
        projectileSize += curAlt.projectileSize * projectileSizeMult;
        projectileSpeed += curAlt.projectileSpeed * projectileSpeedMult;
        if ((projectiles + curAlt.projectiles * projectilesMult) > 0)
            projectiles += curAlt.projectiles * projectilesMult;
        selfDamage += curAlt.selfDamage * selfDamageMult;
        manaCost += curAlt.manaCost * manaCostMult;
    }
    public virtual void Release()
    {

    }
}
