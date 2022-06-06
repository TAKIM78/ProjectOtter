using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorageInformation : MonoBehaviour
{
    [SerializeField] Canvas canvasStorage;
    [SerializeField] TextMeshProUGUI[] textInfo;
    [SerializeField] Image[] imageInfo;
    [SerializeField] Sprite[] icons;

    private DepotManager depotManager;

    private void Start()
    {
        depotManager = GameObject.Find("Managers").GetComponent<DepotManager>();
        StorageIcons();
    }

    private void Update()
    {
        if (canvasStorage.transform.gameObject.activeInHierarchy)
            StorageInfo();
    }

    public void ActiveAndDeactivateInfo()
    {
        if (canvasStorage.transform.gameObject.activeInHierarchy)
            canvasStorage.transform.gameObject.SetActive(false);
        else
        {
            canvasStorage.transform.gameObject.SetActive(true);
            StorageInfo();
        }
    }

    private void StorageInfo()
    {
        int counter = 0;
        foreach (var product in depotManager.GetProductInventory())
        {
            textInfo[counter].text = product.Key + ": " + product.Value;
            counter++;
        }
    }

    private void StorageIcons()
    {
        for (int i = 0; i < imageInfo.Length; i++)
            imageInfo[i].sprite = icons[i];
    }
}
