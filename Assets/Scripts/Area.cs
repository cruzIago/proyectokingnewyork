using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Clase que gestiona las acciones y parametros de las areas*/
public class Area : MonoBehaviour
{
    //Parameters
    public string areaName;
    public int unitsCount;
    public List<ITile> tiles;
    public List<Player> playersInArea;
    protected Vector3 position;
    protected Vector3 position2;
    protected Vector3 positionTiles;
    public bool movementFlag = false;
    protected SceneManager manager;

    //Methods
    /*Constructor*/
    public Area(string areaName, Vector3 position)
    {
        this.areaName = areaName;
        this.unitsCount = 0;
        this.position = position;
        //Tiles & Players filled in SceneManager
    }

    /*Daña al monstruo del area con el indice recibido*/
    public void DamageMonster(int playerIndex)
    {
        playersInArea[playerIndex].ChangeLife(unitsCount * (-1));
    }

    /*Daña a todos los monstruos del area*/
    public void DamageAllMonstersOnArea()
    {
        foreach (Player p in playersInArea) p.ChangeLife(unitsCount * (-1));
    }

    /*Daña a todos los monstruos del juego*/
    public void DamageAllMonsters()
    {
        foreach (Player p in manager.GetPlayers()) p.ChangeLife(unitsCount * (-1));
    }

    public void InitTiles()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomType = Random.Range(0, 7);
            Building newBuilding = Instantiate(manager.TilePrefab[randomType]).GetComponent<Building>();
            newBuilding.transform.position = positionTiles + new Vector3(0, i * 0.12f, 0);
            newBuilding.Init(this);
            tiles.Add(newBuilding);
        }
    }

    /*Evento que se dispara al hacer click sobre el area*/
    public void OnMouseUp()
    {
        //Si se permite movimiento, mueve al jugador activo a este area
        if (movementFlag)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray)){ manager.activePlayer.Move(this); }
        }
    }

    /*Inserta un jugador en la lista de jugadores en el area*/
    public void addPlayer(Player player)
    {
        playersInArea.Add(player);
    }

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "PivotPos1") { this.position = child.transform.position;}
            if (child.tag == "PivotPos2") { this.position2 = child.transform.position;}
            if (child.tag == "PivotPosTile") { this.positionTiles = child.transform.position; }
        }
        this.position.y += 1;
        this.position2.y += 1;
    }

    private void Start()
    {
        playersInArea = new List<Player>();
        tiles = new List<ITile>();
        InitTiles();
        this.unitsCount = 0;
    }

    private void Update()
    {
  
    }

    #region getters and setters
    public string GetName()
    {
        return areaName;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public Vector3 GetPosition2()
    {
        return position2;
    }

    public void setManager(SceneManager manager)
    {
        this.manager = manager;
    }
    #endregion
}
