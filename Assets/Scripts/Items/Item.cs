using System;
using UnityEngine;
[System.Serializable]
public enum Items
{
    KeyDespacho,
    KeySalida,
    Destornillador,
    Mechero,
    Libros,
    Tarjeta
}
public class Item : MonoBehaviour,Interactable
{
    public Items itemType;
    public GameObject icono;
    public GameObject txtCoger;
    public bool colliderActivoAlInicio;

    private void Start()
    {
        GetComponent<Collider>().enabled = colliderActivoAlInicio;
    }

    public void Use()
    {
        
        //gameObject.SetActive(false);
        txtCoger.SetActive(false);
        icono.SetActive(true);
        Destroy(gameObject);
    }
}
