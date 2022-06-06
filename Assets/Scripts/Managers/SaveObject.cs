using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : MonoBehaviour
{
    /* TODO
     * player pos-player status like idle/working etc
     * worker pos-worker status like idle/working etc
     * room built status
     * room level
     * room production status-production percantages would be enough
     * depot resource levels
     * populate the managers
     */

    RoomManager roomManager;


    List<Room> rooms = new List<Room>();
    List<RoomProduction> roomsProduction = new List<RoomProduction>();


    [SerializeField]
    public Vector3 playerPosition;
    public int playerStatus; //0=Idle,1=Working
    
    public List<Vector3> robotPositions;
    public List<int> robotStatuses;

    public List<bool> roomBuiltStatuses;
    public List<int> roomLevels;
    public List<float> roomProductionStatuses;

    public List<ProductType> depotProductTypes;
    public List<int> depotProductAmounts;



    //Oyunun şu anki durumunu toplar ve bu objede gerekli yerlere yazar. Böylelikle save işlemi tamamlanır.
    public void PopulateTheObject()
    {

    }

    //Objenin içindeki değerleri ilgili oyun objelerine yazar. Böylece load işlemi tamamlanır.
    public void DePopulateTheObject()
    {

    }
}
