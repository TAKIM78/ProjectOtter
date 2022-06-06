using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DepotManager : MonoBehaviour
{
    Dictionary<ProductType, int> products;

    /// <summary>
    /// Depodaki envanteri döner.
    /// </summary>
    /// <returns></returns>
    public Dictionary<ProductType, int> GetProductInventory()
    {
        return products;        
    }

    /// <summary>
    /// Depoya materyal ekler. Başarılıysa true döner. Başarısız olmak için şu an bir sebep yok.
    /// </summary>
    /// <param name="productsToPut"></param>
    /// <returns></returns>
    public bool PushProducts(Dictionary<ProductType, int> productsToPut)
    {
        foreach (var product in productsToPut.Keys)
        {
            products[product] += productsToPut[product];
        }

        return true;
    }

    /// <summary>
    /// Depodan parametre olarak verilen materyalleri çeker/siler. Başarılı olursa true, olmazsa false döner.
    /// </summary>
    /// <param name="productsToPull"></param>
    /// <returns></returns>
    public bool PullProducts(Dictionary<ProductType, int> productsToPull)
    {
        if (!CheckIfAffordable(productsToPull)) //Ne olur ne olmaz.
            return false;

        foreach (var product in productsToPull.Keys)
        {
            products[product] -= productsToPull[product];
        }

        return true;
    }

    /// <summary>
    /// Verilen materyallerin depoda olup olmadığına bakar. Varsa true, yoksa false döner.
    /// </summary>
    /// <param name="productsToCheck"></param>
    /// <returns></returns>
    public bool CheckIfAffordable(Dictionary<ProductType, int> productsToCheck)
    {
        bool isAnythingMissing = false;

        foreach (var product in productsToCheck.Keys)
        {
            if (products[product] < productsToCheck[product])
            {
                isAnythingMissing = true;
                break;
            }
        }

        return !isAnythingMissing;
    }

    /// <summary>
    /// Verilen materyallerin depoda olup olmadığına bakar. Eğer eksik varsa bu materyallerin bir dict'ini döner.
    /// </summary>
    /// <param name="productsToCheck"></param>
    /// <param name="showWhatIsMissing"></param>
    /// <returns></returns>
    public Dictionary<ProductType, int> CheckIfAffordable(Dictionary<ProductType, int> productsToCheck, bool showWhatIsMissing)
    {
        Dictionary<ProductType, int> missingProducts = new();

        foreach (var product in productsToCheck.Keys)
        {
            if (products[product] < productsToCheck[product])
            {
                missingProducts.Add(product, productsToCheck[product] - products[product]);
            }
        }

        return missingProducts;
    }

    private void Initialize()
    {
        products = new();

        foreach (ProductType type in Enum.GetValues(typeof(ProductType)))
        {
            products.Add(type, UnityEngine.Random.Range(10, 123));
            //products.Add(type, 0);
        }
    }

    private void Awake()
    {
        Initialize();
    }

    public string DebugInventoryContents()
    {
        StringBuilder sb = new();

        foreach (var product in products.Keys)
        {
            sb.AppendLine($"{product} -> Stok: {products[product]}");
        }

        return sb.ToString();
    }
}
