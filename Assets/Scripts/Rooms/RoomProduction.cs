using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomProduction : MonoBehaviour
{
    public List<ProductType> inputTypes;
    public List<int> inputTypeCounts;

    public List<ProductType> outputTypes;
    public List<int> outputTypeCounts;

    //public int productionFactor; //Dönüşüm oranı. Output = input / factor.
    public int productionStepCount; //Bir üretim cycle'ı için gereken saniye/step miktarı.

    [SerializeField]
    bool isSupplied; //Üretime geri sayımına başlamadan önce gerekli hammadde absorbe edildi mi?
    [SerializeField]
    int stepAccumulation;

    DepotManager depotManager;
    Room room;
    RoomWorkers roomWorkers;



    /// <summary>
    /// Float (0-1 arası) cinsinden üretimin ne kadarının tamamlandığını döner.
    /// </summary>
    /// <returns></returns>
    public float GetProductionCompletionPercentage()
    {
        if (room.isBuilt)
        {
            //Debug.Log((float)stepAccumulation / (float)productionStepCount);
            return (float)stepAccumulation / (float)productionStepCount;
        }
        else
            return 1f;
    }

    public float GetProductionSpeed()
    {
        if (room.isBuilt)
        {
            return 1f / (float)productionStepCount;
            //TODO: Change 1f with actual accumulation. See: room levels.
        }
        else
            return 1f;
    }

    /// <summary>
    /// Üretim olmakta mı onu döner. Üretim ilerliyorsa true, bir sebepten ötürü durduysa false döner.
    /// </summary>
    /// <returns></returns>
    public bool CheckIfProducing()
    {
        return isSupplied;
        //TODO: Şimdilik böyle kurtarabiliriz ama eğer işçiler dahil olunca bir bakmak lazım.
    }

    void Step() //Custom update
    {
        if (!room.isBuilt)
            return;

        Production();
    }

    void Production()
    {
        if (!isSupplied)
        {
            GetInputMaterials();
            return;
        }

        //TODO: Change when room levels.
        stepAccumulation += roomWorkers.GetAssignedWorkerCount();
        //stepAccumulation++;


        if (stepAccumulation >= productionStepCount)
        {
            stepAccumulation = 0;
            isSupplied = false;

            Dictionary<ProductType, int> output = new();
            for (int i = 0; i < outputTypes.Count; i++)
            {
                output.Add(outputTypes[i], outputTypeCounts[i]);
            }

            depotManager.PushProducts(output);
        }
    }

    void GetInputMaterials()
    {
        Dictionary<ProductType, int> inputs = new();
        for (int i = 0; i < inputTypes.Count; i++)
        {
            inputs.Add(inputTypes[i], inputTypeCounts[i]);
        }

        if (depotManager.CheckIfAffordable(inputs))
        {
            if (depotManager.PullProducts(inputs))
                isSupplied = true;
        }
    }

    private void OnEnable()
    {
        StepManager.OnStepProgress += Step;
    }

    private void OnDisable()
    {
        StepManager.OnStepProgress -= Step;
    }

    private void Awake()
    {
        depotManager = GameObject.Find("Managers").GetComponent<DepotManager>();
        room = gameObject.GetComponent<Room>();
        roomWorkers = gameObject.GetComponent<RoomWorkers>();
    }
}
