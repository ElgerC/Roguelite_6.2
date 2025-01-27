using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBattleScript : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private Vector3 pos;

    private bool spawned = false;
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (!spawned)
        {
            spawned = true;
            GameObject go = Instantiate(boss, pos, Quaternion.identity);

            Animator ani = go.GetComponent<Animator>();

            GeneralEnemyScript enemy = go.GetComponent<GeneralEnemyScript>();
            enemy.roamPoint = FindObjectOfType<PlayerScript>().gameObject;

            ani.SetTrigger("Entrance");

            Destroy(gameObject);
        }

    }
}
