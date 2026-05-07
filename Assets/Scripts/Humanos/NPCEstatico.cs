using UnityEngine;

public class NPCEstatico : MonoBehaviour
{
    public int legs;
    public int arms;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetInteger("legs", legs);
        animator.SetInteger("arms", arms);
    }

}
