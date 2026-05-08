using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public bool abierta = false;
    public bool bloqueada;
    public GameObject candado;


    private Animator animator;

    public AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void InteractuaPuerta()
    {
        Debug.Log("interactuo");
        if (abierta)
        {
            animator.SetTrigger("Close");
            abierta = false;
        }
        else
        {
            animator.SetTrigger("Open");
            if(candado != null)
            {
                
                audioSource.Play();
                candado.SetActive(false); // Desactiva el candado si la puerta está bloqueada
                //haz animacion de abrir candado
            }
            abierta = true;
        }
    }

    public void Use()
    {
        if (!bloqueada)
        {
            InteractuaPuerta();
        } else
        {
            GetComponent<Dialogo>().Use();
        }
        
    }
}
