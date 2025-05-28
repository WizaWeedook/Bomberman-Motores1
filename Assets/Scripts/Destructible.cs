using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float tiempoDestruccion = 1f;

    private void Start()
    {
        Destroy(gameObject, tiempoDestruccion);
    }

}
