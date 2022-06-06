using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTests : MonoBehaviour
{
    RoomManager roomManager;
    [SerializeField]
    GameObject roomToConstruct;

    // Start is called before the first frame update
    void Start()
    {
        roomManager = GameObject.Find("Managers").GetComponent<RoomManager>();

        //Invoke("BuildRoom", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void BuildRoom() //IronMine2
    {
        roomManager.BuildNewRoom(roomToConstruct);
        Debug.Log($"Iron mine 2 build result");
    }
}
