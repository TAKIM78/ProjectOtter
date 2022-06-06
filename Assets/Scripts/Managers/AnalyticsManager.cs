using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var res = Analytics.CustomEvent("OpenFactoryScene");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
