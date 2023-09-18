using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    private SO_Dialogue dialogue;

    [SerializeField]
    private UnityEvent<SO_Dialogue> OnStartDialogue;

    public bool isPaco;

    public void TriggerDialogue()
    {
        if (dialogue == null) return;
        OnStartDialogue.Invoke(dialogue);
    }

    public void SetDialogue(SO_Dialogue dialogueToDisplay)
    {
        dialogue = dialogueToDisplay;
    }

    private void OnEnable()
    {
        if (isPaco)
            return;

        GameManager.nextDayDelegate += TriggerDialogue;
    }

    private void OnDisable()
    {
        if (isPaco)
            return;

        GameManager.nextDayDelegate -= TriggerDialogue;
    }
}