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
    SceneManager manager;


    //Methods
    public void ApplyEffect(Player buyer)
    {
        switch (type)
        {
            case CardType.Health:
                buyer.ChangeLife(1);
                break;
            case CardType.Idol:
                foreach (Player p in manager.players) { p.SetIdol(false); }
                buyer.SetIdol(true);
                break;
            case CardType.LibertyStatue:
                foreach (Player p in manager.players) { p.SetStatue(false); }
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

    #region getters and setters
    public string GetName()
    {
        return cardName;
    }

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
