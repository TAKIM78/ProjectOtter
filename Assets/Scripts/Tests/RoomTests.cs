using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTests : MonoBehaviour
{
    RoomManager roomManager;

    // Start is called before the first frame update
    void Start()
    {
        roomManager = GameObject.Find("Managers").GetComponent<RoomManager>();

        Invoke("BuildRoom", 20f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void BuildRoom() //IronMine2
    {
        var go = GameObject.Find("IronMine2");
        var res = roomManager.BuildNewRoom(go);
        Debug.Log($"Iron mine 2 build result: {res}");
    }
}
