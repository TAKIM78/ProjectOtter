using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObsoloteRoomManager : MonoBehaviour
{
    const int mapSizeX = 10; //Tüm alanın boyutları. Oda miktarı cinsinden!
    const int mapSizeY = 10;

    const int roomSizeX = 5; //Bir odanın boyutları. Unity metresi cinsinden!
    const int roomSizeY = 3;

    public GameObject roomPlaceholderCube;

    List<Room> rooms = new List<Room>();
    Dictionary<GameObject, Room> roomGameObjects = new Dictionary<GameObject, Room>();

    // Start is called before the first frame update
    void Start()
    {
        DebugSpawnRooms();
        DebugShowBuildableSlots();
    }

    /// <summary>
    /// Tipi cinsinden (mutfak, lab, maden gibi) tüm odaları döner.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public List<T> GetRoomsOfType<T>() where T : Room
    {
        return rooms.OfType<T>().ToList();
    }

    /// <summary>
    /// Yeni bir oda inşa eder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="location">Odanın inşa edileceği lokasyon.</param>
    /// <param name="roomType">İnşa edilecek oda tipi.</param>
    public void BuildNewRoom<T>(Room location, T roomType) where T : Room
    {
        //TODO: Material logic.
        //TODO: Mesh logic.

        GameObject go = location.gameObject;

        Room oldRoom = location;

        T newRoom = go.AddComponent<T>();
        newRoom.posX = oldRoom.posX;
        newRoom.posY = oldRoom.posY;
        newRoom.height = oldRoom.height;
        newRoom.width = oldRoom.width;
        newRoom.isBuilt = true;

        Destroy(go.GetComponent<Room>());
    }

    /// <summary>
    /// İnşa edilmeye uygun slotları döner.
    /// </summary>
    /// <returns></returns>
    public List<Room> GetBuildableRoomSlots()
    {
        List<Room> buildableSlots = new();

        foreach (Room room in rooms)
        {
            if (!room.isBuilt)
            {
                Room righterRoom = rooms.Where(a => a.posX == (room.posX + roomSizeX) && a.posY == room.posY).ToList().FirstOrDefault();
                Room lefterRoom = rooms.Where(a => a.posX == (room.posX - roomSizeX) && a.posY == room.posY).ToList().FirstOrDefault();

                if (righterRoom != null && righterRoom.isBuilt)
                    buildableSlots.Add(room);
                else if (lefterRoom != null && lefterRoom.isBuilt)
                    buildableSlots.Add(room);
            }
        }

        return buildableSlots;
    }

    List<Vector2> GetAllRoomSlots()
    {
        List<Vector2> roomSlots = new List<Vector2>();

        for (int y = 0; y < mapSizeY; y++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                roomSlots.Add(new Vector2(x * roomSizeX, y * roomSizeY));
            }
        }

        return roomSlots;
    }

    void DebugSpawnRooms()
    {
        foreach (var slot in GetAllRoomSlots())
        {
            GameObject go = GameObject.Instantiate(roomPlaceholderCube, new Vector3(slot.x, slot.y, 0), Quaternion.identity);
            Room room = go.AddComponent<Room>();
            room.width = roomSizeX;
            room.height = roomSizeY;
            room.posX = slot.x;
            room.posY = slot.y;

            roomGameObjects.Add(go, room);
            rooms.Add(room);
        }
    }

    void DebugShowBuildableSlots()
    {
        //Random olarak bazı roomları build edilmiş yap.
        foreach (var room in rooms)
        {
            if (UnityEngine.Random.Range(0, 10) > 7)
            {
                room.isBuilt = true;
            }
        }

        foreach (var go in roomGameObjects.Keys)
        {
            if (roomGameObjects[go].isBuilt)
            {
                go.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
        }

        //Şimdi buildable slotları alıp boya.
        foreach (var roomSlot in GetBuildableRoomSlots())
        {
            roomSlot.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
}