using UnityEngine;

public class Screwdriver : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    private int punchesAmount = 0;

    private void Awake()
    {
        audioSource.clip = clips[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Engine"))
        {
            audioSource.Play();
            punchesAmount++;
            if (punchesAmount == 3)
            {
                audioSource.clip = clips[1];
                audioSource.Play();
            }
        }
    }
}
