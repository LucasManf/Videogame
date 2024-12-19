using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GranadeScript : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    private Rigidbody2D Rigidbody2D;
    private float rotationSpeed = 500f;
    private float initialRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.angularVelocity = rotationSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.CompareTag("Player"))
        {
            return; // No explotar si colisiona con el jugador
        }
        if(collision.CompareTag("BossFight"))
        {
            return;
        }

        EnemyScript enemy = collision.GetComponent<EnemyScript>();
        TankScript tank = collision.GetComponent<TankScript>();


        if(enemy != null)
        {

            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if(tank != null)
        {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
        
    }

}
