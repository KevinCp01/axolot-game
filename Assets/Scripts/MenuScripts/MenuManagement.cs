using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    // Metodo que carga la escena del videojuego
    public void BotonStart()
    {
        SceneManager.LoadScene("ExampleScene");
    }

    // Metodo que cierra el videojuego
    public void BotonQuit()
    {
        Debug.Log("Cerrando Juego....");
        Application.Quit();
    }

    // Metodo que envia al link del foro de la PWA
    public void BotonForo()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=OOtTeOV1zNg");
        Debug.Log("Entro al metodo..");
    }
}
