using UnityEngine;

public class Inventario : MonoBehaviour
{
    public bool destornillador = false;
    public bool llaveGrande = false;
    public bool tarjeta = false;
    public bool mechero = false;
    public bool llavePequeña = false;
    public bool libros = false;
    
    public void GetItem(string item)
    {
        switch (item)
        {
            case "Destornillador":
                destornillador = true;
                break;
            case "KeyDespacho":
                llavePequeña = true;
                break;
            case "Tarjeta":
                tarjeta = true;
                break;
            case "Mechero":
                mechero = true;
                break;
            case "KeySalida":
                llaveGrande = true;
                break;
            case "Libros":
                libros = true;
                break;
        }
    }
}
