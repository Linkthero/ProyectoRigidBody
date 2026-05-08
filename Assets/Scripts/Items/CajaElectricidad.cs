using System.Collections;
using UnityEngine;

public class CajaElectricidad : MonoBehaviour, Interactable
{
    public GameObject efecto;
    [Header("Ground check")]
    [SerializeField] private float GroundRadius;
    [SerializeField] private GameObject[] luces;
    public AudioSource audioSource;

    void Start()
    {
        efecto.SetActive(false); // Asegura que el efecto esté desactivado al inicio del juego
    }

    public void Use()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, GroundRadius); // Realiza una esfera de colisión para verificar si el jugador está 

        foreach (Collider col in hits)
        {
            if (col.gameObject.tag == "Player")
            {
                Inventario inv = col.gameObject.GetComponent<Inventario>();
                Debug.Log(inv);
                if (inv.destornillador)
                {
                    gameObject.GetComponent<Dialogo>().enabled = false; // Desactiva el componente de diálogo para evitar que se muestre el mensaje de que el jugador no tiene los libros o el mechero en su inventario
                    efecto.SetActive(true); // Activa el efecto de fuego si el jugador tiene los libros y el mechero en su inventario
                    if(audioSource != null && !audioSource.isPlaying)
                        audioSource.Play();
                    FindAnyObjectByType<Profesora>().cortocircuito = true;
                    StartCoroutine(apagon()); // Inicia la rutina de apagado de las luces
                }
                break;
            }
        }

        if (gameObject.GetComponent<Dialogo>().enabled)
        {
            Debug.Log("Me llamo por aqui");
            gameObject.GetComponent<Dialogo>().Use(); // Activa el componente de diálogo para mostrar el mensaje de que el jugador no tiene los libros o el mechero en su inventario
        }
    }

    IEnumerator apagon()
    {
        foreach (GameObject l in luces)
            l.SetActive(false); // Apaga las luces

        yield return new WaitForSeconds(20f); // Espera 5 segundos antes de apagar el fuego

        foreach (GameObject l in luces)
            l.SetActive(true); // Apaga las luces

    }
}
