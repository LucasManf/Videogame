using System.Collections;
using UnityEngine;

public class FinalBossScript : MonoBehaviour
{
    public enum bossStates { shooting, hurt, moving, ended };

    public bossStates currentStates;

    public GameObject EnemyBulletPrefab;
    public GameObject MissilePrefab;
    public GameObject Player;
    public GameObject deathEffect;

    private float LastShoot;
    public int Health = 15;
    private bool isShooting = false;
    private Animator Animator;

    // Movimiento
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight = true; // Empezamos moviéndonos a la derecha

    // Estado de daño
    private int hitsReceived = 0; // Contador de golpes

    public Collider2D hitBox;

    public Transform boss;

    private Coroutine currentAttackCoroutine; // Para almacenar la referencia de la corutina de disparo

    void Start()
    {
        currentStates = bossStates.shooting; // El estado inicial es "shooting"
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.activeSelf) return;

        Animator.SetBool("Shooting", isShooting != false);

        switch (currentStates)
        {
            case bossStates.shooting:
                
                // Lógica de disparo
                if (Time.time > LastShoot + 3f && currentStates == bossStates.shooting) // Solo dispara si no está moviéndose
                {
                    isShooting = true;
                    currentAttackCoroutine = StartCoroutine(SpecialAttack());  // Iniciar la corutina de disparo
                    LastShoot = Time.time;
                }

                if (hitsReceived >= 2) // Moverse después de recibir 2 golpes
                {
                    currentStates = bossStates.moving;  // Cambia al estado de movimiento
                    /* hitBox.enabled = false;  */ // Inmunidad durante el movimiento
                    hitsReceived = 0; // Reseteamos el contador de golpes después de moverse
                }
                break;

            case bossStates.moving:
                // Lógica de movimiento
                Move();
                break;

            case bossStates.ended:
                // Lógica cuando el jefe es derrotado
                break;
        }
    }

    private void Move()
    {
        Animator.SetBool("Walking", true);
        // Si el jefe se está moviendo, cancela cualquier disparo en curso
        if (currentAttackCoroutine != null)
        {
            StopCoroutine(currentAttackCoroutine); // Detenemos la corutina de disparo
            isShooting = false; // Aseguramos que no se siga disparando
        }

        // Determinamos si el jefe debe moverse hacia la derecha o hacia la izquierda
        if (moveRight)
        {
            boss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            if (boss.position.x > rightPoint.position.x)
            {
                boss.localScale = new Vector3(0.6723f, 0.6723f, 0.6723f); // Dirección derecha
                moveRight = false;
                EndMovement();
            }
        }
        else
        {
            boss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            if (boss.position.x < leftPoint.position.x)
            {
                boss.localScale = new Vector3(-0.6723f, 0.6723f, 0.6723f); // Dirección izquierda
                moveRight = true;
                EndMovement();
            }
        }
    }

    private IEnumerator SpecialAttack()
{
    // Dirección base según la escala del jefe
    Vector2 baseDirection = transform.localScale.x > 0 ? Vector2.left : Vector2.right; // Derecha si escala x > 0, izquierda si x < 0
    float[] angles = { 0, 15, 30, 45 }; // Ángulos de disparo

    for (int i = 0; i < angles.Length; i++)
    {
        // Calcular la dirección ajustada con el ángulo
        Vector2 shootDirection = Quaternion.Euler(0, 0, angles[i] * (baseDirection.x > 0 ? 1 : -1)) * baseDirection;

        // Disparar dos balas por cada ángulo
        for (int j = 0; j < 2; j++)
        {
            // Crear la bala
            GameObject bullet = Instantiate(EnemyBulletPrefab, transform.position + (Vector3)baseDirection * 0.1f, Quaternion.identity);
            bullet.transform.localScale = new Vector3(-1, 1, 1); // Invertir la escala según la dirección
            bullet.GetComponent<EnemyBulletScript>().SetDirection(shootDirection);

            // Esperar antes de disparar la siguiente bala
            yield return new WaitForSeconds(0.1f);
        }

        // Esperar antes de disparar las siguientes balas
        yield return new WaitForSeconds(0.2f);
    }

    isShooting = false; // Se pone a false cuando termina de disparar
}

    public void Hit()
    {
        // Solo contar el golpe si no está en el estado de movimiento
        if (currentStates == bossStates.moving) return;

        hitsReceived++; // Aumentamos el contador de golpes
        Health--;

        if (Health <= 0)
        {
            // Aquí se manejaría la lógica cuando el jefe muere, por ejemplo:
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void EndMovement()
    {
        Animator.SetBool("Walking", false);
        currentStates = bossStates.shooting; // Después de moverse, volvemos al estado de disparo
        /* hitBox.enabled = true;  */// Habilitamos la colisión nuevamente
    }

    public void MissileAttack()
    {
        // Altura fija sobre el jefe
        float heightOffset = 8.5f;

        // Objeto a instanciar
        GameObject prefab = MissilePrefab;

        // Calcula la posición sobre el jefe
        Vector3 spawnPosition = transform.position + new Vector3(0f, heightOffset, 0f);

        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, -90f);

        // Instancia el prefab con la rotación especificada
        Instantiate(prefab, spawnPosition, spawnRotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if(player != null)
        {
            player.DealDamage();
        }
    }
}
