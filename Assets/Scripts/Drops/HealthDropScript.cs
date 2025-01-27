using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDropScript : MonoBehaviour, IDropable
{
    [SerializeField] private int amount;
    public void OnCollect(PlayerScript playerScript)
    {
        playerScript.TakeDamage(amount);

        Destroy(gameObject);
    }
}
