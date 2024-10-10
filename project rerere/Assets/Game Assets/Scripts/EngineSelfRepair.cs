using UnityEngine;

public class EngineSelfRepair : MonoBehaviour
{
    [SerializeField] private ParticleSystem mergeDetailsParticles;
    [SerializeField] private AudioSource mergeDetailsSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Detail"))
        {
            mergeDetailsParticles.transform.position = other.transform.position;
            mergeDetailsParticles.Play();
            mergeDetailsSound.Play();
            Destroy(other.gameObject);
        }
    }
}
