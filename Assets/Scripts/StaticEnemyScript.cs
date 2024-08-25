using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyScript : MonoBehaviour
{
    public GameObject EnemyBulletPrefab;
    public GameObject Player;
    
    private float LastShoot;
    private int Health = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.activeSelf) return;

        float distance = Mathf.Abs(Player.transform.position.x - transform.position.x);

        if (distance < 6.0f)
        {
            if(Time.time > LastShoot + 1.5f)
            {

                EnemyShoot();
                LastShoot = Time.time;

            }
        }
    }

    private void EnemyShoot()
    {
        Vector3 direction;
        direction = Vector2.left;

        GameObject bullet = Instantiate(EnemyBulletPrefab, transform.position + direction * 0.2f, Quaternion.identity);
        bullet.transform.localScale = new Vector3(-1, 1, 1);
        bullet.GetComponent<EnemyBulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health = Health - 1;
        if(Health == 0) Destroy(gameObject);
    }
}
