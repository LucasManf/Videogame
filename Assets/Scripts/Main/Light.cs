using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light : MonoBehaviour
{
    public Light2D luz;
    public float tiempoEntreParpadeos = 0.25f;
    public float duracionParpadeo = 0.01f;

    void Start()
    {
        StartCoroutine(Parpadear());
    }

    IEnumerator Parpadear()
    {
        while (true)
        {
            
            luz.enabled = false;
            yield return new WaitForSeconds(duracionParpadeo*Time.deltaTime);

            luz.enabled = true;
            yield return new WaitForSeconds(duracionParpadeo*Time.deltaTime);

            luz.enabled = false;
            yield return new WaitForSeconds(duracionParpadeo*Time.deltaTime);

            luz.enabled = true;
            yield return new WaitForSeconds(tiempoEntreParpadeos*Time.deltaTime);
        }
    }
}