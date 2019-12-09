using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Esta clase controla la UI en el tutorial. 
 */
public class UI_Tutorial : MonoBehaviour
{
    public List<GameObject> tutorial_scenes; //Lista de escenas para pasar entre ellas
    public Button skipButton; //Boton para saltar el tutorial
    public Button nextButton; //Boton para pasar a la siguiente escena del tutorial
    public Button backButton; //Boton para volver a la escena anterior o al menu principal
    private int index;
    private bool isEnding;

    void Start()
    {
        isEnding = false;
        index = 0;
        skipButton.onClick.AddListener(skipTutorial);
        nextButton.onClick.AddListener(nextTutorial);
        backButton.onClick.AddListener(backTutorial);
    }

    /*
     *  Pasa al siguiente tutorial
     */
    void nextTutorial()
    {
        if (index + 1 > tutorial_scenes.Count-1)
        {
            //noTutorial=true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
        else
        {
            tutorial_scenes[index].SetActive(false);
            index++;
            tutorial_scenes[index].SetActive(true);
        }

    }

    /*
     * Omite el tutorial 
     */
    void skipTutorial() {
        //noTutorial=true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Debug.Log("Next scene");
    }

    /*
     * Vuelve al ultimo tutorial o al menu 
     */
    void backTutorial() {
        if (index - 1 < 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            Debug.Log("Go to menu");
        }
        else
        {
            tutorial_scenes[index].SetActive(false);
            index--;
            tutorial_scenes[index].SetActive(true);
        }
    }
}
