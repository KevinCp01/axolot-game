using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{

    // Declaramos una instancia de Transform llamado followTransform para el seguimiento de la camara al jugador
    public Transform followTransform;

    void FixedUpdate()
    {
     
      // Utilizamos el transform.position creando un Vector3 que esta focuseando al objeto de player asignado al codigo en el inspector.
     this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y + 2, this.transform.position.z);

    }
}
