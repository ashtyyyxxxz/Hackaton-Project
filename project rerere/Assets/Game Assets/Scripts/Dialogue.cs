using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Dialogue : MonoBehaviour
{
    [SerializeField] private string dialogueLine;
    [SerializeField] private float dialogueShowCharCd = 0.07f;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private AudioSource keyboardClick;

    private void StartDialogue()
    {
        StopAllCoroutines();
        StartCoroutine(StartDialogueRoutine());
    }

    private IEnumerator StartDialogueRoutine()
    {
        dialogueText.text = string.Empty;

        foreach(char c in dialogueLine)
        {
            keyboardClick.pitch = Random.Range(0.8f, 1.7f);
            keyboardClick.Play();
            dialogueText.text += c;
            yield return new WaitForSeconds(dialogueShowCharCd);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialogue();
        }
    }

}
