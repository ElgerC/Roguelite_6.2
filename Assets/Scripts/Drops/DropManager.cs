using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
    }

    public SpellStats GenerateAlteration()
    {
        int choose = Random.Range(-1, 2);
        SpellStats stats = new SpellStats();

        stats.damage = 1 * choose;

        choose = Random.Range(-1, 2);
        stats.chargeTime = 0.5f * choose;

        choose = Random.Range(-1, 2);
        stats.range = 3 * choose;
        
        choose = Random.Range(-1, 2);
        stats.projectileSize = 3 * choose;

        choose = Random.Range(-1, 2);
        stats.projectileSpeed = 1 * choose;

        choose = Random.Range(0, 2);
        stats.projectiles = 1 * choose;

        choose = Random.Range(-1, 2);
        stats.selfDamage = 1 * choose;

        choose = Random.Range(-1, 2);
        stats.manaCost = 10 * choose;

        return stats;
    }

}
