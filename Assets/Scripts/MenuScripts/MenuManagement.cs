using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public void BotonStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    public void BotonQuit()
    {
        Debug.Log("Cerrando Juego....");
        Application.Quit();
    }
    public void BotonForo()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=OOtTeOV1zNg");
        Debug.Log("Entro al metodo..");
    }
}
