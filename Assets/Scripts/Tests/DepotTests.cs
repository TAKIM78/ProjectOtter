using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepotTests : MonoBehaviour
{
    DepotManager depotManager;

    // Start is called before the first frame update
    void Start()
    {
        depotManager = GameObject.Find("Managers").GetComponent<DepotManager>();

        //Invoke("PushProducts", 5f);
        //PullProduct();
    }

    void PushProducts()
    {
        Debug.Log("Before push: " + depotManager.DebugInventoryContents());
        Dictionary<ProductType, int> pushes = new();
        pushes.Add(ProductType.Steel, 10);
        depotManager.PushProducts(pushes);
        Debug.Log("After push: " + depotManager.DebugInventoryContents());
    }

    void PullProduct()
    {
        Debug.Log("Before pull: " + depotManager.DebugInventoryContents());
        Dictionary<ProductType, int> pulls = new();
        pulls.Add(ProductType.Steel, 15);
        var result = depotManager.PullProducts(pulls);
        Debug.Log($"After pull({result}): " + depotManager.DebugInventoryContents());
    }
}
