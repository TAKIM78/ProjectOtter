using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    RoomManager roomManager;


    public void SaveGame()
    {
        SaveObject saveObject = gameObject.AddComponent<SaveObject>();

        saveObject.PopulateTheObject();

        string jsonStr = JsonUtility.ToJson(saveObject);

        PlayerPrefs.DeleteKey("SaveData"); //Batıl bir adamım, ne olur ne olmaz eski datayı silelim.
        PlayerPrefs.SetString("SaveData", jsonStr);
        PlayerPrefs.Save();

        Destroy(saveObject);
    }

    public void LoadGame()
    {
        string jsonStr = PlayerPrefs.GetString("SaveData", "null");

        if (jsonStr != "null") //Eğer daha önceden bir save varsa devam et. Yoksa default(editördeki) değerler ile otomatik devam.
        {
            SaveObject saveObject = gameObject.AddComponent<SaveObject>();
            JsonUtility.FromJsonOverwrite(jsonStr, saveObject);
        }
    }

    private void Start()
    {

    }
}
