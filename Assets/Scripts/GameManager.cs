using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject itemPrefab;

    Dictionary<string, List<SavedItem>> items;
    Dictionary<string, bool> enteranceLines;

    public SceneName lastScene = SceneName.Home;
    void Awake()
    {
        Instance = this;
        items = new Dictionary<string, List<SavedItem>>();
        enteranceLines = new Dictionary<string, bool>();
        SceneManager.sceneLoaded += SceneManager_OnSceneLoaded;
    }
    void Start()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    public SceneName GetLastScene()
    {
        return lastScene;
    }
    public void TryPlayEnteranceLine(QuestLine enteranceLine)
    {
        string sceneName = GetSceneName();

        if (!enteranceLines.ContainsKey(sceneName) && enteranceLine != null)
        {
            PlayerController.Instance.PlayQuestBubble(enteranceLine);
            enteranceLines[sceneName] = true;
        }
    }
    public void GoToScene(string name)
    {
        PlayerController.Instance.ClearInteract();
        SceneName.TryParse(GetSceneName(), out lastScene);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    void SceneManager_OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = GetSceneName();
        if (items.ContainsKey(sceneName))
        {
            foreach (SavedItem savedItem in items[sceneName])
            {
                ItemController.SpawnItem(savedItem.itemSO, savedItem.pos);
            }
        }
    }
    public void ItemCreated(ItemSO itemSO_, Vector3 pos_)
    {
        string sceneName = GetSceneName();
        if (!items.ContainsKey(sceneName))
        {
            items.Add(sceneName, new List<SavedItem>());
        }
        List<SavedItem> itemsInScene = items[sceneName];
        itemsInScene.Add(new SavedItem { pos = pos_, itemSO = itemSO_ });
        items[sceneName] = itemsInScene;
    }
    public void ItemDestroyed(ItemSO itemSO_)
    {
        string sceneName = GetSceneName();
        if (!items.ContainsKey(sceneName))
        {
            return;
        }
        List<SavedItem> itemsInScene = items[sceneName];
        for (int i = 0; i < itemsInScene.Count; i++)
        {
            SavedItem savedItem = itemsInScene[i];
            if (itemSO_.itemName == savedItem.itemSO.itemName)
            {
                itemsInScene.Remove(savedItem);
            }
        }

        items[sceneName] = itemsInScene;
    }
}

public enum SceneName
{
    Home,
    BedRoom,
    GameRoom,
    LivingRoom,
    StudyRoom,
    Kitchen
}

public class SavedItem
{
    public ItemSO itemSO;
    public Vector3 pos;
}