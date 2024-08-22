using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRespawn : MonoBehaviour
{
    public Vector3 respawnPoint;

    void Start()
    {
        respawnPoint = transform.position;
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        gameObject.SetActive(true);
        /* Time.timeScale = 1; */
    }
}