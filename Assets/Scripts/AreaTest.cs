using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTest : MonoBehaviour
{

    //Parameters
    public string areaName;
    protected int unitsCount;
    protected List<Tile> tiles;
    protected List<Player> playersInArea;
    protected Vector3 position;

    //Methods
    public AreaTest(string areaName, Vector3 position)
    {
        this.areaName = areaName;
        this.unitsCount = 0;
        this.position = position;
        //Tiles & Players filled in SceneManager
    }

    public void DamageMonster(int playerIndex)
    {
        playersInArea[playerIndex].ChangeLife(unitsCount * (-1));
    }

    public void DamageAllMonstersOnArea()
    {
        foreach (Player p in playersInArea) p.ChangeLife(unitsCount * (-1));
    }

    public void DamageAllMonsters()
    {
        //Lo mismo pero con todos los monstruos del juego!
    }

    public string GetName()
    {
        return areaName;
    }

    public Vector3 OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Me has tocao! " + areaName);
            return position;
        }
        //Should never go this way
        Debug.Log("Pos la has liao");
        return new Vector3(1, 1, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.unitsCount = 0;
        this.position = transform.position;
        Debug.Log("Area " + this.areaName + ": " + this.position);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
