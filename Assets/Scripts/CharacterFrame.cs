using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterFrame : MonoBehaviour
{
    public Sprite characterSprite;
    public string characterName;
    public Text characterNameText;
    public Image characterImage;
    public int charIndex;

    private void Start()
    {
        Change();
    }

    public void Change() {
        characterNameText.text = "" + characterName;
        characterImage.sprite = characterSprite;
    }




}
