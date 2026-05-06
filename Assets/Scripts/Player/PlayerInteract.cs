using System;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInteract : MonoBehaviour
{
    public GameObject interactText;
    public GameObject itemText;

    public GameObject nearestGameObject;

    [Header("Ground check")]
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float GroundRadius = 0.25f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if(other.GetComponent<Item>() != null)
            {
                itemText.SetActive(true);              

            } else
            {
                interactText.SetActive(true);
            }
            
        }

    }


    private void OnInteract()
    {
        Debug.Log("Interactuo");
        CheckInteractables();


    }
    void CheckInteractables()
    {
        nearestGameObject = null; // Reinicia la variable para almacenar el objeto interactuable más cercano
        Collider[] hits = Physics.OverlapSphere(GroundCheck.position, GroundRadius); // Realiza una esfera de colisión para verificar si el jugador está tocando el suelo

        foreach (Collider col in hits)
        {
            if (col.gameObject.tag == "Interactable")
            {
                nearestGameObject = col.gameObject; // Almacena el objeto interactuable más cercano para su uso posterior en la función OnInteract
                break;
            }
        }

        if (nearestGameObject != null)
        {
            var itemInteractuable = nearestGameObject.GetComponent<Interactable>();
            if (itemInteractuable != null)
            {
                itemInteractuable.Use();
                try
                {
                    string item = nearestGameObject.GetComponent<Item>().itemType.ToString();
                    if (item != null)
                        GetComponent<Inventario>().GetItem(item);
                } catch (Exception e)
                {
                    Debug.Log("el objeto no tiene la clase item");
                }
                
                
            }
        }
    }

    //private GameObject GetNearestGameObject()
    //{
    //    GameObject result = null;
    //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out var hit, 3))
    //    {
    //        if(hit.transform.CompareTag("Interactable") || hit.transform.CompareTag("Item"))
    //            result = hit.transform.gameObject;
    //    }
    //    return result;
    //}

    private void OnTriggerExit(Collider other)
    {
        interactText.SetActive(false);
        itemText.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        if (GroundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GroundCheck.position, GroundRadius); // Dibuja una esfera en el editor para visualizar el área de detección del suelo
    }
}
