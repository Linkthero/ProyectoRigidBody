using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogo : MonoBehaviour, Interactable
{
    public string textoDialogo;
    public PanelDialogo panelDialogo;
    public AudioSource audioSource;
    public void Use()
    {
        panelDialogo.CambiarTexto(textoDialogo);
        panelDialogo.gameObject.SetActive(true);
        if(audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        StartCoroutine(espera());
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(3);
        panelDialogo.gameObject.SetActive(false);
    }
}
