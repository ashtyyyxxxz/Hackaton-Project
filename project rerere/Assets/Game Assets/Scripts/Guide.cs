using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Guide : MonoBehaviour
{
    [SerializeField] private Transform[] pointsOfInterest;
    [SerializeField] private AudioClip[] speech;
    [SerializeField] private Animator guideAnimator;
    [SerializeField] private Animation buttonAnimations;
    [SerializeField] private InputActionReference aButton;

    private NavMeshAgent agent;
    private AudioSource audioSource;

    private int currentPoint = 0;
    private bool inZone;

    private void OnEnable()
    {
        aButton.action.performed += AButton;
    }

    private void AButton(InputAction.CallbackContext obj)
    {
        if(inZone)
        {
            GoToNextPoint();
        }
    }

    private void OnDisable()
    {
        aButton.action.performed -= AButton;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        WalkToPoint();
        audioSource.clip = speech[currentPoint];
        audioSource.Play();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, pointsOfInterest[currentPoint].position) <= 0.5f)
        {
            guideAnimator.SetBool("IsWalking", false);
        }
    }

    private void WalkToPoint()
    {
        agent.SetDestination(pointsOfInterest[currentPoint].position);
        guideAnimator.SetBool("IsWalking", true);
    }

    public void GoToNextPoint()
    {
        currentPoint++;
        if(currentPoint > pointsOfInterest.Length)
        {
            currentPoint--;
            return;
        }
        audioSource.clip = speech[currentPoint];
        audioSource.Play();
        WalkToPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = true;
            buttonAnimations.Play("InteractButtonAppear");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = false;
            buttonAnimations.Play("InteractButtonDisappear");
        }
    }
}
