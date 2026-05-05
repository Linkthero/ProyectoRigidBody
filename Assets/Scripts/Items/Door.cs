using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public bool abierta = false;

    private Animator animator;

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
            abierta = true;
        }
    }

    public void Use()
    {
        InteractuaPuerta();
    }
}
