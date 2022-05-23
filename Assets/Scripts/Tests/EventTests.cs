using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTests : MonoBehaviour
{
    private void OnEnable()
    {
        StepManager.OnStepProgress += Step;
    }

    private void OnDisable()
    {
        StepManager.OnStepProgress -= Step;
    }

    void Step()
    {
        //Debug.Log("Step event received.");
    }
}
