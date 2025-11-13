using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloGameManager : MonoBehaviour
{
    #region GameManager
    private static GonzaloGameManager instance;
    public static GonzaloGameManager Instance
    {
        get
        {
            // Si instance no está definido, lo creamos
            if (instance == null)
            {
                // Creamos un nuevo GameObject
                GameObject gameObject = new GameObject();
                // Establecemos el nombre
                gameObject.name = "GonzaloGameManager";
                // Añadimos el componet GameManagerSingleton
                instance = gameObject.AddComponent<GonzaloGameManager>();
                // Inicializamos todos los managers
                instance.PlayerManager = new GonzaloPlayerManager();
                // Marcamos el objeto para que no se destruya entre escenas
                DontDestroyOnLoad(Instance.gameObject);
            }
            // Devolvemos la instancia
            return instance;
        }
    }
#endregion

    //Variables públicas de cada manager
    public GonzaloPlayerManager PlayerManager = null;
}
