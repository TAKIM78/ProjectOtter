using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject canvasPrefab;
    [SerializeField] private float speedOfProgressBar = 1f;

    public float eachFillingProducts;   //bar dolumunda kaç adet materiyal üretilecek. Üretildikten sonra 52. satýrda eklenecek.

    private Canvas createdCanvas;
    private Image barImage;
    private float roomWidth, roomHeight;

    Room myRoom;
    RoomProduction myRoomProduction;

    private void Start()
    {
        createdCanvas = Instantiate(canvasPrefab, transform.position + Vector3.back + new Vector3(7.65f , 0.5f, 0f), Quaternion.identity).GetComponent<Canvas>();
        CanvasSettings();
        barImage = createdCanvas.transform.GetChild(0).transform.GetComponent<Image>();
        barImage.transform.localScale = new Vector3(0.5f, 0.1f, 1f);

        myRoom = gameObject.GetComponent<Room>();
        myRoomProduction = gameObject.GetComponent<RoomProduction>();
    }

    private void Update()
    {
        SetScaleOfCanvas();
        FillProgressBar();
    }

    private void CanvasSettings()
    {
        roomWidth = gameObject.transform.localScale.x;
        roomHeight = gameObject.transform.localScale.y;
    }

    private void SetScaleOfCanvas()
    {
        if (createdCanvas.transform.localScale.x != roomWidth || createdCanvas.transform.localScale.y != roomHeight)
            createdCanvas.transform.localScale = new Vector3(roomWidth, roomHeight, 1f);
    }

    private void FillProgressBar()
    {
        if (myRoom.isBuilt)
        {
            float completionPercantage = myRoomProduction.GetProductionCompletionPercentage();

            if (barImage.fillAmount <= completionPercantage)
                barImage.fillAmount += speedOfProgressBar * Time.deltaTime;
            else if (barImage.fillAmount > 0 && completionPercantage == 0f)
                barImage.fillAmount += speedOfProgressBar * Time.deltaTime;
        }

        if (barImage.fillAmount == 1f)
            barImage.fillAmount = 0f;
        else if (barImage.fillAmount >= 0.99f)
        {
            barImage.fillAmount = 1f;
        }
    }
}
