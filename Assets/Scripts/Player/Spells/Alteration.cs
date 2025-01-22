using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellStats", menuName = "ScriptableObject/SpellStats")]
public class SpellStats : ScriptableObject
{
    public float damage;    
    public float chargeTime;
    public float range;
    public float projectileSize;
    public float projectileSpeed;
    public float projectiles;
    public float selfDamage;
    public float manaCost;
}
