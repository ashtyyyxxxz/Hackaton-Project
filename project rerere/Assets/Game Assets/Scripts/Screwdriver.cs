using UnityEngine;

public class Screwdriver : MonoBehaviour
{
    [SerializeField] private AudioSource punchSound;
    [SerializeField] private AudioSource completeRepair;

    private int punchesAmount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Engine"))
        {
            punchSound.Play();
            punchesAmount++;
            if (punchesAmount == 3)
            {
                completeRepair.Play();
            }
        }
    }
}
