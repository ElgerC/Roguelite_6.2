using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{
    [SerializeField] protected SpellStats baseStats;

    public List<SpellStats> alterations = new List<SpellStats>();

    [SerializeField] private float damageMult;
    [SerializeField] private float chargeTimeMult;
    [SerializeField] private float rangeMult;
    [SerializeField] private float projectileSizeMult;
    [SerializeField] private float projectileSpeedMult;
    [SerializeField] private float projectilesMult;
    [SerializeField] private float selfDamageMult;
    [SerializeField] private float manaCostMult;


    protected float damage;
    protected float chargeTime;
    protected float range;
    protected float projectileSize;
    protected float projectileSpeed;
    protected float projectiles;
    protected float selfDamage;
    protected float manaCost;

    public PlayerScript playerScript;

    protected virtual void Start()
    {
        ApplyAlteration(baseStats);

        for(int i = 0; i < alterations.Count; ++i)
        {
            ApplyAlteration(alterations[i]);
        }

        MultAplication();
    }

    public void ApplyAlteration(SpellStats curAlt)
    {
        damage += curAlt.damage;
        chargeTime += curAlt.chargeTime;
        range += curAlt.range;
        projectileSize += curAlt.projectileSize;
        projectileSpeed += curAlt.projectileSpeed;
        projectiles += curAlt.projectiles;
        selfDamage += curAlt.selfDamage;
        manaCost += curAlt.manaCost;
    }

    private void MultAplication()
    {
        damage *= damageMult;
        chargeTime *= chargeTimeMult;
        range *= rangeMult;
        projectileSize *= projectileSizeMult;
        projectileSpeed *= projectileSpeedMult;
        projectiles *= projectilesMult;
        selfDamage *= selfDamageMult;
        manaCost *= manaCostMult;
    }
    public virtual void Release()
    {

    }
}
