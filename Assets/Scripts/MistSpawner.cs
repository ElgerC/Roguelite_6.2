using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mist;

    [SerializeField] private Vector3 spawnPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(mist,spawnPos,Quaternion.identity);
        Destroy(gameObject);
    }
}
