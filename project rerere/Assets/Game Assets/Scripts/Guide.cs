using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] private Transform[] pointsOfInterest;

    [SerializeField] private Animator animator;

    private int currentPoint = 0;

    private void Awake()
    {
        WalkToPoint();
    }

    private void WalkToPoint()
    {
        Vector3.MoveTowards(transform.position, pointsOfInterest[currentPoint].position,1);
        transform.LookAt(pointsOfInterest[currentPoint].position);
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
