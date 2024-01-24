using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour, ICollectable
{
    /**
     * TODO:
     * La clase Currency representa a las monedas que el jugador puede recolectar. Serán usadas para comprar objetos y otros beneficios en tiendas
     */

    public void Collect()
    {
        Debug.Log("Currency collected");
    }
}
