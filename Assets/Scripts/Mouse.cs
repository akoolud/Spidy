using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : QuestGiver
{
    [SerializeField] Transform visual;


    [SerializeField] QuestLine knockLine;
    [SerializeField] QuestLine[] preQuestLines;
    [SerializeField] QuestLine[] pastQuestLines;
    [SerializeField] ItemSO outputitem;
    [SerializeField] ItemSO inputItem;
    [SerializeField] Transform fishy;


    int preQuestLineIndex;
    int pastQuestLineIndex;

    bool isOut;
    void Start()
    {
        Hide();
    }

    void Show()
    {
        isOut = true;
        visual.gameObject.SetActive(true);
        StartCoroutine(HideAfter10());
    }
    void Hide()
    {
        isOut = false;
        visual.gameObject.SetActive(false);
    }

    IEnumerator HideAfter10()
    {
        yield return new WaitForSeconds(10);
        Hide();
    }
    public override void Interact(PlayerController player)
    {
        if (isOut)
        {
            if (preQuestLines != null && preQuestLineIndex < preQuestLines.Length)
            {
                QuestLine questLine = preQuestLines[preQuestLineIndex];
                if (questLine.owner == QuestLineOwner.Player)
                {
                    player.PlayQuestBubble(questLine);
                }
                else
                {
                    this.PlayQuestBubble(questLine);
                }
                preQuestLineIndex++;
            }
            else if (player.HasItem() && player.GetItem() == inputItem)
            {
                fishy.gameObject.SetActive(false);
                PlayerController.Instance.ClearItem(transform);
                ItemController.DropItem(outputitem, transform.position);
            }
            else if (pastQuestLines != null && pastQuestLineIndex < pastQuestLines.Length)
            {
                QuestLine questLine = pastQuestLines[pastQuestLineIndex];
                if (questLine.owner == QuestLineOwner.Player)
                {
                    player.PlayQuestBubble(questLine);
                }
                else
                {
                    this.PlayQuestBubble(questLine);
                }
            }
        }
        else
        {
            player.PlayQuestBubble(knockLine);
            Show();
        }
    }
}
