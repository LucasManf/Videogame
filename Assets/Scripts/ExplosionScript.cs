using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public Collider2D explosionCollider;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        EnemyScript enemy = collision.GetComponent<EnemyScript>();
        StaticEnemyScript staticEnemy = collision.GetComponent<StaticEnemyScript>();
        MortarScript mortar = collision.GetComponent<MortarScript>();
        TankScript tank = collision.GetComponent<TankScript>();
        FinalBossScript finalBoss = collision.GetComponent<FinalBossScript>();

        if(player != null)
        {
            player.DealDamage();
        }

        if(enemy != null)
        {
            enemy.Hit();
            enemy.Hit();
        }

        if(staticEnemy != null)
        {
            staticEnemy.Hit();
            staticEnemy.Hit();
        }

        if(mortar != null)
        {
            mortar.Hit();
        }

        if(tank != null)
        {
            tank.TakeHit();
        }
        
        if(finalBoss != null)
        {
            finalBoss.Hit();
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void DissableCollider() {
        explosionCollider.enabled = false;
    }

}
