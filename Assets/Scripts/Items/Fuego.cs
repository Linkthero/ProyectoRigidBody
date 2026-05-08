using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Fuego : MonoBehaviour, Interactable
{
    public GameObject efectoFuego;
    [Header("Ground check")]
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float GroundRadius = 0.25f;
    public AudioSource audioSource;

    void Start()
    {
        efectoFuego.SetActive(false); // Asegura que el efecto de fuego esté desactivado al inicio del juego
    }

    public void Use()
    {
        Collider[] hits = Physics.OverlapSphere(GroundCheck.position, GroundRadius); // Realiza una esfera de colisión para verificar si el jugador está 

        foreach (Collider col in hits)
        {
            if (col.gameObject.tag == "Player")
            {
                Inventario inv = col.gameObject.GetComponent<Inventario>();
                Debug.Log(inv);
                if (inv.libros && inv.mechero)
                {
                    gameObject.GetComponent<Dialogo>().enabled = false; // Desactiva el componente de diálogo para evitar que se muestre el mensaje de que el jugador no tiene los libros o el mechero en su inventario
                    efectoFuego.SetActive(true); // Activa el efecto de fuego si el jugador tiene los libros y el mechero en su inventario
                    if (audioSource != null && !audioSource.isPlaying)
                        audioSource.Play();
                    FindAnyObjectByType<Movimiento>().incendio = true;
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
}
