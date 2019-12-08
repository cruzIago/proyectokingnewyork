using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 *  Controla el flujo de la interfaz
 */
public class UI_Controller : MonoBehaviour
{
    public List<CharacterFrame> listCharacterFrames; //Prefabs de los personajes totales
    private List<CharacterFrame> charactersInScreen; //Personajes que se ven en pantalla actualmente para evitar repeticiones
    public GameObject characterContainer; //Contenedor de los frames de los personajes
    public InputField numberPlayers; // Numero de jugadores, solo como pista visual
    public Button startGameButton; //Boton para empezar la partida

    private int numberCharacters; //Numero de personajes en pantalla
    private const int MAX_CHARACTERS = 6; //Minimo y maximo de personajes
    private const int MIN_CHARACTERS = 2;

    [Range(MIN_CHARACTERS, MAX_CHARACTERS)]
    public int initialCharacters = 2; //Control de numero de personajes iniciales desde el editor

    void Start()
    {
        charactersInScreen = new List<CharacterFrame>();
        numberCharacters = 0;
        foreach (CharacterFrame ch in listCharacterFrames)
        {
            if (!charactersInScreen.Contains(ch))
            {
                numberCharacters++;
                numberPlayers.text = "" + numberCharacters;
                CharacterFrame test = Instantiate(ch, characterContainer.transform);
                Button[] botones = test.GetComponentsInChildren<Button>();
                botones[0].onClick.AddListener(delegate { backCharacter(botones[0].GetComponentInParent<CharacterFrame>()); });
                botones[1].onClick.AddListener(delegate { nextCharacter(botones[1].GetComponentInParent<CharacterFrame>()); });
                charactersInScreen.Add(test);
                if (charactersInScreen.Count == initialCharacters)
                {
                    break;
                }
            }
        }
        startGameButton.onClick.AddListener(comenzarPartida);
    }
    /*
     * Inicia la partida y envia la lista de los jugadores, ya aleatorizada 
     */
    void comenzarPartida()
    {
        //Recoger el array de personajes, aleatorizar y enviarlo.
        //Comprobar si se paso el tutorial alguna vez? mejor hacer siempre click en omitir por ahora
        //DontDestroy o static?
        Shuffle(charactersInScreen);
        Debug.Log(charactersInScreen);
    }

    /*
     * Metodo de aleatorizar listas de https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
     * En algunos lados aparece como static, comprobar si es obligatorio
     */
    void Shuffle<T>(IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    #region Cambio de personaje
    /*
     * Cambia al personaje anterior
     */
    void backCharacter(CharacterFrame parent)
    {
        bool isChanged = false;
        bool isAdded = false;
        int newIndex = parent.charIndex;
        int maxIter = 0;
        while (!isChanged && maxIter < 10)
        {
            maxIter++;
            newIndex--;
            isAdded = false;
            if (newIndex < 0)
            {
                newIndex = 5;
            }
            foreach (CharacterFrame ch in charactersInScreen)
            {
                if (ch.charIndex == newIndex)
                {
                    isAdded = true;
                }
            }
            if (!isAdded)
            {
                parent.charIndex = newIndex;
                parent.characterSprite = listCharacterFrames[parent.charIndex].characterSprite;
                parent.characterName = listCharacterFrames[parent.charIndex].characterName;
                parent.Change();
                isChanged = true;
            }
        }
    }
    /*
     * Cambia al siguiente personaje posible 
     */
    void nextCharacter(CharacterFrame parent)
    {
        bool isChanged = false;
        bool isAdded = false;
        int newIndex = parent.charIndex;
        int maxIter = 0;
        while (!isChanged && maxIter < 10)
        {
            maxIter++;
            newIndex++;
            isAdded = false;
            if (newIndex > 5)
            {
                newIndex = 0;
            }
            foreach (CharacterFrame ch in charactersInScreen)
            {
                if (ch.charIndex == newIndex)
                {
                    isAdded = true;
                }
            }
            if (!isAdded)
            {
                parent.charIndex = newIndex;
                parent.characterSprite = listCharacterFrames[parent.charIndex].characterSprite;
                parent.characterName = listCharacterFrames[parent.charIndex].characterName;
                parent.Change();
                isChanged = true;
            }
        }

    }

    #endregion

    #region Frame de personajes
    /*
     * Añade un personaje a la escena 
     */
    void DynamicFrameAdd()
    {
        bool isAdded = false;
        foreach (CharacterFrame ch in listCharacterFrames)
        {
            foreach (CharacterFrame chinlist in charactersInScreen)
            {
                if (chinlist.characterName.Equals(ch.characterName))
                {
                    isAdded = true;
                }
            }
            if (!isAdded)
            {
                CharacterFrame test = Instantiate(ch, characterContainer.transform);
                Button[] botones = test.GetComponentsInChildren<Button>();
                botones[0].onClick.AddListener(delegate { backCharacter(botones[0].GetComponentInParent<CharacterFrame>()); });
                botones[1].onClick.AddListener(delegate { nextCharacter(botones[1].GetComponentInParent<CharacterFrame>()); });
                charactersInScreen.Add(test);
                break;
            }
            isAdded = false;

        }
    }
    /*
     * Quita el ultimo personaje de la lista 
     */
    void DynamicFrameLess()
    {
        CharacterFrame test = charactersInScreen[charactersInScreen.Count - 1];
        Button[] botones = test.GetComponentsInChildren<Button>();
        botones[0].onClick.RemoveAllListeners();
        botones[1].onClick.RemoveAllListeners();
        botones[0].onClick.AddListener(delegate { backCharacter(botones[0].GetComponentInParent<CharacterFrame>()); });
        botones[1].onClick.AddListener(delegate { nextCharacter(botones[1].GetComponentInParent<CharacterFrame>()); });
        charactersInScreen.Remove(test);
        Destroy(test.gameObject);
    }
    #endregion

    #region Numero de jugadores

    /**
     * Aumenta el número de jugadores 
     */
    public void moreCharacters()
    {
        if (numberCharacters < MAX_CHARACTERS)
        {
            numberCharacters++;
            numberPlayers.text = "" + numberCharacters;
            DynamicFrameAdd();
        }
    }

    /**
     * Disminuye el número de jugadores 
     */
    public void lessCharacters()
    {
        if (numberCharacters > MIN_CHARACTERS)
        {
            numberCharacters--;
            numberPlayers.text = "" + numberCharacters;
            DynamicFrameLess();
        }
    }
    #endregion
}
