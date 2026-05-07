using UnityEngine;

public class PuertaDespachos : Door, Interactable
{
    [Header("Ground check")]
    [SerializeField] private float GroundRadius;
    public Door otraPuerta;
    new public void Use()
    {
        Debug.Log("Use Puerta Despachos");
        if (bloqueada)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, GroundRadius); // Realiza una esfera de colisión para verificar si el jugador está 

            foreach (Collider col in hits)
            {
                if (col.gameObject.tag == "Player")
                {
                    Inventario inv = col.gameObject.GetComponent<Inventario>();
                    if (inv.tarjeta)
                    {
                        gameObject.GetComponent<Dialogo>().enabled = false; // Desactiva el componente de diálogo para evitar que se muestre el mensaje de que el jugador no tiene los libros o el mechero en su inventario
                        bloqueada = false; // Desbloquea la puerta 
                        otraPuerta.bloqueada = false;
                        otraPuerta.InteractuaPuerta();
                        InteractuaPuerta();
                        break;
                    }
                }
            }

            if (gameObject.GetComponent<Dialogo>().enabled)
            {
                gameObject.GetComponent<Dialogo>().Use(); // Activa el componente de diálogo para mostrar el mensaje de que el jugador no tiene los libros o el mechero en su inventario
            }
        }
    }
}
