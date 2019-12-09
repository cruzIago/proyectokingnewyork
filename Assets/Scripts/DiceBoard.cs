using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBoard : MonoBehaviour
{
    [SerializeField]
    private DieSpawner spawner;
    [SerializeField]
    private DieChecker checker;
    public Camera mainCamera;
    [SerializeField]
    private float speedFacingToCamera = 70f;
    private Quaternion originalRotation;

    private int currentNumToss = 0;
    public bool isFacingToCamera = false;
    private bool finishTossingDice = false;

    public Turn turn;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //isFacingToCamera = transform.rotation.Equals(mainCamera.transform.rotation);
        isFacingToCamera = true;
        /*if (finishTossingDice && isFacingToCamera)
        {
            checker.PrintResult();
        }*/

        FaceBoardToCamera();
        //InputedReroll();
        
    }

    private void FaceBoardToCamera()
    {
        if(spawner.AllDiceStop())
        {

            Vector3 cameraPos = mainCamera.transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, mainCamera.transform.rotation, speedFacingToCamera * Time.deltaTime);

        }
    }

    public void InputedReroll()
    {
        if (!finishTossingDice && isFacingToCamera)
        {
            currentNumToss++;
            transform.rotation = originalRotation;
            spawner.Reroll();
            finishTossingDice = currentNumToss == 2;
            if (finishTossingDice)
            {
                turn.NextState();
            }
        }
    }

    public void SolveDice(Player player)
    {
        checker.PrintResult();
        checker.SolveDice(player);
    }

}
