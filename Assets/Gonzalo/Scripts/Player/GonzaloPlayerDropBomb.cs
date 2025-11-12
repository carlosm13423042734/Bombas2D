using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloPlayerDropBomb : MonoBehaviour
{
    [SerializeField]
    Transform bombDropper;
    [SerializeField]
    GameObject Bomb;
    [SerializeField]
    private int numBombs;

    GameObject newBomb;
    private List<GameObject> listBombs;
    public int bombCount = 0;

    private void Start()
    {
        listBombs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (bombCount >= numBombs) return;            
            DropBomb();
            bombCount++;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            ExploteBombs();
        }
    }

    //Dropear bomba
    private void DropBomb()
    {
        newBomb = Instantiate(Bomb, bombDropper.position, bombDropper.rotation);
        listBombs.Add(newBomb);
    }

    //Explotar las bombas
    private void ExploteBombs()
    {
        if (listBombs.Count > 0)
        {   
            foreach (GameObject bomb in listBombs)
            {
                if (bomb != null)
                {
                    GonzaloBomb newBombScript = bomb.GetComponent<GonzaloBomb>();
                    newBombScript.Explosion();
                }
            }
            listBombs.RemoveAll(bomb => bomb == null);
            bombCount = 0;
        }
    }
}
