using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Constants
    protected readonly int MAX_STARS = 20;
    //Parameters
    public string playerName;
    public string monsterName;
    public Sprite monsterSprite;
    public int stars;
    public int remainingHealth;
    protected int maxHealth;
    public int energy;
    protected bool hasIdol;
    protected bool hasStatue;
    protected bool winner;
    List<Card> deck;
    List<Decorator> effects;
    Vector3 position;

    //Methods
    public Player(string playerName, string monsterName, Vector3 startingPos)
    {
    }

    public void ChangeLife(int life)
    {
        this.remainingHealth += life;
        if (this.remainingHealth > maxHealth) this.remainingHealth = maxHealth;
    }

    public void ChangeStars(int stars)
    {
        this.stars += stars;
        if (this.stars >= MAX_STARS)
        {
            this.stars = MAX_STARS;
            winner = true;
            //Y desata el evento que toque
        }
    }

    public void ChangeEnergy(int energy)
    {
        this.energy += energy;
    }

    public void BuyCard(Card targetCard, int cost)
    {
        Debug.Log("I'm buying this card: " + targetCard.GetName());
        deck.Add(targetCard);
        this.energy -= cost;
    }

    public void ReRollMarket()
    {
        Debug.Log("Mulligan!");
        this.energy -= 2;
    }

    public void Move(Area targetArea)
    {
        Debug.Log("Moving to " + targetArea.GetName());
        position = targetArea.GetPosition();
        transform.position = transform.TransformPoint(position);
    }

    public void Attack(int damage)
    {
        Debug.Log(damage + " damage to other Monstas");
    }

    private void Start()
    {
        //this.playerName = "Jughead";
        //this.monsterName = "Captain Fish";
        this.position = new Vector3(1, 1, 1);
        //this.monsterSprite = Resources.Load<Sprite>("Characters/" + monsterName + ".png");//Test pending
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
        
    }

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
    #endregion
}
