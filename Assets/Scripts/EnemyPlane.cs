using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{

    public GameObject EnemyBulletPrefab;
    public GameObject Player;
    
    private float LastShoot;
    private int Health = 3;
    private float startPosition;
    public float moveSpeed; // Velocidad de movimiento del enemigo



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > LastShoot + 0.2f)
        {
            EnemyShoot();
            LastShoot = Time.time; 
        }
    }

    private void EnemyShoot()
    {
        Vector3 direction = Vector3.left;

        GameObject bullet = Instantiate(EnemyBulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<EnemyBulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health--;
        if(Health == 0) Destroy(gameObject);
    }
}
