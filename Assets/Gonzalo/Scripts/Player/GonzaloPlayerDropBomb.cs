using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    private PlayerThrowMode throwMode;

    // Start is called before the first frame update
    private void Start()
    {
        listBombs = new List<GameObject>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<GonzaloPlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pulsar flechas para dropear bomba
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            //Establecer modo de lanzamiento
            GonzaloGameManager.Instance.PlayerManager.SetPlayerThrowMode(PlayerThrowMode.Up);
            //Si no has llegado al límite de bombas, dropeas una bomba y se añade 1 al contador
            if (bombCount >= numBombs) return;            
            DropBomb();
            bombCount++;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Establecer modo de lanzamiento
            GonzaloGameManager.Instance.PlayerManager.SetPlayerThrowMode(PlayerThrowMode.Down);
            //Si no has llegado al límite de bombas, dropeas una bomba y se añade 1 al contador
            if (bombCount >= numBombs) return;
            DropBomb();
            bombCount++;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //Establecer modo de lanzamiento
            GonzaloGameManager.Instance.PlayerManager.SetPlayerThrowMode(PlayerThrowMode.Forward);
            //Si no has llegado al límite de bombas, dropeas una bomba y se añade 1 al contador
            if (bombCount >= numBombs) return;
            DropBomb();
            bombCount++;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //Establecer modo de lanzamiento
            GonzaloGameManager.Instance.PlayerManager.SetPlayerThrowMode(PlayerThrowMode.Forward);
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
        //Obtener el modo de lanzamiento de bombas
        throwMode = GonzaloGameManager.Instance.PlayerManager.GetPlayerThrowMode();
        //Según el modo, lanzaremos la bomba de una manera u otra
        switch (throwMode)
        {
            case PlayerThrowMode.Up:
                //Lanzamos la bomba hacia arriba
                newBomb = Instantiate(Bomb, bombDropper.position + new Vector3(0,1), bombDropper.rotation);
                newBomb.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                listBombs.Add(newBomb);
                break;
            case PlayerThrowMode.Down:
                //Dejamos la bomba debajo nuestra, no hace falta hacer nada
                newBomb = Instantiate(Bomb, bombDropper.position, bombDropper.rotation);
                listBombs.Add(newBomb);
                break;
            case PlayerThrowMode.Forward:
                //Lanzamos la bomba hacia la derecha o izquierda
                newBomb = Instantiate(Bomb, bombDropper.position + new Vector3(0, 1), bombDropper.rotation);
                newBomb.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
                //Hacia la derecha
                if (playerMovementScript.GetPlayerDirection())
                {
                    newBomb.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5f, ForceMode2D.Impulse);
                }
                //Hacia la izquierda
                if (!playerMovementScript.GetPlayerDirection())
                {
                    newBomb.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5f, ForceMode2D.Impulse);
                }
                listBombs.Add(newBomb);
                break;
        }
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
        }
    }

    //Enviar número de bombas disponibles
    public int GetNumBombs()
    {
        return numBombs - bombCount;
    }
}
