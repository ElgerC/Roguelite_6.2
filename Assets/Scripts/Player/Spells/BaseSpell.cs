using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{
    [SerializeField] protected SpellStats baseStats;

    public List<SpellStats> alterations = new List<SpellStats>();

    [Header("Multipliars")]
    [SerializeField] private float damageMult;
    [SerializeField] private float chargeTimeMult;
    [SerializeField] private float rangeMult;
    [SerializeField] private float projectileSizeMult;
    [SerializeField] private float projectileSpeedMult;
    [SerializeField] private float projectilesMult;
    [SerializeField] private float selfDamageMult;
    [SerializeField] private float manaCostMult;

    [Header("Stats")]
    [SerializeField] protected float damage;
    [SerializeField] protected float chargeTime;
    [SerializeField] protected float range;
    [SerializeField] protected float projectileSize;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float projectiles;
    public float selfDamage;
    public float manaCost;

    public PlayerScript playerScript;

    protected virtual void Awake()
    {
        ApplyAlteration(baseStats);

        for(int i = 0; i < alterations.Count; ++i)
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
        projectiles += curAlt.projectiles * projectilesMult;
        selfDamage += curAlt.selfDamage * selfDamageMult;
        manaCost += curAlt.manaCost * manaCostMult;
    }
    public virtual void Release()
    {

    }
}
