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
    private GonzaloPlayerMovement playerMovementScript;

    // Start is called before the first frame update
    private void Start()
    {
        listBombs = new List<GameObject>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<GonzaloPlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pulsar Q para dropear bomba
        if (Input.GetKeyUp(KeyCode.Q))
        {
            //Si no has llegado al límite de bombas, dropeas una bomba y se añade 1 al contador
            if (bombCount >= numBombs) return;            
            DropBomb();
            bombCount++;
        }

        //Pulsar E para explotar bombas
        if (Input.GetKeyUp(KeyCode.E))
        {
            ExploteBombs();
        }
    }

    //Dropear bomba, se instancia una nueva bomba y se añade a una lista
    private void DropBomb()
    {
        newBomb = Instantiate(Bomb, bombDropper.position, bombDropper.rotation);
        listBombs.Add(newBomb);
    }

    //Explotar las bombas
    private void ExploteBombs()
    {
        //Si en la lista hay más de 0 bombas, las explota todas
        if (listBombs.Count > 0)
        {   
            foreach (GameObject bomb in listBombs)
            {
                if (bomb != null)
                {
                    GonzaloBomb newBombScript = bomb.GetComponent<GonzaloBomb>();
                    newBombScript.Explode();
                }
            }
            //Se borran las bombas de la lista y se reinicia el contador de bombas
            listBombs.RemoveAll(bomb => bomb == null);
            bombCount = 0;
            //En inician Corrutinas para que la explosión de la bomba sea más realista, impidiendo que el jugador pueda moverse ni se detenga el movimiento horizontal antes de tiempo.
            //Es decir, dejamos que la física haga lo suyo
            StartCoroutine(PlayerCantMove());
            StartCoroutine(PlayerIsImpulsed());
        }
    }

    //Corrutina para que el jugador no pueda moverse
    IEnumerator PlayerCantMove()
    {
        playerMovementScript.IsAllowedToMove = false; 
        yield return new WaitForSeconds(0.5f);
        playerMovementScript.IsAllowedToMove = true;
    }

    //Corrutina para que el movimiento horizontal no se detenga
    IEnumerator PlayerIsImpulsed()
    {
        playerMovementScript.IsBeenImpulsed = true;
        yield return new WaitForSeconds(1.5f);
        //playerMovementScript.IsBeenImpulsed = false;
    }

    //Enviar número de bombas disponibles
    public int GetNumBombs()
    {
        return numBombs - bombCount;
    }
}
