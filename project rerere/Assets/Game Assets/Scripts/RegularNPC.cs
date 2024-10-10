using UnityEngine;
using UnityEngine.AI;

public class RegularNPC : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(WalkToRandomPoint), 2, 10);
    }

    private void WalkToRandomPoint() 
    { 
        agent.SetDestination(GetRandomPosition());
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f));
        NavMeshHit hit;
        NavMesh.SamplePosition(position, out hit, 10, 1);
        position = hit.position;

        return position;
    }

}
