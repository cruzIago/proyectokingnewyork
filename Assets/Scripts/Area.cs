using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Clase que gestiona las acciones y parametros de las areas*/
public class Area : MonoBehaviour
{
    //Parameters
    public string areaName;
    protected int unitsCount;
    protected List<Tile> tiles;
    public List<Player> playersInArea;
    protected Vector3 position;
    protected Vector3 position2;
    public bool movementFlag = false;
    protected SceneManager manager;

    //Methods
    /*Constructor*/
    public Area(string areaName, Vector3 position)
    {
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
        foreach (Player p in manager.players) p.ChangeLife(unitsCount * (-1));
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

    private void Start()
    {
        this.unitsCount = 0;
        this.position = transform.position;
        this.position2 = transform.position;
        this.position2.z += 3;//Position2 es un offset para cuando 2j estan en un area. Debe adaptarse al modelo final
        Debug.Log("[" + this.areaName + "] pos1: " + this.position + " pos2: " + this.position2);
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
