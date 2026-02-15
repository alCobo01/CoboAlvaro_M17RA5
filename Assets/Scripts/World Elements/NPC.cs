using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AnimationBehaviour))]
[RequireComponent(typeof(Collider))]
public class NPC : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => "to talk with Gaunfaul";
    
    [SerializeField] private NPCDialogue dialogueData;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText, nameText;
    [SerializeField] private Image portraitImage;
    
    private AnimationBehaviour _animationBehaviour;
    private PlayerMovementController _currentPlayerMovement;
    private int _dialogueIndex;
    private bool _isTyping, _isDialogueActive;

    private void Awake() => _animationBehaviour = GetComponent<AnimationBehaviour>();

    public void Interact(GameObject interactor)
    {
        if (_isDialogueActive) 
            NextLine();
        else
        {
            LockPlayerMovement(interactor);
            StartDialogue();
        }
    }

    private void LockPlayerMovement(GameObject interactor)
    {
        if (interactor.TryGetComponent(out PlayerMovementController playerMovement))
        {
            _currentPlayerMovement = playerMovement;
            _currentPlayerMovement.SetMovementLocked(true);
        }
    }

    private void StartDialogue()
    {
        _isDialogueActive = true;
        _dialogueIndex = 0;
        
        _animationBehaviour.TriggerTalk();

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLine());
    }

    private void NextLine()
    {
        if (_isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[_dialogueIndex]);
            _isTyping = false;
        }
        else
        {
            _dialogueIndex++;
            if (_dialogueIndex < dialogueData.dialogueLines.Length)
                StartCoroutine(TypeLine());
            else
                EndDialogue();
        }
    }

    private IEnumerator TypeLine()
    {
        _isTyping = true;
        dialogueText.SetText("");

        foreach(var letter in dialogueData.dialogueLines[_dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        _isTyping = false;
    }
    
    private void EndDialogue()
    {
        StopAllCoroutines();
        _isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);

        if (_currentPlayerMovement == null) return;
        _currentPlayerMovement.SetMovementLocked(false);
        _currentPlayerMovement = null;
    }
}
