using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum dieResult { OUCH = 0, DESTRUCTION = 1, ATTACK = 2, HEAL = 3, CELEBRITY = 4, ENERGY = 5 };


public class Dice : MonoBehaviour
{
    //Enumeration
    //Parameters
    public dieResult currentResult { get; set; }
    public bool stays { get; set; }
    private bool selected = false;
    private bool applyResult = false;
    public bool stop { get; set; }
    public DieChecker dieChecker;
    private Player owner;
    [SerializeField]
    private float diceVelocity = 0.0f;
    private Rigidbody rb;
    private const float MAX_TORQUE = 90.0f;
    private Vector3 initPos;
    private bool hasBeenRolled = false;

    [SerializeField]
    private GameObject outline;
    private Material outlineMaterial;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        outlineMaterial = outline.GetComponent<Renderer>().material;
    }

    public void createDie(DieChecker dieChecker)
    {
        this.dieChecker = dieChecker;
    }

    private void Start()
    {
        initPos = transform.position;
        InitMaterial();
        SetOutline(0.0f);
        Roll();
    }

    void Update()
    {
        diceVelocity = rb.velocity.magnitude;
        if (IsStop() && !applyResult)
        {
            ApplyResult();
        }
        
    }

    /*Suma el resultado al checker*/
    private void ApplyResult()
    {
        applyResult = true;
        stop = true;
        rb.isKinematic = true;
        dieChecker.SumResult(currentResult);
    }

    

    public void Roll()
    {   
        rb.isKinematic = false;
        rb.velocity = -transform.up;
        stays = false;
        stop = false;
        applyResult = false;
        Vector3 randomDirection = new Vector3(Random.value, Random.value, Random.value);
        rb.AddTorque(randomDirection * Random.value * MAX_TORQUE, ForceMode.Acceleration);
        transform.rotation = Quaternion.AngleAxis(Random.value * 360.0f, randomDirection);
        transform.position = initPos;
        hasBeenRolled = true;
    }

    public void SubstractResult()
    {
        if(applyResult)
            dieChecker.SubtractResult(currentResult);
    }

    public void ReRoll()
    {
        selected = stays;
        if (!selected)
        {
            //dieChecker.SubtractResult(currentResult);
            SubstractResult();
            Roll();
        }
    }

    public void Select()
    {
        if (!selected)
        {
            stays = !stays;

            if (stays)
            {
                SetOutline(0.3f);
            }
            else
            {
                SetOutline(0.0f);
            }
        }
    }
    
    /*Para que el material por defecto sea transparente*/
    private void InitMaterial()
    {     
        outlineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        outlineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        outlineMaterial.SetInt("_ZWrite", 0);
        outlineMaterial.DisableKeyword("_ALPHATEST_ON");
        outlineMaterial.DisableKeyword("_ALPHABLEND_ON");
        outlineMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        outlineMaterial.renderQueue = 3000;
    }

    private void SetOutline(float alpha)
    {  
        outlineMaterial.color = new Color(0, 1, 0, alpha);
    }

    public bool IsStop()
    {
        return diceVelocity == 0.0f;
    }

    public bool HasBeenRolled()
    {
        return hasBeenRolled;
    }

    public void SetHasBeenRolled(bool hasBeenRolled) {
        this.hasBeenRolled = hasBeenRolled;
    }

    
}
