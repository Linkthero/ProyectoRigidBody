using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private Vector3 destination;
    [SerializeField] private int childrenIndex;
    public float speed = 2f; // Velocidad de movimiento
    [SerializeField] private Transform camino;
    private Rigidbody rb;

    [SerializeField] private Coroutine runningPatroll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destination = camino.GetChild(0).position; // Establece el destino inicial al primer punto del camino
        rb = GetComponent<Rigidbody>(); // Obtiene el componente Rigidbody para el movimiento físico

        rb.MovePosition(destination); // Mueve el objeto al destino inicial utilizando Rigidbody para un movimiento físico suave
        runningPatroll = StartCoroutine(Patroll());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Patroll()
    {
        Debug.Log("Empieza a andar");
        while (true)
        {
            if (Vector3.Distance(transform.position, destination) < 1f)
            {Debug.Log("a");
                childrenIndex++;
                childrenIndex = childrenIndex % camino.childCount;

                destination = camino.GetChild(childrenIndex).position;
                rb.MovePosition(Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime)); // Mueve el objeto hacia el destino utilizando Rigidbody para un movimiento físico suave
                //GetComponent<NavMeshAgent>().SetDestination(destination);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
