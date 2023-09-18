using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void NextDayEvent();

    public static event NextDayEvent nextDayDelegate;

    private int actualDay = 1;

    [SerializeField]
    private int startMoneyAmount = 700;

    private int actualMoney;

    [SerializeField]
    private SO_HistoryNode rootNode;

    [HideInInspector]
    public SO_HistoryNode actualNode;

    private DialogueTrigger dialogueTrigger;

    [SerializeField]
    private DialogueTrigger pacoDialogue;

    [SerializeField]
    private Animator transitionAnim;

    private ChangeSceneTrigger sceneTrigger;

    private void Awake()
    {
        actualNode = rootNode;
        actualMoney = startMoneyAmount;
    }

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueTrigger.SetDialogue(actualNode.historyText);
        sceneTrigger = GetComponent<ChangeSceneTrigger>();

        StartCoroutine(FirstDay());
    }

    private IEnumerator FirstDay()
    {
        yield return new WaitForSeconds(3f);

        nextDayDelegate.Invoke();
    }

    private void GiveMoney(int moneyToGive)
    {
        moneyToGive -= actualMoney;

        if (actualMoney < 0)
        {
            //We lost

            //TODO: METER AQUÍ UNA PANTALLA DE DERROTA

            actualMoney = 0;
        }
    }

    public void NextDay()
    {
        //Invoca todos los métodos suscritos a este método
        StartCoroutine(NextDayCoroutine());

        transitionAnim.SetTrigger("DayChange");
    }

    private IEnumerator NextDayCoroutine()
    {
        yield return new WaitForSeconds(5f);

        actualDay++;

        nextDayDelegate.Invoke();

        Debug.Log("Nuevo día de Paco en el bar");
    }

    public void NextNode(bool node)
    {
        PacoSetDialogue(node);

        if (actualNode.isEnd())
        {
            //Dialogo de paco en base a si ha ganado o no
            pacoDialogue.TriggerDialogue();

            StartCoroutine(EndGame());

            return;
        }

        if (!node)
        {
            //DONT GIVE NODE
            actualNode = actualNode.nodeDontGive;
        }
        else
        {
            //GIVE NODE
            GiveMoney(actualNode.outputAmount);

            actualNode = actualNode.nodeGive;
        }

        //Dialogo de paco en base a si ha ganado o no
        pacoDialogue.TriggerDialogue();

        //Nuevo diálogo para el día siguiente
        dialogueTrigger.SetDialogue(actualNode.historyText);
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3f);

        sceneTrigger.Trigger();
    }

    private void PacoSetDialogue(bool node)
    {
        if (!node)
            pacoDialogue.SetDialogue(actualNode.badEndText);
        else
            pacoDialogue.SetDialogue(actualNode.goodEndText);
    }

    public int GetActualDay() => actualDay;

    public int GetActualMoney() => actualMoney;

    public SO_HistoryNode GetActualNode() => actualNode;
}