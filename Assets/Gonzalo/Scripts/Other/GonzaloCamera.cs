using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloCamera : MonoBehaviour
{
    public void MoveCamera(GameObject spawn)
    {
        this.transform.position = spawn.transform.position;
    }
}
