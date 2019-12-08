using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Se encarga de actualizar la pantalla de fin de juego dependiendo de como y quien gane la partida 
 */
public class UI_EndGame : MonoBehaviour
{
    public Image victoryImage; //Imagen de victoria
    public Text victoryText; //Texto de victoriaç
    public Button againButton;
    public Button menuButton;

    void Start()
    {
        //Poner la imagen y el texto de victoria adecuado
        againButton.onClick.AddListener(OnAgainClick);
        menuButton.onClick.AddListener(OnMenuClick);
    }

    /*
     * Vuelve a empezar una partida con la misma configuración 
     */
    void OnAgainClick() {
    }

    /*
     * Vuelve al menu 
     */
    void OnMenuClick() {
    }
}
