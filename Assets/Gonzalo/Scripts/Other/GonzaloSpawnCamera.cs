using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloSpawnCamera : MonoBehaviour
{
    [SerializeField]
    float radious;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Puedes cambiar el color
        Gizmos.DrawWireSphere(transform.position, radious); //Dibujo
    }
}
