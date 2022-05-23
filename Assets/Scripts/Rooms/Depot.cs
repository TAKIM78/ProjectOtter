using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Depot : Room
{
    DepotManager depotManager;
    TMP_Text debugText;

    protected override void Initialization()
    {
        base.Initialization();

        type = RoomType.Depot;

        depotManager = GameObject.Find("Managers").GetComponent<DepotManager>();
        debugText = gameObject.GetComponentInChildren<TMP_Text>();
    }

    protected override void Step()
    {
        base.Step();
        debugText.text = depotManager.DebugInventoryContents();
    }
}
