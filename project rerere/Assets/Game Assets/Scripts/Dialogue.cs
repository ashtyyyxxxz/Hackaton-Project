using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.InputSystem;

// better use ready prefab
[RequireComponent(typeof(Collider))]
public class Dialogue : MonoBehaviour
{
    [SerializeField] private DialogueLine[] dialogueLines;
    [SerializeField] private float dialogueShowCharCd = 0.07f;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private AudioSource keyboardClickSource;
    [SerializeField] private AudioSource speechSource;

    [Header("Actions")]
    [SerializeField] private InputActionReference aButtonAction;
    [SerializeField] private InputActionReference bButtonAction;

    private Animator animator;
    private bool inZone;
    private int currentLine;


    private void Awake()
    {
        currentLine = 0;
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        aButtonAction.action.performed += AButton;
        bButtonAction.action.performed += BButton;
    }

    private void OnDisable()
    {
        aButtonAction.action.performed -= AButton;
        bButtonAction.action.performed -= BButton;
    }

    private void AButton(InputAction.CallbackContext obj)
    {
        if (inZone) NextLine();
    }

    private void BButton(InputAction.CallbackContext obj)
    {
        if (inZone) PrevLine();
    }

    private void NextLine()
    {
        currentLine++;
        if (currentLine > dialogueLines.Length) return;

        if (dialogueText.text != dialogueLines[currentLine - 1].line)
        {
            StopAllCoroutines();
            speechSource.Stop();
<<<<<<< Updated upstream
            dialogueText.text = dialogueLines[currentLine - 1].line;
=======
            dialogueText.text = dialogueLines[currentLine].line;
>>>>>>> Stashed changes
            return;
        }
        StartDialogue();
    }

    private void PrevLine()
    {
        if (dialogueText.text != dialogueLines[currentLine].line)
        {
            StopAllCoroutines();
            speechSource.Stop();
            dialogueText.text = dialogueLines[currentLine].line;
            return;
        }

        currentLine--;
        if (currentLine < 0)
        {
            currentLine++;
            return;
        }

        StartDialogue();
    }

    private void StartDialogue()
    {
        StopAllCoroutines();
        StartCoroutine(StartDialogueRoutine());

        speechSource.clip = dialogueLines[currentLine].clip;
        speechSource.Play();
    }

    private IEnumerator StartDialogueRoutine()
    {
        dialogueText.text = string.Empty;

        foreach (char c in dialogueLines[currentLine].line)
        {
            keyboardClickSource.pitch = UnityEngine.Random.Range(1, 2);
            keyboardClickSource.Play();
            dialogueText.text += c;
            yield return new WaitForSeconds(dialogueShowCharCd);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            dialogueText.text = string.Empty;
            inZone = true;
            animator.SetTrigger("Appear");
            Invoke(nameof(StartDialogue), 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inZone = false;
        animator.SetTrigger("Disappear");
        keyboardClickSource.Stop();
        speechSource.Stop();

        StopAllCoroutines();
    }

}

[Serializable]
public class DialogueLine
{
    public string line;
    public AudioClip clip;
}