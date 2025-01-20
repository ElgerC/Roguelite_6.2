using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public int damage;

    //When a object goes thru the colider the object takes damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagabele Idmg = collision.GetComponent<IDamagabele>();
        
        if(Idmg != null)
        {
            Idmg.TakeDamage(damage);
        }
    }
}
