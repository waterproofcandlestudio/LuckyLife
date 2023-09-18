//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 10/03/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    private SO_Dialogue currentDialogue;

    [Header("Dialogue Sound control")]
    [SerializeField] private int min_letterSpawn = 4;

    [SerializeField] private int max_letterSpawn = 6;

    #region Inspector Variables

    [SerializeField]
    private TextMeshProUGUI npcNameTxt;

    [SerializeField]
    private TextMeshProUGUI sentenceTxt;

    public UnityEvent onDialogueEnd;

    #endregion Inspector Variables

    private void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(SO_Dialogue dialogue)
    {
        if (dialogue == null) return;

        currentDialogue = dialogue;

        sentenceTxt.text = "";

        DisplayDialogueUI();

        sentences.Clear();

        //Add the sentences to a queue
        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        //Display the first sentence
        DisplayNextSentence();
    }

    private void DisplayDialogueUI()
    {
        npcNameTxt.text = currentDialogue.npcName;
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //Add the last sentence to dequeue
        string currentSentence = sentences.Dequeue();

        //Stop all the previous sentences display
        StopAllCoroutines();

        //Start the dequeu animation for the next sentence
        StartCoroutine(TypeSentence(currentSentence));
    }

    /// <summary>
    /// Animate the sentence to write
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    private IEnumerator TypeSentence(string sentence)
    {
        sentenceTxt.text += "";

        int i = 0;
        int letterSpawn = Random.Range(min_letterSpawn, max_letterSpawn);

        foreach (char letter in sentence.ToCharArray())
        {
            sentenceTxt.text += letter;

            i++;
            if (i % letterSpawn == 1)
            {
                letterSpawn = Random.Range(min_letterSpawn, max_letterSpawn);
                float rand = Random.Range(0.95f, 1.05f);
                AudioManager.instance.PlayClip(SoundsFX.SFX_LetterPop, rand);
                AudioManager.instance.PlayClip(SoundsFX.SFX_Talk, rand);
            }

            yield return null;
        }

        sentenceTxt.text += Environment.NewLine;
    }

    public void ClearDialogue()
    {
        StartCoroutine(ClearDelay());
    }

    private IEnumerator ClearDelay()
    {
        yield return new WaitForSeconds(2f);
        sentenceTxt.text = "";
    }

    private void EndDialogue()
    {
        sentences.Clear();
        onDialogueEnd.Invoke();
        Debug.Log("Dialogue ended");
    }
}