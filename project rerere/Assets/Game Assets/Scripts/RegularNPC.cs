using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RegularNPC : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private Vector3 goalPos;

    private bool isReadyToWalk = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isReadyToWalk)
        {
            goalPos = GetRandomPosition();

            WalkToPoint(goalPos);
        }

        if(Vector3.Distance(transform.position,goalPos) <= 0.5f)
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void WalkToPoint(Vector3 position) 
    { 
        agent.SetDestination(GetRandomPosition());
        animator.SetBool("IsWalking", true);
        StartCoroutine(WalkCooldown());
    }

    private IEnumerator WalkCooldown()
    {
        isReadyToWalk = false;
        yield return new WaitForSeconds(14);
        isReadyToWalk = true;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f));
        NavMeshHit hit;
        NavMesh.SamplePosition(position, out hit, 100, 1);
        position = hit.position;

        return position;
    }

}
