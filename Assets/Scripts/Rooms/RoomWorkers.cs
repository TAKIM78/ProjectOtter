using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWorkers : MonoBehaviour
{
    [SerializeField]
    GameObject workerSlot1, workerSlot2, workerSlot3;
    Worker slot1Worker, slot2Worker, slot3Worker;


    public List<GameObject> GetAvailableWorkerSlots()
    {
        List<GameObject> availableSlots = new();

        if (slot1Worker == null)
            availableSlots.Add(workerSlot1);
        if (slot2Worker == null)
            availableSlots.Add(workerSlot2);
        if (slot3Worker == null)
            availableSlots.Add(workerSlot3);

        return availableSlots;
    }

    public int GetAssignedWorkerCount()
    {
        return 3 - GetAvailableWorkerSlots().Count;
    }

    public bool AddWorker(Worker worker)
    {
        bool isSucces = false;

        if (slot1Worker == null)
        {
            slot1Worker = worker;
            worker.workPlaceSlot = workerSlot1;
            isSucces = true;
        }
        else if (slot2Worker == null)
        {
            slot2Worker = worker;
            worker.workPlaceSlot = workerSlot2;
            isSucces = true;
        }
        else if (slot3Worker == null)
        {
            slot3Worker = worker;
            worker.workPlaceSlot = workerSlot3;
            isSucces = true;
        }

        if (isSucces)
            worker.MoveToWorkPlaceSlot();

        return isSucces;
    }

    //Returns null if fails.
    public Worker RemoveWorker()
    {
        Worker removedWorker = null;

        if (slot3Worker != null)
        {
            slot3Worker.isIdle = true;
            removedWorker = slot3Worker;
            slot3Worker = null;
        }
        else if (slot2Worker != null)
        {
            slot2Worker.isIdle = true;
            removedWorker = slot2Worker;
            slot2Worker = null;
        }
        else if (slot1Worker != null)
        {
            slot1Worker.isIdle = true;
            removedWorker = slot1Worker;
            slot1Worker = null; ;
        }

        return removedWorker;
    }
}
