using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.UI;
using System;

public class RoomInformations : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_informations;
    [SerializeField] Image image;
    [SerializeField] Sprite[] icons;
    private RoomManager _roomManager;
    private RaycastHit hit;
    private Ray ray;
    private GameObject selectedCollider;
    private Touch touch;

    private void Start()
    {
        _roomManager = GameObject.Find("Managers").GetComponent<RoomManager>();
    }

    private void Update()
    {
        WhenMouseClicked();
    }

    private void WhenMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject roomColliders;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                roomColliders = _roomManager.GetClickedRoomObject(hit.transform.gameObject);
            else
                roomColliders = null;

            if (roomColliders != null)
            {
                if (!text_informations.transform.gameObject.activeInHierarchy)
                {
                    text_informations.transform.gameObject.SetActive(true);
                    image.transform.gameObject.SetActive(true);
                }
                else if (text_informations.transform.gameObject.activeInHierarchy && selectedCollider == roomColliders)
                {
                    text_informations.transform.gameObject.SetActive(false);
                    image.transform.gameObject.SetActive(false);
                }
                selectedCollider = roomColliders;

                SetTextInformations(roomColliders);
                Debug.Log(selectedCollider.name);
                Debug.Log($"Seçilen oda: {roomColliders.transform.name}");
            }
        }
    }

    private void SetTextInformations(GameObject selectedRoom)
    {
        text_informations.text = String.Empty;

        if (selectedRoom.GetComponent<Room>().isBuilt)
        {
            if (selectedRoom.GetComponent<Room>().type == RoomType.Depot)
            {
                //Depo
                text_informations.text = "Depo";
            }
            else if (selectedRoom.GetComponent<Room>().type == RoomType.RobotProductionLab)
            {
                //Robot üretim odası.
                text_informations.text = "Robot Üretim Odası";
            }
            else //TODO: Roket odası ve koridoru da dahil etmek lazım bu döngüye.
            {
                RoomProduction roomProduction = selectedRoom.GetComponent<RoomProduction>();

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"{selectedRoom.GetComponent<Room>().type.ToString()}");
                stringBuilder.AppendLine("\nInput Materials:");
                foreach (ProductType materyalTipi in roomProduction.inputTypes)
                {
                    string s = materyalTipi + ": " + roomProduction.inputTypeCounts[roomProduction.inputTypes.IndexOf(materyalTipi)].ToString();
                    stringBuilder.AppendLine(s);
                }
                stringBuilder.AppendLine("\nOutput Materials: ");
                foreach (ProductType ciktiTipi in roomProduction.outputTypes)
                {
                    string s = ciktiTipi + ": " + roomProduction.outputTypeCounts[roomProduction.outputTypes.IndexOf(ciktiTipi)].ToString();
                    stringBuilder.AppendLine(s);
                }

                text_informations.text = stringBuilder.ToString();
            }
            SetRoomImages(selectedRoom.transform.name);
        }
        else if (selectedRoom.GetComponent<Room>().isBuilt == false)
        {
            //Buraya gerekli logic konulabilir.
            RoomBuildCost roomBuildCost = selectedRoom.GetComponent<RoomBuildCost>();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{selectedRoom.GetComponent<Room>().type.ToString()} is not built yet!");
            stringBuilder.AppendLine("Construction Materials: ");
            foreach (ProductType materyalTipi in roomBuildCost.costTypes)
            {
                string s = materyalTipi + ": " + roomBuildCost.costTypeCounts[roomBuildCost.costTypes.IndexOf(materyalTipi)].ToString();
                stringBuilder.AppendLine(s);
            }

            text_informations.text = stringBuilder.ToString();
            SetRoomImages(selectedRoom.transform.name);
        }
    }

    private void SetRoomImages(string selectedRoomName)
    {
        int roomNo = Int32.Parse(selectedRoomName.Replace("Room", ""));
        if (icons[roomNo - 1].rect.width > icons[roomNo - 1].rect.height)
        {
            float x = icons[roomNo - 1].rect.width / 200f;
            image.rectTransform.sizeDelta = new Vector2(200f, icons[roomNo - 1].rect.height / x);
        }
        else if(icons[roomNo - 1].rect.height> icons[roomNo - 1].rect.width)
        {
            float x = icons[roomNo - 1].rect.height / 200f;
            image.rectTransform.sizeDelta = new Vector2(icons[roomNo - 1].rect.width / x, 200f);
        }
        image.sprite = icons[roomNo - 1];
    }
}
