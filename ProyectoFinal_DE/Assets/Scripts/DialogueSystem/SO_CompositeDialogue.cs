//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 19/02/2022
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Dialogue/New Dialogue List"))]
public class SO_CompositeDialogue : ScriptableObject
{
    public List<SO_Dialogue> dialogueList;

    public bool nextDialogue = true;

    public int dialogueIndex = 0;
}