using UnityEngine;
using UnityEngine.AI;

public class Guide : MonoBehaviour
{
    [SerializeField] private Transform[] pointsOfInterest;

    [SerializeField] private Animator animator;

    private NavMeshAgent agent;

    private int currentPoint = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        WalkToPoint();
    }

    private void WalkToPoint()
    {
        agent.SetDestination(pointsOfInterest[currentPoint].position);
    }

    public void GoToNextPoint()
    {
        currentPoint++;
        if(currentPoint > pointsOfInterest.Length)
        {
            currentPoint--;
            return;
        }
        WalkToPoint();
    }

    public void GoToPreviousPoint()
    {
        currentPoint--;
        if (currentPoint < 0)
        {
            currentPoint++;
            return;
        }
        WalkToPoint();
    }
}
