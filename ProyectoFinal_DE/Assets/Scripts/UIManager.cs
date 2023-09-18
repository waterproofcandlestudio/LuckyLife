using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyAmountTxt;

    [SerializeField]
    private TextMeshProUGUI actualDayTxt;

    [SerializeField]
    private Image pacoImage;

    [SerializeField]
    private float decreaseMoneyDuration = 1f;

    [SerializeField]
    private GameManager gameManager;

    private void Start()
    {
        moneyAmountTxt.text = gameManager.GetActualMoney().ToString();
    }

    public void RefreshUI()
    {
        actualDayTxt.text = gameManager.GetActualDay().ToString();

        //Función para hacer aparecer a paco desde poca opacidad a opacidad completa
        pacoImage.sprite = gameManager.actualNode.pacoModel;
    }

    public void UpdateMoney(bool incoming)
    {
        int amountToUpdate = incoming ? gameManager.GetActualNode().inputAmount : -gameManager.GetActualNode().outputAmount;

        StartCoroutine(DecreaseValue(amountToUpdate, decreaseMoneyDuration));
    }

    private IEnumerator DecreaseValue(int amount, float duration)
    {
        int startValue = int.Parse(moneyAmountTxt.text);
        int endValue = startValue + amount;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // Calculate the new value
            int newValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, timeElapsed / duration));

            // Update the UI Text component with the new value
            moneyAmountTxt.text = newValue.ToString();

            // Wait for the next frame
            yield return null;

            // Update the elapsed time
            timeElapsed += Time.deltaTime;
        }

        // Ensure that the final value is set correctly
        moneyAmountTxt.text = endValue.ToString();
    }

    private void OnEnable()
    {
        GameManager.nextDayDelegate += RefreshUI;
    }

    private void OnDisable()
    {
        GameManager.nextDayDelegate -= RefreshUI;
    }
}