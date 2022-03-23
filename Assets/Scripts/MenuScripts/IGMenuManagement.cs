using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IGMenuManagement : MonoBehaviour
{
    public void AbrirMenu(string escena)
    {
        SceneManager.LoadScene(escena);
    }
}
