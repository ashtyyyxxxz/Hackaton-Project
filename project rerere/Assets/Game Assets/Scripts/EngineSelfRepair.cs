using UnityEngine;

public class EngineSelfRepair : MonoBehaviour
{
    [SerializeField] private ParticleSystem mergeDetailsParticles;
    [SerializeField] private AudioSource mergeDetailsSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Detail"))
        {
            mergeDetailsParticles.transform.position = collision.transform.position;
            mergeDetailsParticles.Play();
            mergeDetailsSound.Play();
            Destroy(collision.gameObject);
        }
    }
}
