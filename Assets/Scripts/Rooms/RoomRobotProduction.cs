using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRobotProduction : MonoBehaviour
{
    public List<ProductType> inputTypes;
    public List<int> inputTypeCounts;

    public int productionStepCount; //Bir üretim cycle'ı için gereken saniye/step miktarı.

    [SerializeField]
    bool isSupplied; //Üretime geri sayımına başlamadan önce gerekli hammadde absorbe edildi mi?
    [SerializeField]
    int stepAccumulation;

    [SerializeField]
    GameObject workerRobotPrefab;

    [SerializeField]
    GameObject workerSpawnPlaceGameObject;

    DepotManager depotManager;
    WorkerManager workerManager;
    Room room;

    bool isSmartBoiPresent;

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

        //TODO: Change when room workers implemented.
        stepAccumulation++;

        if (stepAccumulation >= productionStepCount)
        {
            stepAccumulation = 0;
            isSupplied = false;

            WorkerType workerTypeToProduce = WorkerType.Default;

            if (isSmartBoiPresent == false)
            {
                isSmartBoiPresent = true;
                workerTypeToProduce = WorkerType.Smartboi;
            }
            else
            {
                int rng = Random.Range(2, 9);
                switch (rng)
                {
                    case 2:
                        workerTypeToProduce = WorkerType.Zibidi;
                        break;
                    case 3:
                        workerTypeToProduce = WorkerType.Mibidi;
                        break;
                    case 4:
                        workerTypeToProduce = WorkerType.Hübele;
                        break;
                    case 5:
                        workerTypeToProduce = WorkerType.Nübele;
                        break;
                    case 6:
                        workerTypeToProduce = WorkerType.Humhum;
                        break;
                    case 7:
                        workerTypeToProduce = WorkerType.Zumzum;
                        break;
                    case 8:
                        workerTypeToProduce = WorkerType.Jüjü;
                        break;

                    default:
                        break;
                }
            }

            GameObject workerGO = GameObject.Instantiate(workerRobotPrefab, workerSpawnPlaceGameObject.transform);
            Worker worker = workerGO.GetComponent<Worker>();
            worker.type = workerTypeToProduce;

            workerManager.CreateNewWorker(workerGO);
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
        workerManager= GameObject.Find("Managers").GetComponent<WorkerManager>();
        room = gameObject.GetComponent<Room>();
    }
}
