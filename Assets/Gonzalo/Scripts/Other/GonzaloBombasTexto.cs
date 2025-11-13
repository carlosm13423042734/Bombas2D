using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GonzaloBombasTexto : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI numBombs;

    GonzaloPlayerDropBomb bombDropperScript;

    // Start is called before the first frame update
    void Start()
    {
        bombDropperScript = GameObject.FindGameObjectWithTag("Player").GetComponent<GonzaloPlayerDropBomb>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    //Cambiar el texto de las bombas disponibles
    private void UpdateText() 
    {       
        numBombs.text = ("BOMBAS DISPONIBLES: " + bombDropperScript.GetNumBombs());
    }
}
