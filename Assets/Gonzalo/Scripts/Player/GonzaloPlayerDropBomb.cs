using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloPlayerDropBomb : MonoBehaviour
{
    [SerializeField]
    Transform bombDropper;
    [SerializeField]
    GameObject bomb;
    [SerializeField]
    int numBombs;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            DropBomb();
        }
    }

    private void DropBomb()
    {
        Instantiate(bomb, bombDropper.position, bombDropper.rotation);

    }
}
