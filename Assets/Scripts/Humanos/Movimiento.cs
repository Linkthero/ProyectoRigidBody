using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private Vector3 destination;
    [SerializeField] private int childrenIndex;
    public float speed = 2f; // Velocidad de movimiento
    [SerializeField] private Transform[] waypoints;
    private Rigidbody rb;

    [SerializeField] private Coroutine runningPatroll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destination = waypoints[0].position; // Establece el destino inicial al primer punto del camino
        rb = GetComponent<Rigidbody>(); // Obtiene el componente Rigidbody para el movimiento físico

        rb.MovePosition(destination); // Mueve el objeto al destino inicial utilizando Rigidbody para un movimiento físico suave
        runningPatroll = StartCoroutine(Patroll());
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = (destination - transform.position).normalized * speed; // Calcula la dirección hacia el destino y aplica la velocidad al Rigidbody para un movimiento físico suave
        Vector3 direccion = destination - transform.position; // Calcula la dirección hacia el destino

        Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);

        // 3. Suavizar la rotación (Quaternion.Slerp)
        Quaternion rotacionFinal = Quaternion.Slerp(rb.rotation, rotacionDeseada, 2 * Time.fixedDeltaTime);

        // 4. Aplicar al Rigidbody
        rb.MoveRotation(rotacionFinal);
    }

    IEnumerator Patroll()
    {
        Debug.Log("Empieza a andar");
        while (true)
        {
            if (Vector3.Distance(transform.position, destination) < 1f)
            {Debug.Log("a");
                childrenIndex++;
                childrenIndex = childrenIndex % waypoints.Length;

                destination = waypoints[childrenIndex].position;
                rb.MovePosition(transform.position); // Mueve el objeto hacia el destino utilizando Rigidbody para un movimiento físico suave
                //GetComponent<NavMeshAgent>().SetDestination(destination);
            }
            yield return new WaitForSeconds(1);
        }
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
