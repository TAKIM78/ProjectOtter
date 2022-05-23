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

    private void Start()
    {
        createdCanvas = Instantiate(canvasPrefab, transform.position + Vector3.back, Quaternion.identity).GetComponent<Canvas>();
        CanvasSettings();
        barImage = createdCanvas.transform.GetChild(0).transform.GetComponent<Image>();
        barImage.transform.localScale = new Vector3(0.5f, 0.1f, 1f);
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
        //if (!gameObject.GetComponent<Room>().isBuilt)//Batu: oda inşa halde değilse progress bar öylesine doluyor. Onun yerine komple disable edilebilir.
        //    barImage.fillAmount += speedOfProgressBar * Time.deltaTime; //Burası Emirhan'ın orijinal kısmı.
        //if (gameObject.GetComponent<Room>().isBuilt && gameObject.GetComponent<Room>().type != RoomType.Depot)//Batu: oda inşa edilmiş ve depo değilse.Depo üretim yapmıyor.
        //    barImage.fillAmount = gameObject.GetComponent<RoomProduction>().GetProductionCompletionPercentage();//Batu: RoomProduction'dan üretiminin ne kadarının tamamlandığı bilgisi elde edilir.

        float completionPercantage = 0f;
        
        if(gameObject.GetComponent<Room>().isBuilt && barImage.fillAmount <= gameObject.GetComponent<RoomProduction>().GetProductionCompletionPercentage())
            barImage.fillAmount += speedOfProgressBar * Time.deltaTime;

        if (gameObject.GetComponent<Room>().isBuilt && barImage.fillAmount > 0f && gameObject.GetComponent<RoomProduction>().GetProductionCompletionPercentage() == 0f)
            barImage.fillAmount += speedOfProgressBar * Time.deltaTime;

        if (barImage.fillAmount == 1f)
            barImage.fillAmount = 0f;
        else if (barImage.fillAmount >= 0.99f)
        {
            barImage.fillAmount = 1f;
            //Barýn dolduðu an burasý malzeme üretimi burada tam bar dolduðunda yapýlacak veya artacak!
        }
    }
}
