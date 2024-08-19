using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject Player;
    
    private float LastShoot;
    private int Health = 3;
    private float startPosition;
    public float moveSpeed; // Velocidad de movimiento del enemigo
    private bool movingRight = true; // Dirección inicial de movimiento
    private bool isShooting = false;


    void Start()
    {
        startPosition = transform.position.x;
        movingRight = true;
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!Player.activeSelf) return;

        float distance = Mathf.Abs(Player.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 0.7f)
        {
            isShooting = true;

            Vector3 direction = Player.transform.position - transform.position;
            if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            Shoot();
            LastShoot = Time.time;
        }
        else
        {
            isShooting = false;
        }

        if (!isShooting)
        {
            if (movingRight)
            {
                transform.position += moveSpeed * Time.deltaTime * Vector3.right;
            }
            else
            {
                transform.position += moveSpeed * Time.deltaTime * Vector3.left;
            }

            // Invertir dirección al llegar al borde
            if (transform.position.x > startPosition + 0.5f)
            {
                movingRight = false;
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else if (transform.position.x < startPosition - 0.5f)
            {
                movingRight = true;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }


    private void Shoot()
    {
        Vector3 direction;
        if(transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health = Health - 1;
        if(Health == 0) Destroy(gameObject);
    }
}
