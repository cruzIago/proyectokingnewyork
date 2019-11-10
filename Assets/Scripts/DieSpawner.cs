using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSpawner : MonoBehaviour
{
    public Dice diePrefab;
    public Camera mainCamera;
    public int numDice = 6;
    public DieChecker dieChecker;
    public List<Dice> dice = new List<Dice>();
    private bool allDiceStop = false;

    public bool diceCreated = false;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numDice; i++)
        {
            Dice die = Instantiate(diePrefab,transform.TransformPoint(new Vector3(i,0,0)),Quaternion.identity);
            die.transform.parent = transform;
            die.createDie(dieChecker);
            dice.Add(die);
        }
        diceCreated = true;
    }

    private void Update()
    {
        SelectDieByCursor();
    }

    private void SelectDieByCursor()
    {
        if (Input.GetMouseButtonDown(0))
        {    
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log(hitInfo.collider.name);
                Dice dieSelected = hitInfo.transform.GetComponent<Dice>();
                if (dieSelected != null)
                {
                    dieSelected.stays = !dieSelected.stays;
                }
            }
        }
    }

    public bool AllDiceStop()
    {
           
        foreach (Dice die in dice)
        {
            if (!die.IsStop())
            {
                return false;
            }
                
        }
        //Debug.Log("Todos se han parado");
        return true;
    }

    public bool AllDiceRolled()
    {

        foreach (Dice die in dice)
        {
            if (!die.HasBeenRolled())
            {
                return false;
            }

        }
        //Debug.Log("Todos se han parado");
        return true;
    }

    public void Reroll()
    {
        foreach(Dice die in dice)
        {
            if (!die.stays)
            {
                die.ReRoll();
            }
        }
    }
}
