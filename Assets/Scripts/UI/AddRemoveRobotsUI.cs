using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddRemoveRobotsUI : MonoBehaviour
{
    [SerializeField] GameObject[] rooms;
    [SerializeField] GameObject[] canvases;
    private GameObject selectedRoomObj;
    private WorkerManager workerManager;
    private RoomManager _roomManager;
    private int clickedRoomNo = 0;

    private void Start()
    {
        workerManager = GameObject.Find("Managers").GetComponent<WorkerManager>();
        _roomManager = GameObject.Find("Managers").GetComponent<RoomManager>();
    }

    private void Update()
    {
        ActivateCanvases();
    }

    public void Number3()
    {
        clickedRoomNo = 3;
        selectedRoomObj = rooms[0];
    }
    public void Number4()
    {
        clickedRoomNo = 4;
        selectedRoomObj = rooms[1];
    }
    public void Number5()
    {
        clickedRoomNo = 5;
        selectedRoomObj = rooms[2];
    }
    public void Number6()
    {
        clickedRoomNo = 6;
        selectedRoomObj = rooms[3];
    }
    public void Number7()
    {
        clickedRoomNo = 7;
        selectedRoomObj = rooms[4];
    }
    public void Number8()
    {
        clickedRoomNo = 8;
        selectedRoomObj = rooms[5];
    }
    public void Number9()
    {
        clickedRoomNo = 9;
        selectedRoomObj = rooms[6];
    }
    public void Number10()
    {
        clickedRoomNo = 10;
        selectedRoomObj = rooms[7];
    }
    public void Number11()
    {
        clickedRoomNo = 11;
        selectedRoomObj = rooms[8];
    }
    public void Number12()
    {
        clickedRoomNo = 12;
        selectedRoomObj = rooms[9];
    }

    public void AddWorkerUI()
    {
        if (workerManager.GetWorkerSlotAvailability(selectedRoomObj) > 0)
        {
            workerManager.AddWorker(clickedRoomNo);
        }
    }

    public void RemoveWorkerUI()
    {
        if (workerManager.GetWorkerSlotAvailability(selectedRoomObj) <= 0)
        {
            workerManager.RemoveWorker(clickedRoomNo);
        }
    }

    private void ActivateCanvases()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].GetComponent<Room>().isBuilt && rooms[i].activeInHierarchy)
                canvases[i].SetActive(true);
        }
    }
}
