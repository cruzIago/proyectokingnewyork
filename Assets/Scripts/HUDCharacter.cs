using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCharacter : MonoBehaviour
{
    private Sprite characterPortrait;
    private int id;

    public Text lifeText;
    public Text fameText;
    public Text energyText;
    public Image frame;
    public Image portrait;

    public List<Sprite> portraits;
    public Sprite activeFrame;
    public Sprite inactiveFrame;

    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = "10";
        fameText.text = "0";
        energyText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Getters y setters
    public Sprite getCharPortrait()
    {
        return characterPortrait;
    }

    public void setCharPortrait()
    {
        characterPortrait = portraits[id];
        portrait.sprite = characterPortrait;
    }

    public int getId()
    {
        return id;
    }

    public void setId(int id)
    {
        this.id = id;
        setCharPortrait();
    }

    public void setFrameActive(bool active)
    {
        if (active)
        {
            frame.sprite = activeFrame;
        }
        else
        {
            frame.sprite = inactiveFrame;
        }
    }

    public void setLifes(int lifes)
    {
        lifeText.text = "" + lifes;
    }

    public void setFame(int fame)
    {
        fameText.text = "" + fame;
    }

    public void setEnergy(int energy)
    {
        energyText.text = "" + energy;
    }
    #endregion
}
