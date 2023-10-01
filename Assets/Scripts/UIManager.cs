using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Image currentItemSprite;
    [SerializeField] Canvas interactHintCanvas;
    [SerializeField] TextMeshProUGUI interactHintTarget;
    [SerializeField] Canvas doorHintCanvas;
    [SerializeField] TextMeshProUGUI doorHintTarget;
    [SerializeField] Canvas itemHintCanvas;
    [SerializeField] TextMeshProUGUI itemHintTarget;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UpdateAllVisibility();
        PlayerController.Instance.OnItemChange += PlayerController_OnItemChange;
    }
    void PlayerController_OnItemChange(object sender, System.EventArgs e)
    {
        UpdateCurrentItemSprite();
    }
    void UpdateCurrentItemSprite()
    {
        ItemSO itemSO = PlayerController.Instance.GetItem();
        if (itemSO != null)
        {
            currentItemSprite.sprite = itemSO.itemSprite;
            currentItemSprite.color = Color.white;
        }
        else
        {
            currentItemSprite.sprite = null;
            currentItemSprite.color = new Color(0, 0, 0, .7f);
        }
    }
    public void UpdateAllVisibility()
    {
        UpdateDoorHintVisibility();
        UpdateInteractHintVisibility();
        UpdateItemHintVisibility();
    }
    public void UpdateItemHintVisibility()
    {
        IHasInteract interactable = PlayerController.Instance.GetHasInteract();
        if (interactable != null && interactable is ItemController)
        {
            ItemController itemController = (ItemController)interactable;
            bool state = itemController != null;
            if (state)
            {
                itemHintTarget.text = itemController.GetName();
            }
            itemHintCanvas.gameObject.SetActive(state);
        }
        else
        {
            itemHintCanvas.gameObject.SetActive(false);
        }
    }
    public void UpdateInteractHintVisibility()
    {
        IHasInteract interactable = PlayerController.Instance.GetHasInteract();
        if (interactable != null && interactable is QuestGiver)
        {
            QuestGiver questGiver = (QuestGiver)interactable;
            bool state = questGiver != null;
            if (state)
            {
                interactHintTarget.text = questGiver.GetName();
            }
            interactHintCanvas.gameObject.SetActive(state);
        }
        else
        {
            interactHintCanvas.gameObject.SetActive(false);
        }
    }
    public void UpdateDoorHintVisibility()
    {
        IHasInteract interactable = PlayerController.Instance.GetHasInteract();

        if (interactable != null && interactable is DoorController)
        {
            DoorController doorController = (DoorController)interactable;
            bool state = doorController != null;
            if (state)
            {
                doorHintTarget.text = doorController.GetName();
            }
            doorHintCanvas.gameObject.SetActive(state);
        }
        else
        {
            doorHintCanvas.gameObject.SetActive(false);
        }
    }

    BubbleController lastBubble;
    public void SetLatestBubble(BubbleController bubbleController)
    {
        if (lastBubble != null && lastBubble != bubbleController)
        {
            lastBubble.Hide();
        }
        lastBubble = bubbleController;
    }
}
