using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IGMenuManagement : MonoBehaviour
{

    // Metodo del menu dentro del juego que recibe una string dependiendo del boton pulsado (Continuar, Salir)
    public void AbrirMenu(string escena)
    {
        SceneManager.LoadScene(escena);
    }
}
