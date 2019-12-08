using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    //Enumeration
    public enum CardType {Health, Idol, LibertyStatue}

    //Parameters
    string cardName;
    [SerializeField] private CardType type;
    public Sprite cardImage;
    [SerializeField] SceneManager manager;
    private bool clickFlag = false;
    Player buyer;


    //Methods
    public void ApplyEffect()
    {
        Debug.Log("Se ha activado el efecto " + type + " desde el siñor: " + buyer.GetMonsterName());
        switch (type)
        {
            case CardType.Health:
                buyer.ChangeLife(1);
                break;
            case CardType.Idol:
                foreach (Player p in manager.GetPlayers()) { p.SetIdol(false); }
                buyer.SetIdol(true);
                break;
            case CardType.LibertyStatue:
                foreach (Player p in manager.GetPlayers()) { p.SetStatue(false); }
                buyer.SetStatue(true);
                break;
            default:
                break;
        }
    }

    public void ChangeVisibility(bool visible)
    {
        if (visible) { this.gameObject.SetActive(true); }
        else { this.gameObject.SetActive(false); }
    }

    public void ClickOnCard()
    {
        Debug.Log("Me tocaste: " + type);
        if (clickFlag)
        {
            buyer.SetSelectedCard(this);
        }

        manager.CreateConfirmButton();
    }

    #region getters and setters
    public string GetName()
    {
        return cardName;
    }

    public void SetFlag(bool flag) { clickFlag = flag; }

    public bool GetFlag() { return clickFlag; }

    public void SetBuyer(Player player) { buyer = player;  }

    #endregion


    #region monobehaviour
    public void Start()
    {
    }

    public void Update()
    {
        
    }
#endregion

}
