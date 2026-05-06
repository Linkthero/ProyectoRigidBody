using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogo : MonoBehaviour, Interactable
{
    public string textoDialogo;
    public PanelDialogo panelDialogo;
    public void Use()
    {
        panelDialogo.CambiarTexto(textoDialogo);
        panelDialogo.gameObject.SetActive(true);
        StartCoroutine(espera());
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(3);
        panelDialogo.gameObject.SetActive(false);
    }
}
