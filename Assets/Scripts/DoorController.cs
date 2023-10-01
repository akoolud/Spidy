using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour, IHasInteract
{
    [SerializeField] SceneName nextScene;


    void GoToNextScene()
    {
        GameManager.Instance.GoToScene(nextScene.ToString());
    }
    public string GetName()
    {
        return nextScene.ToString();
    }

    public void OnHover(bool state)
    {
        UIManager.Instance.UpdateDoorHintVisibility();
    }
    public void Interact(PlayerController player)
    {
        GoToNextScene();
    }
}
