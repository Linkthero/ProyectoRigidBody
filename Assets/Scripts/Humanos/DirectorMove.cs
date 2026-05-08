using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private Vector3 destination;
    public float speed = 2f; // Velocidad de movimiento
    [SerializeField] private Transform waypoint;
    public bool incendio;

    [SerializeField] private GameObject llave;
    private Animator animator;
    public GameObject cuerpo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destination = waypoint.position; // Establece el destino inicial al primer punto del camino
        incendio = false;
        animator = GetComponent<Animator>(); // Obtiene el componente Animator para controlar las animaciones
        cuerpo.transform.rotation = Quaternion.Euler(-90f, 90f, 0); // Asegura que el cuerpo del personaje siempre mire hacia adelante (180 grados en el eje Y)
    }

    // Update is called once per frame
    void Update()
    {
        if (incendio)
        {
            incendio = false;
            StartCoroutine(IrAIncendio());
        }

        if(Vector3.Distance(transform.position, destination) < 1f)
        {
            animator.SetInteger("arms", 34);
            animator.SetInteger("legs", 36);
        }
        if(llave != null)
        {
            if (Vector3.Distance(transform.position, llave.transform.position) > 3f)
            {
                llave.GetComponent<Collider>().enabled = true;
                llave = null;
            }
        }
            
    }

    IEnumerator IrAIncendio()
    {
        animator.SetInteger("legs", 1);
        gameObject.GetComponent<Dialogo>().enabled = false; // Desactiva el componente de diálogo para evitar que se muestre el mensaje de que el personaje no puede ir al incendio
        GetComponent<NavMeshAgent>().SetDestination(destination);
        yield return null;
    }

    public void OnTriggerEnter(Collider other)
    {
        Door puerta = other.GetComponent<Door>();
        if (puerta != null)
        {
            if(!puerta.abierta)
            {
                puerta.Use();
            }
        }
    }
}
