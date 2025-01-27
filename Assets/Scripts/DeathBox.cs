using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private bool active = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            IDamagabele Idmg = collision.GetComponent<IDamagabele>();

            if (Idmg != null)
            {
                Idmg.TakeDamage(1000);
            }
        } else
        {
            active = true;
        }
    }
}
