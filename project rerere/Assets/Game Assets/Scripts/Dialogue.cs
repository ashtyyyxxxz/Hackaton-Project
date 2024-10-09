using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// better use ready prefab
[RequireComponent(typeof(Collider))]
public class Dialogue : MonoBehaviour
{
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private float dialogueShowCharCd = 0.07f;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private AudioSource keyboardClick;

    [Header("Actions")]
    [SerializeField] private InputActionReference aButtonAction;
    [SerializeField] private InputActionReference bButtonAction;

    private Animator animator;


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
        NextLine();
    }

    private void BButton(InputAction.CallbackContext obj)
    {
        PrevLine();
    }

    private int currentLine;

    private void NextLine()
    {
        if(dialogueText.text != dialogueLines[currentLine])
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentLine];
            return;
        }
        currentLine++;
        if (currentLine > dialogueLines.Length) return;
        StartDialogue();
    }
    
    private void PrevLine()
    {
        if (dialogueText.text != dialogueLines[currentLine])
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentLine];
            return;
        }

        currentLine--;
        if(currentLine < 0) return;
        StartDialogue();
    }

    private void StartDialogue()
    {
        StopAllCoroutines();
        StartCoroutine(StartDialogueRoutine());
    }

    private IEnumerator StartDialogueRoutine()
    {
        dialogueText.text = string.Empty;

        foreach(char c in dialogueLines[currentLine])
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
            animator.SetTrigger("Appear");
            StartDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetTrigger("Disappear");
    }

}
