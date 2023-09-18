using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField]
    private Scenes sceneToGo;

    public UnityEvent<Scenes> onSceneChange;

    public void Trigger()
    {
        onSceneChange.Invoke(sceneToGo);
    }
}