using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager instance;

    [SerializeField] private List<GameObject> drops = new List<GameObject>();

    [SerializeField] private GameObject alteration;

    [Header("ChanceDrop")]
    [SerializeField] private float[] baseDropChance;
    [SerializeField] private float[] dropChance;
    [SerializeField] private float[] chanceAdition;



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

        stats.damage = choose;

        choose = Random.Range(-1, 2);
        stats.chargeTime = choose;

        choose = Random.Range(-1, 2);
        stats.range = choose;
        
        choose = Random.Range(-1, 2);
        stats.projectileSize = choose;

        choose = Random.Range(-1, 2);
        stats.projectileSpeed = choose;

        choose = Random.Range(0, 2);
        stats.projectiles = choose;

        choose = Random.Range(-1, 2);
        stats.selfDamage = choose;

        choose = Random.Range(-1, 2);
        stats.manaCost = choose;

        return stats;
    }

    public GameObject GenerateDrop()
    {
        GameObject drop = null;

        for(int i = 0; i < drops.Count; i++)
        {
            float chance = Random.Range(0, 100);

            if(chance <= dropChance[i])
            {
                dropChance[i] = baseDropChance[i];
                drop = drops[i];
            } else
            {
                dropChance[i] += chanceAdition[i];
            }
        }

        return drop;
    }
}
