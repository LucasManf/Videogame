using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto tocó el piso
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero; // Elimina la velocidad
            rb.angularVelocity = 0f; // Detiene la rotación
            rb.constraints = RigidbodyConstraints2D.FreezeAll; // Congela su posición y rotación
  
        }
    }
}
