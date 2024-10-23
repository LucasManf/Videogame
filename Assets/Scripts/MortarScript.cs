using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarScript : MonoBehaviour
{
    public GameObject mortarShellPrefab;
    public float launchForce = 4.2f;
    public float launchAngle = 45f;
    public GameObject Player;
    
    private float LastShoot;

    void Update()
    {
        if (!Player.activeSelf) return;

        float distance = Mathf.Abs(Player.transform.position.x - transform.position.x);

        if (distance < 4.0f)
        {
            if(Time.time > LastShoot + 2f)
            {
                FireMortar();
                LastShoot = Time.time;

            }
            
        }
    }

    void FireMortar()
    {
        GameObject shell = Instantiate(mortarShellPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = shell.GetComponent<Rigidbody2D>();

        // Calculate launch velocity components
        float radians = launchAngle * Mathf.Deg2Rad;
        float xVelocity = -launchForce * Mathf.Cos(radians);
        float yVelocity = launchForce * Mathf.Sin(radians);

        // Apply initial velocity
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
}