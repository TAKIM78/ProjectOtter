using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    RoomManager roomManager;

    List<Worker> workers = new();

    public int GetWorkerSlotAvailability(GameObject roomGameObject)
    {
        return roomGameObject.GetComponent<RoomWorkers>().GetAvailableWorkerSlots().Count;
    }

    public void AddWorker(int roomNumber)
    {
        Room room = roomManager.GetRoomByNumber(roomNumber);
        RoomWorkers roomWorkers = room.gameObject.GetComponent<RoomWorkers>();

        foreach (var worker in workers)
        {
            if (worker.isIdle)
            {
                roomWorkers.AddWorker(worker);                
                break;
            }
        }
    }

    public void RemoveWorker(int roomNumber)
    {
        Room room = roomManager.GetRoomByNumber(roomNumber);
        RoomWorkers roomWorkers = room.gameObject.GetComponent<RoomWorkers>();

        roomWorkers.RemoveWorker();
    }

    public void CreateNewWorker(GameObject workerGO)
    {
        workers.Add(workerGO.GetComponent<Worker>());
    }

    private void Start()
    {
        roomManager = gameObject.GetComponent<RoomManager>();
    }
}
