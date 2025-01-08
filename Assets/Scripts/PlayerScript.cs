using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour,IDamagabele
{
    [SerializeField] private int health;
    [SerializeField] private int imunityDuration;
    private bool canTakeDmg = true;
    public void TakeDamage(int amount)
    {
        if (canTakeDmg)
        {
            health -= amount;
            StartCoroutine(ImunityTimer());
        }
    }

    private IEnumerator ImunityTimer()
    {
        canTakeDmg = false;
        yield return new WaitForSeconds(imunityDuration);
        canTakeDmg = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDropable dropable = collision.gameObject.GetComponent<IDropable>();
        
        if(dropable != null )
        {
            dropable.OnCollect();
        }    
        
    }
}
