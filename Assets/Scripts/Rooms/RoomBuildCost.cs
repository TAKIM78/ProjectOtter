using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuildCost : MonoBehaviour
{
    public List<ProductType> costTypes;
    public List<int> costTypeCounts;


    //[SerializeField] List<ProductInfoScriptableObject> costs;

    /// <summary>
    /// Dictionary cinsinden odanın satın alma/inşa etme maliyetini döner.
    /// </summary>
    /// <returns></returns>
    public Dictionary<ProductType, int> GetBuildCost()
    {
        Dictionary<ProductType, int> cost = new();

        for (int i = 0; i < costTypes.Count; i++)
        {
            cost.Add(costTypes[i], costTypeCounts[i]);
        }

        return cost;
    }
}
