using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Clase que gestiona la información y las acciones relacionadas con cada uno de los jugadores*/
public class Player : MonoBehaviour


{
    //Constants
    protected readonly int MAX_STARS = 20;
    //Parameters
    public Area currentArea;
    public string playerName;
    public string monsterName;
    public Sprite monsterSprite; //unused atm. Might delete later
    public int stars;
    public int remainingHealth;
    protected int maxHealth;
    public int energy;
    protected bool hasIdol;
    protected bool hasStatue;
    protected bool winner;
    public bool isActive { get; set; }
    List<Card> deck;//Permanentes
    List<Decorator> effects;
    Vector3 position;

    //Methods

    /*Constructor*/
    public Player(string playerName, string monsterName, Vector3 startingPos)
    {
        this.playerName = playerName;
        this.monsterName = monsterName;
        this.position = startingPos;
        this.monsterSprite = Resources.Load<Sprite>("Characters/" + monsterName + ".png");//Test pending
        this.stars = 0;
        this.remainingHealth = 10;
        this.maxHealth = 10;
        this.energy = 0;
        this.hasIdol = false;
        this.hasStatue = false;
        this.winner = false;
    }

    /*Modifica la vida del jugador dentro del rango aceptable y desata los eventos que se requieran*/
    public void ChangeLife(int life)
    {
        this.remainingHealth += life;
        if (this.remainingHealth > maxHealth) { this.remainingHealth = maxHealth; }
        if (this.remainingHealth < 0) {this.remainingHealth = 0; Debug.Log("He muerto"); }
    }

    /*Modifica la fama del jugador dentro del rango aceptable y desata los eventos que se requieran*/
    public void ChangeStars(int stars)
    {
        this.stars += stars;
        if (this.stars >= MAX_STARS)
        {
            this.stars = MAX_STARS;
            winner = true;
            //Desata el evento de Game Over
        }
    }

    /*Modifica la energía del jugador. Nunca puede pagar más energía de la que tenga y no tiene límite máximo*/
    public void ChangeEnergy(int energy)
    {
        this.energy += energy;
    }

    /*Compra una carta del mercado. Este metodo no elimina esa carta del mercado*/
    public void BuyCard(Card targetCard, int cost)
    {
        Debug.Log("I'm buying this card: " + targetCard.GetName());
        deck.Add(targetCard);//TODO: Delete
        ChangeEnergy(-cost); //Test!
        targetCard.SetBuyer(this);
        targetCard.ApplyEffect();
    }

    /*Mueve al jugador al area seleccionada*/
    public void Move(Area targetArea)
    {
        //Permite que el jugador se mueva de nuevo al area que abandona y lo elimina de la lista
        if (!currentArea.GetName().Contains("Manhattan")) { currentArea.movementFlag = true; }
        currentArea.playersInArea.Remove(this);

        //Mueve al jugador de la posicion 2 a la 1 en caso de que hubiese dos jugadores en el area
        foreach(Player p in currentArea.playersInArea) {
            p.position = currentArea.GetPosition();
            p.transform.position = position;
        }

        //Cambia el area a la nueva, mete al jugador en la lista y desactiva su flag
        currentArea = targetArea;
        currentArea.addPlayer(this);
        currentArea.movementFlag = false;

        //Si es el unico jugador en el area lo mueve al recuadro 1 y si no al 2
        if (currentArea.playersInArea.Count == 1) { position = currentArea.GetPosition(); }
        if (currentArea.playersInArea.Count == 2) { position = currentArea.GetPosition2(); }
        transform.position = position;
    }

    /*Ataque a otros monstruos*/
    public void Attack(int damage)
    {
        Debug.Log(damage + " damage to other Monstas");
    }

    #region Monobehaviour
    /*Inicializacion del GameObject*/
    private void Start()
    {
        this.position = new Vector3(1, 1, 1);
        this.stars = 0;
        this.remainingHealth = 10;
        this.maxHealth = 10;
        this.energy = 0;
        this.hasIdol = false;
        this.hasStatue = false;
        this.winner = false;

        Debug.Log(this.playerName + ", " +
            this.monsterName + ", " +
            this.position + ", " +
            this.stars + ", " +
            this.remainingHealth + ", " +
            this.maxHealth + ", " +
            this.winner + ", " +
            this.energy + ".");
    }

    private void Update()
    {
        transform.position = position;
    }
    #endregion

    #region getters and setters
    public Vector3 GetPosition()
    {
        return position;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public string GetMonsterName()
    {
        return monsterName;
    }

    public int GetNumberOfCards()
    {
        return deck.Count;
    }

    public void SetIdol(bool itHas)
    {
        hasIdol = itHas;
    }

    public bool HasIdol()
    {
        return hasIdol;
    }

    public void SetStatue(bool itHas)
    {
        hasStatue = itHas;
    }

    public bool HasStatue()
    {
        return hasStatue;
    }
    #endregion
}
