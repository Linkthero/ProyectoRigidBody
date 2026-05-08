using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float jumpForce = 6f;
    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isGrounded = true;

    [SerializeField] private float runMultiplier = 2f;
    private bool isRunning = false;
    [SerializeField] private AudioClip[] pasos;
    public AudioSource audioSource;

    //[Header("Ground check")]
    //[SerializeField] private Transform GroundCheck;
    //[SerializeField] private float GroundRadius = 0.25f;
    //private AnimacionesPlayer animacionesPlayer;

    //public AudioSource audioSourcePasos;
    //public AudioSource audioSourceSalto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtiene el componente Rigidbody para aplicar física al jugador
        //animacionesPlayer = GetComponent<AnimacionesPlayer>(); // Obtiene el componente AnimacionesPlayer para controlar las animaciones del jugador
    }

    public void OnGolpear(InputValue value)
    {
        if (value.isPressed)
        {
            //animacionesPlayer.Golpear(); // Reproduce la animación de golpe al recibir la entrada correspondiente
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();   // Almacena la entrada de movimiento del jugador (eje horizontal y vertical)
        
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y)); // Convierte la entrada de movimiento a la dirección del mundo
        isRunning = Keyboard.current != null && (Keyboard.current.leftShiftKey.isPressed);  // Verifica si la tecla Shift está presionada para determinar si el jugador está corriendo
        float currentSpeed = isRunning ? speed * runMultiplier : speed; // Aplica el multiplicador de velocidad si el jugador está corriendo
        Vector3 velocity = direction * currentSpeed; // Calcula la velocidad del jugador en función de la dirección y la velocidad actual
        Vector3 newVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z); // Mantiene la velocidad vertical actual del jugador para permitir el salto y la gravedad
        rb.linearVelocity = newVelocity; // Aplica la nueva velocidad al Rigidbody del jugador
        if (moveInput != Vector2.zero)
        {  // Verifica si el jugador está intentando moverse (si la entrada de movimiento no es cero)
            if (!audioSource.isPlaying)
            {
                if (isRunning){
                    audioSource.pitch = 1.8f; // Aumenta el tono del sonido de pasos si el jugador está corriendo para reflejar la mayor velocidad de movimiento
                } else
                {
                    audioSource.pitch = 1.3f;
                }
                    int n = UnityEngine.Random.Range(0, pasos.Length); // Selecciona un clip de sonido de pasos aleatorio del array de clips de sonido de pasos
                audioSource.PlayOneShot(pasos[n]);
            }
        }
        //if (moveInput.x > 0 || moveInput.y > 0)
        //{
        //    if (!audioSourcePasos.isPlaying)
        //        audioSourcePasos.Play(); // Reproduce el sonido de pasos si el jugador se está moviendo y el sonido no está ya reproduciéndose
        //    else
        //        if (audioSourcePasos.isPlaying)
        //        audioSourcePasos.Stop(); // Detiene el sonido de pasos si el jugador no se está moviendo pero el sonido está reproduciéndose
        //}
    }

    public void OnJump(InputValue value)
    {
        if (!isGrounded) return; // Si el jugador no está tocando el suelo, no permite saltar
        if (value.isPressed)
        {
            // audioSourceSalto.Play(); // Reproduce el sonido de salto al iniciar el salto
            //animacionesPlayer.AnimacionSaltar1(); // Reproduce la animación de salto
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Aplica una fuerza hacia arriba para hacer que el jugador salte
        }
    }

    void CheckInteractables()
    {
        //    Collider[] hits = Physics.OverlapSphere(GroundCheck.position, GroundRadius); // Realiza una esfera de colisión para verificar si el jugador está tocando el suelo

        //    //bool groundedNow = false; // Variable temporal para almacenar el estado de si el jugador está tocando el suelo en esta actualización

        //    foreach (Collider col in hits)
        //    {
        //        if (col.gameObject.tag == "Interactable")
        //        {

        //        }
        //    }

        //if (groundedNow != isGrounded)
        //{
        //    isGrounded = groundedNow; // Actualiza el estado de isGrounded si ha cambiado
        //    //animacionesPlayer.enSuelo(isGrounded); // Actualiza la animación del jugador para reflejar si está en el suelo o no
        //}


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "suelo")
        {
            isGrounded = true;
        }
    }



}
