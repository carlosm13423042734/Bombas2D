using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject spawn1;
    [SerializeField]
    GameObject spawn2;

    Camera camera;
    private bool passed = false;

    private void Awake()
    {
        camera = Camera.main;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            GonzaloCamera cameraScript = camera.gameObject.GetComponent<GonzaloCamera>();
            if (passed == false)
            {
                passed = true;
                cameraScript.MoveCamera(spawn2);
            }
            else if (passed == true)
            {
                passed = false;
                cameraScript.MoveCamera(spawn1);
            }
        }
    }
}
