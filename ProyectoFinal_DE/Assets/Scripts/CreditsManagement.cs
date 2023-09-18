using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManagement : MonoBehaviour
{
    [SerializeField] GameObject menu_Panel;
    [SerializeField] GameObject credits_Panel;

    public void Activate_MainMenu()
    {
        menu_Panel.SetActive(true);
        credits_Panel.SetActive(false);
    }
    public void Activate_Credits()
    {
        menu_Panel.SetActive(false);
        credits_Panel.SetActive(true);
    }
}
