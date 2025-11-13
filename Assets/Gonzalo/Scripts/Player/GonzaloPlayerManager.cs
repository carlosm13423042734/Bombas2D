using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloPlayerManager : MonoBehaviour
{
    private PlayerThrowMode throwMode;

    //Set del modo de lanzado del personaje
    public void SetPlayerThrowMode(PlayerThrowMode newThrowMode)
    {
        this.throwMode = newThrowMode;
    }
    //Get del modo de lanzado del personaje
    public PlayerThrowMode GetPlayerThrowMode()
    {
        return this.throwMode;
    }
}
