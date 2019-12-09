using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/*Clase que gestiona la escena de desarrollo del juego*/
public class SceneManager : MonoBehaviour
{
    //Variable de debugging
    public bool debugMode = true;
    //Parameters
    //UX Interface

    #region UX Parameters
    public HUDCharacter HUDCharPrefab;
    private List<HUDCharacter> HUDCharacters;
    public GameObject layoutCharsInGame;

    public Canvas canvas;

    //UI
    public Button buttonTurn;
    public Button buttonYes;
    public Button buttonNo;
    public Button buttonOk;
    public GameObject panel;
    public Button buttonRethrow;
    public Button buttonContinue;
    public GameObject DiceHUD;
    #endregion

    #region Game Parameters
    private List<Player> players;
    public List<Area> areas;
    public Player activePlayer;
    public int activePId;
    protected Turn turn;
    [SerializeField] private GameObject turnPrefab;
    public Market market;
    public GameObject DiceBoardPos;
    public List<GameObject> TilePrefab;
    public List<Player> playersPrefab;
    #endregion

    //Methods

    #region Game Flow
    //Methods
    /*Comienza el siguiente turno*/
    public void NextTurn()
    {
        HighlightActivePlayer(false);
        do {
            activePId++;
            if (activePId >= players.Count)
            {
                activePId = 0;
            }
            activePlayer = players[activePId];
        } while (activePlayer.remainingHealth <= 0);
        turn.ChangePlayer(activePlayer);
        HighlightActivePlayer(true);
    }

    protected void CollectPlayers()
    {
        UI_Controller UIController = GameObject.Find("selectCharacterManager").GetComponent<UI_Controller>();
        List<CharacterFrame> characterFrames = UIController.GetCharactersInScreen();
        players = new List<Player>();
        HUDCharacters = new List<HUDCharacter>();
        foreach (CharacterFrame character in characterFrames)
        {
            Player p = Instantiate(playersPrefab[character.charIndex]) ;
            p.InitPlayer(this);
            players.Add(p);
            HUDCharacter c = Instantiate(HUDCharPrefab, layoutCharsInGame.transform);
            c.setId(character.charIndex);
            HUDCharacters.Add(c);
        }
    }

    /*Inicializa la posicion de los jugadores y relaciona jugadores y areas. Indica jugador activo
     Instancia las tarjetas de la GUI de cada jugador*/
    protected void StartGame()
    {
        

        initButtons();
        activePlayer = players[0];
        activePId = 0;
        HighlightActivePlayer(true);
        foreach (Area a in areas) a.setManager(this);
        foreach (Player p in players)
        {
            SelectArea(p);
        }
        
        panel.SetActive(false);
        DiceHUD.SetActive(false);
        //turn = new Turn(activePlayer, Turn.State.Begining, this);
        turn = Instantiate(turnPrefab).GetComponent<Turn>();
        market.HideCards();
        turn.diceBoardPos = DiceBoardPos;
        turn.StartTurn(activePlayer, Turn.State.ThrowDice, this);
        
    }

    //TODO Real functionality
    protected void SelectArea(Player p)
    {
        switch (p.GetMonsterName())
        {
            case "Cpt. Fish":
                p.Move(areas[3]);         
                break;
            case "Drakonis":
                p.Move(areas[5]);
                break;
            case "Kong":
                p.Move(areas[6]);
                break;
            case "Mantis":
                p.Move(areas[3]);
                break;
            case "ROB":
                p.Move(areas[4]);
                break;
            case "Sheriff":
                p.Move(areas[5]);
                break;
            default:
                print("Vamos no me jodas");
                break;
        }
    }
    #endregion

    #region UI Methods
    /*Actualiza la GUI*/
    public void UpdateGUI()
    {
        for(int i = 0; i < HUDCharacters.Count; i++)
        {
            changeUILifes(players[i].remainingHealth, HUDCharacters[i]);
            changeUIFame(players[i].stars, HUDCharacters[i]);
            changeUIEnergy(players[i].energy, HUDCharacters[i]);
            if(players[i].remainingHealth <= 0)
            {
                MarkDeadPlayer(HUDCharacters[i]);
            }
        }
    }

    /*Cambia el el numero de un elemento concreto de la UI*/
    protected void changeUILifes(int number, HUDCharacter HUDChar)
    {
        HUDChar.setLifes(number);
    }

    protected void changeUIEnergy(int number, HUDCharacter HUDChar)
    {
        HUDChar.setEnergy(number);
    }

    protected void changeUIFame(int number, HUDCharacter HUDChar)
    {
        HUDChar.setFame(number);
    }

    /*Hace la animacion de marcar o desmarcar la tarjeta del jugador activo*/
    protected void HighlightActivePlayer(bool highlight)
    {
        HUDCharacters[activePId].setFrameActive(highlight);
    }

    /*Marca la tarjeta de un jugador como muerto haciendola transparente*/
    protected void MarkDeadPlayer(HUDCharacter HUDChar)
    {
        HUDChar.gameObject.SetActive(false);
    }
    #endregion

    #region Button Methods
    /*Inicializa la visibilidad de todos los botones*/
    protected void initButtons()
    {
        buttonTurn.gameObject.SetActive(false);
        buttonYes.gameObject.SetActive(false);
        buttonNo.gameObject.SetActive(false);
        buttonOk.gameObject.SetActive(false);
    }

    /*Visibiliza el boton de continuar*/
    public void CreateConfirmButton()
    {
        buttonTurn.gameObject.SetActive(true);
    }

    /*Evento de pulsado de continuar*/
    public void OnClickConfirm()
    {
        
        if (turn.getState() == Turn.State.Movement)
        {
            //Inhabilita la posibilidad de moverse a areas y pasa a la fase de mercado
            foreach (Area a in areas) a.movementFlag = false;
            //turn.Market();
        }
        else if (turn.getState() == Turn.State.Market)
        {
            foreach (Card c in market.shownCards)
            {
                c.SetFlag(false);
            }
            Card card = activePlayer.GetSelectedCard();
            if (card != null)
            {
                card.ApplyEffect();
                activePlayer.SetSelectedCard(null);
            }
            if (turn.getState() == Turn.State.ThrowDice)
            {
                turn.NextState();
            }
            else
            {
                Debug.Log("Sin efecto");
            }
            market.HideCards();
        }
        turn.NextState();
        buttonTurn.gameObject.SetActive(false);
    }

    /*Evento de pulsado de si*/
    public void OnClickYes()
    {
        Debug.Log("FALCON YES");
        buttonYes.gameObject.SetActive(false);
        buttonNo.gameObject.SetActive(false);
        turn.MoveWithClick();
    }

    /*Evento de pulsado de no*/
    public void OnClickNo()
    {
        Debug.Log("CLICKED ON NO");
        turn.NextState();
        panel.SetActive(false);
        buttonYes.gameObject.SetActive(false);
        buttonNo.gameObject.SetActive(false);
    }

    /*Evento de pulsado de ok*/
    public void OnClickOk()
    {
        Debug.Log("CLICKED ON OK");

        panel.SetActive(false);
        buttonOk.gameObject.SetActive(false);
        CreateConfirmButton();
        //Habilita las areas a las que el jugador puede moverse
        foreach (Area a in areas)
        {
            //Comprueba que el área no sea Manhattan, que tenga menos de 2 jugadores y que no sea el área actual
            if (!a.GetName().Contains("Manhattan") && a.playersInArea.Count < 2
                && a.GetName() != activePlayer.currentArea.GetName()) { a.movementFlag = true; }
        }
    }

    public void OnClickRethrow()
    {
        turn.RethrowDice();
    }

    public void OnClickContinue()
    {
        turn.NextState();
    }

    public void createDiceHUD()
    {
        DiceHUD.SetActive(true);
    }
    public void hideDiceHUD()
    {
        DiceHUD.SetActive(false);
    }
    #endregion

    #region Effects
    public void AttackOtherPlayers(Player currentPlayer, int damage)
    {
        foreach (Player player in players)
        {
            if (currentPlayer.IsInManhattan() != player.IsInManhattan())
            {
                player.ChangeLife(-damage);
            }
        }

    }

    public void Ouch(Player player, int ouches)
    {
        if (ouches == 1)
        {
            Area playerArea = player.currentArea;
            player.ChangeLife(-playerArea.unitsCount);

        }
        else if (ouches == 2)
        {

            Area playerArea = player.currentArea;
            playerArea.DamageAllMonstersOnArea();

        }
        else if (ouches >= 3)
        {
            foreach (Area area in areas)
            {
                area.DamageAllMonstersOnArea();
            }
        }
    }
    #endregion

    #region Monobehaviour
    void Start()
    {
        CollectPlayers();
        StartGame();
        //Testing
        //turn.Move();
        //turn.Market();
    }

    void Update()
    {
        
    }
    #endregion

    #region Getters and Setters
    public List<Player> GetPlayers()
    {
        return players;
    }
    #endregion
}
