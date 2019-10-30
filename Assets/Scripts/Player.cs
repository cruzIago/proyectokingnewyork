using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    //Constants
    protected readonly int MAX_STARS = 20;
    //Parameters
    protected string playerName;
    protected string monsterName;
    //Avatar: Model or Image
    protected int stars;
    protected int remainingHealth;
    protected int maxHealth;
    protected int energy;
    protected bool hasIdol;
    protected bool hasStatue;
    protected bool winner;
    List<Card> deck;
    List<Decorator> effects;
    Vector3 position;

    //Methods
    public Player(string playerName, string monsterName, Vector3 startingPos)
    {
        this.playerName = playerName;
        this.monsterName = monsterName;
        this.position = startingPos;
        //Avatar
        this.stars = 0;
        this.remainingHealth = 10;
        this.maxHealth = 10;
        this.energy = 0;
        this.hasIdol = false;
        this.hasStatue = false;
        this.winner = false;
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
    }

    public void Attack(int damage)
    {
        Debug.Log(damage + " damage to other Monstas");
    }
}
