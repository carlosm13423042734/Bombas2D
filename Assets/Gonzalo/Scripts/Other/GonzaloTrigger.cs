using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject spawn1;
    [SerializeField]
    GameObject spawn2;

    Camera camera1;
    private bool passed = false;

    private void Awake()
    {
        camera1 = Camera.main;
    }

    //Método para mover la cámara a un punto o a otro
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            GonzaloCamera cameraScript = camera1.gameObject.GetComponent<GonzaloCamera>();

            //Si el jugador no ha pasado la fase, la cámara se mueve hacia el siguiente punto
            if (passed == false)
            {
                passed = true;
                cameraScript.MoveCamera(spawn2);
            }
            //Si el jugador sí ha pasado la fase, la cámara se mueve hacia el punto anterior
            else if (passed == true)
            {
                passed = false;
                cameraScript.MoveCamera(spawn1);
            }
        }
    }
}
