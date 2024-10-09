using System.Collections;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    [SerializeField] private Material completedDrawingMat; 
    [SerializeField] private ParticleSystem pencilParticles;

    private IEnumerator StartTimer(MeshRenderer meshRenderer)
    {
        yield return new WaitForSeconds(4);
        meshRenderer.material = completedDrawingMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drawing"))
        {
            pencilParticles.Play();
            StartCoroutine(StartTimer(other.GetComponent<MeshRenderer>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pencilParticles.Stop();
    }
}
