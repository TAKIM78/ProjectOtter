using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Worker : MonoBehaviour
{
    public WorkerType type;
    public bool isIdle;
    public GameObject workPlaceSlot;
    NavMeshAgent navMeshAgent;
    Animation animation;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        InvokeRepeating("WanderAimlessly", 1f, 10f);
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle == true) //Idle means no workplace!!! 
        {
            if (navMeshAgent.remainingDistance < 2f)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWorking", false);
            }
        }
        else
        {
            if (navMeshAgent.remainingDistance < 2f)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWorking", true);
            }
        }
    }

    public void MoveToWorkPlaceSlot()
    {
        navMeshAgent.SetDestination(workPlaceSlot.transform.position);
        //TODO: Set animation thing
        animator.SetBool("isRunning", true);
        animator.SetBool("isWorking", false);
        isIdle = false;
    }

    void WanderAimlessly()
    {
        if (isIdle == false)
            return;

        float x = Random.Range(-25, 40);
        float y = Random.Range(-1, -10);
        float z = Random.Range(-9, 1);
        Vector3 target = new Vector3(x, y, z);

        navMeshAgent.SetDestination(target);

        //TODO: set animation paramater
        animator.SetBool("isRunning", true);
        animator.SetBool("isWorking", false);
    }
}
