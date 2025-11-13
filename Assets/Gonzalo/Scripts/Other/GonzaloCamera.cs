using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloCamera : MonoBehaviour
{
    //Mover la cámara al punto de spawn especificado
    public void MoveCamera(GameObject spawn)
    {
        this.transform.position = spawn.transform.position;
    }
}
