using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{
    [SerializeField] protected SpellStats baseStats;

    public List<SpellStats> alterations = new List<SpellStats>();

    [SerializeField] protected float damage;
    [SerializeField] protected float chargeTime;
    [SerializeField] protected float range;
    [SerializeField] protected float projectileSize;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float projectiles;
    [SerializeField] protected float selfDamage;
    [SerializeField] protected float manaCost;

    protected virtual void Start()
    {
        ApplyAlteration(baseStats);

        for(int i = 0; i < alterations.Count; ++i)
        {
            ApplyAlteration(alterations[i]);
        }
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
}
