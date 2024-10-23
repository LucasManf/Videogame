using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShellScript : MonoBehaviour
{
    public float Speed;
    public GameObject ExplosionPrefab;

    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;


    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            return; // No explotar si colisiona con el mortero
        }

        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        
        /* Rigidbody2D.velocity = Vector2.zero;
        Rigidbody2D.angularVelocity = 0f; */

        if(player != null)
        {

            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}