using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBoard : MonoBehaviour
{
    [SerializeField]
    private DieSpawner spawner;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private float speedShowToCamera = 70f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.AllDiceStop())
        {
            Debug.Log("Todos los dados se han parado");
            Vector3 cameraPos = mainCamera.transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, mainCamera.transform.rotation, speedShowToCamera * Time.deltaTime);
           
        }
    }
}
