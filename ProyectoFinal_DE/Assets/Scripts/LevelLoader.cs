using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Animator transition;

    [SerializeField]
    private float transitionTime;

    [SerializeField]
    private GameObject image;

    private void Awake()
    {
        image.SetActive(true);
    }

    public void LoadNextLevel(Scenes _sceneToGoTo)
    {
        string sceneToGoTo = "";

        switch (_sceneToGoTo)
        {
            case Scenes.MainMenu:
                sceneToGoTo = "MainMenu";
                break;

            case Scenes.Game:
                sceneToGoTo = "UI Testing";
                break;

            case Scenes.EndGame:
                sceneToGoTo = "FinalScene";
                break;

            default:
                break;
        }

        StartCoroutine(LoadLevel(sceneToGoTo));
    }

    private IEnumerator LoadLevel(string sceneToGoTo)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneToGoTo);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel("MainMenu"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

public enum Scenes
{
    MainMenu,
    Game,
    EndGame,
    None
}