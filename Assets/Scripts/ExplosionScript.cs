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

        if(player != null)
        {
            player.DealDamage();
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
