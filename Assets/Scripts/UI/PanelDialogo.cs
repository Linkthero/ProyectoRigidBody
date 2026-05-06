using TMPro;
using UnityEngine;

public class PanelDialogo : MonoBehaviour
{
    private TextMeshProUGUI txtDialogo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        txtDialogo = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    public void CambiarTexto(string txt)
    {
        txtDialogo.text = txt;
    }

}
