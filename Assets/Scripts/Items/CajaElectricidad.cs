using UnityEngine;

public class CajaElectricidad : MonoBehaviour, Interactable
{
    public GameObject efecto;
    [Header("Ground check")]
    [SerializeField] private float GroundRadius = 0.25f;

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
                    FindAnyObjectByType<Profesora>().cortocircuito = true;
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
