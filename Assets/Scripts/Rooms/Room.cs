using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    public float posX, posY;
    [SerializeField]
    public float width, height;

    public bool isBuilt;

    public RoomType type;

    //public int roomLevel;

    public GameObject roomToTheLeft;
    public GameObject roomToTheRight;
    public GameObject shadowObject;

    protected virtual void Step()
    {
        Construction();
    }

    void Construction()
    {

    }

    private void OnEnable()
    {
        StepManager.OnStepProgress += Step;
    }

    private void OnDisable()
    {
        StepManager.OnStepProgress -= Step;
    }

    private void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {

    }
}