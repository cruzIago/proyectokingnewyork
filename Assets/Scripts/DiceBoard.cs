using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBoard : MonoBehaviour
{
    [SerializeField]
    private DieSpawner spawner;
    [SerializeField]
    private DieChecker checker;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private float speedShowToCamera = 70f;
    private Quaternion originalRotation;

    private int currentNumToss = 0;
    private bool finishTossingDice = false;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.AllDiceStop())
        {
            
            Vector3 cameraPos = mainCamera.transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, mainCamera.transform.rotation, speedShowToCamera * Time.deltaTime);
           
        }

        
        if (Input.GetKeyDown(KeyCode.Return) && !finishTossingDice)
        {
            currentNumToss++;
            transform.rotation = originalRotation;
            spawner.Reroll();
            finishTossingDice = currentNumToss == 3;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            checker.PrintResult();
        }
    }
}
