using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITalkable
{
    public void TriggerDialogue();

    public void ChangeDialogue(bool change);
}

public interface ITalkableComposite : ITalkable
{
    public void SelectDialogue(SO_Dialogue dialogueToShow);
}