using System.Collections;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    [SerializeField] private Material completedDrawingMat; 
    [SerializeField] private ParticleSystem pencilParticles;

    [SerializeField] private GameObject engine;

    private void Awake()
    {
        engine.SetActive(false);
    }

    private IEnumerator StartTimer(MeshRenderer meshRenderer)
    {
        yield return new WaitForSeconds(4);
        meshRenderer.material = completedDrawingMat;
        engine.SetActive(true);
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