using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0f, 0f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            PlayerHealth.instance.DealDamage();
            Destroy(gameObject);

        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
