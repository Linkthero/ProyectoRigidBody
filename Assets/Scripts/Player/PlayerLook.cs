using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Sensibilidad de la camara")]
    [SerializeField] public float mousesensibility = 0.15f;

    [Header("Pitch clamp (angulo de inclinacion)")]
    [SerializeField] public float minPitch = -60f; // Limita el ángulo de inclinación vertical para evitar que la cámara gire completamente hacia arriba o hacia abajo
    [SerializeField] public float maxPitch = 60f; // Limita el ángulo de inclinación vertical para evitar que la cámara gire completamente hacia arriba o hacia abajo

    private Vector2 lookInput;
    private float yaw; // Almacena la rotación horizontal del jugador (eje Y)
    private float pitch; // Almacena la rotación vertical de la cámara (eje X)


    private Transform cameraTransform;
    private Camera currentCamera;
    private bool canMove = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateActiveCamera();
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor para que no se mueva fuera de la ventana del juego
        Cursor.visible = false; // Oculta el cursor para una experiencia de juego más inmersiva
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return; // Si el jugador no puede moverse, no se ejecuta el código de rotación
        if (cameraTransform == null)
            return; // Si no se ha asignado una cámara, no se ejecuta el código de rotación
        //rotacion horizontal del jugador
        float yaw = lookInput.x * mousesensibility; // Calcula la rotación horizontal del jugador en función de la entrada del mouse y la sensibilidad
        transform.Rotate(0, yaw, 0, Space.Self); // Aplica la rotación horizontal al jugador en su propio espacio local

        //rotacion camara
        pitch -= lookInput.y * mousesensibility; // Calcula la rotación vertical de la cámara en función de la entrada del mouse y la sensibilidad
        pitch = Mathf.Clamp(yaw, minPitch, maxPitch); // Limita la rotación vertical de la cámara para evitar que gire completamente hacia arriba o hacia abajo
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0); // Aplica la rotación vertical a la cámara en su propio espacio local
    }

    public void SetCanMove(bool value)
    {
        canMove = value; // Permite habilitar o deshabilitar el movimiento del jugador, lo que afecta la capacidad de rotar la cámara
    }

    private void UpdateActiveCamera()
    {
        if (Camera.main != currentCamera)
        {
            currentCamera = Camera.main; // Actualiza la referencia a la cámara principal del juego
            if (currentCamera != null)
            {
                cameraTransform = currentCamera.transform; // Actualiza la referencia al transform de la cámara para aplicar las rotaciones correctamente
                pitch = cameraTransform.localEulerAngles.x; // Establece el ángulo máximo de inclinación en función de la rotación inicial de la cámara para mantener la orientación correcta
                if (pitch > 180f) pitch -= 360f; // Ajusta el ángulo máximo de inclinación si es mayor a 180 grados para evitar problemas de rotación y mantener la orientación correcta de la cámara
            }
        }
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>(); // Almacena la entrada de movimiento del mouse para usarla en la rotación del jugador y la cámara
                                          //Debug.Log("onLook");
    }

    private void OnEnable()
    {
        lookInput = Vector2.zero; // Reinicia la entrada de movimiento del mouse al habilitar el script para evitar movimientos no deseados al activar el jugador o la cámara
    }

}

