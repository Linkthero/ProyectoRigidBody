using System.Collections;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Animator animator;
    public GameObject panelFadeOut;
    private bool finJuego = false;

    private void Start()
    {
        if(panelFadeOut != null)
        {
            animator = panelFadeOut.GetComponent<Animator>();
            panelFadeOut.SetActive(false);
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            if (!finJuego)
            {
                Debug.Log("Fin del juego");
                finJuego = true;
                StartCoroutine(irAlMenu());
            }
            
        }
    }

    IEnumerator irAlMenu()
    {
        panelFadeOut.SetActive(true);
        yield return new WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void Jugar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Juego");
    }

    public void Salir()
    {
        Application.Quit();
    }

}
