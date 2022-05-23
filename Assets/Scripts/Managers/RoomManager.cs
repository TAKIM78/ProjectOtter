using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    GameObject roomShadowObject;

    [SerializeField]
    private int roomSizeX = 5; //Odaların boyutları aynı, ve bir odanın boyutları böyle.
    [SerializeField]
    private int roomSizeY = 3;

    [SerializeField]
    public int mapSizeX; //Tüm alanın boyutu.
    [SerializeField]
    public int mapSizeY;

    private List<Room> rooms = new List<Room>();

    DepotManager depotManager;

    /// <summary>
    /// Tipi cinsinden (mutfak, lab, maden gibi) odayı döner.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Room GetRoomOfType(RoomType type)
    {
        return rooms.Where(a => a.type == type).FirstOrDefault();
    }

    /// <summary>
    /// Yeni bir oda inşa eder (odayı aktive eder). Başarılıysa true, değilse false döner.
    /// </summary>
    /// <param name="roomObject">İnşa edilecek odanın gameObjesi.</param>
    public bool BuildNewRoom(GameObject roomObject)
    {
        Room room = roomObject.GetComponent<Room>();
        Dictionary<ProductType, int> buildCost = roomObject.GetComponent<RoomBuildCost>().GetBuildCost();

        if (!depotManager.CheckIfAffordable(buildCost))
            return false;
        if (room.isBuilt)
            return false;

        depotManager.PullProducts(buildCost);

        room.isBuilt = true;

        DeShadowizeRoom(roomObject); //Kendisi.
        if (room.roomToTheRight != null) //Sağındaki
            DeShadowizeRoom(room.roomToTheRight);
        if (room.roomToTheLeft != null) //Solundaki
            DeShadowizeRoom(room.roomToTheLeft);

        return true;
    }

    void ShadowizeRoom(GameObject roomObject)
    {
        Room room = roomObject.GetComponent<Room>();
        GameObject go = GameObject.Instantiate(roomShadowObject, new Vector3(room.posX, room.posY, -0.5f), Quaternion.identity);
        room.shadowObject = go;
    }

    void DeShadowizeRoom(GameObject roomObject)
    {
        Room room = roomObject.GetComponent<Room>();
        Destroy(room.shadowObject);

        //Bu odanın komşuları tıklanabilir olmalı. O yüzden shadowObjelerinin box colliderı ölür.
        if (room.roomToTheLeft != null)
            Destroy(room.roomToTheLeft.GetComponent<Room>().shadowObject.GetComponent<BoxCollider>());
        if (room.roomToTheRight != null)
            Destroy(room.roomToTheRight.GetComponent<Room>().shadowObject.GetComponent<BoxCollider>());
    }

    void Initialization()
    {
        depotManager = gameObject.GetComponent<DepotManager>(); //Aynı manager objesindeyiz.        
    }

    void Awake()
    {
        Initialization();
    }

    private void Start()
    {
        //Oda listesini doldur.
        foreach (var roomObject in GameObject.FindGameObjectsWithTag("Room"))
        {
            rooms.Add(roomObject.GetComponent<Room>());
        }

        //Tüm odaları gölgele.
        foreach (var room in rooms)
        {
            ShadowizeRoom(room.gameObject);
        }

        //İlgili odalardan gölgeyi kaldır. Performans olarak biraz saçma bir hareket ama debug için bu şekilde.
        foreach (var room in rooms)
        {
            if (room.isBuilt)
                DeShadowizeRoom(room.gameObject);
        }
    }
}