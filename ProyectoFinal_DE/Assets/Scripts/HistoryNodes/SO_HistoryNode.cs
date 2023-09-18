using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = ("Node/History Node"))]
public class SO_HistoryNode : ScriptableObject
{
    [Header("Dialogues")]
    [Space(15)]
    public SO_Dialogue historyText;

    public SO_Dialogue goodEndText;
    public SO_Dialogue badEndText;

    [Header("Nodes")]
    [Space(15)]
    public SO_HistoryNode nodeDontGive;

    public SO_HistoryNode nodeGive;

    [Header("Money Quantities")]
    [Space(15)]
    public int inputAmount = 100;

    public int outputAmount = 150;

    [Header("Prefab Model")]
    [Space(15)]
    public Sprite pacoModel;

    public bool isEnd()
    {
        if (nodeGive == null && nodeDontGive == null)
        {
            return true;
        }

        return false;
    }
}