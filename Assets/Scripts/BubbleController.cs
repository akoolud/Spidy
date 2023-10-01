using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleController : MonoBehaviour
{
    [SerializeField] Canvas bubbleCanvas;
    [SerializeField] TextMeshProUGUI description;


    IEnumerator hideCaroutine;
    void Awake()
    {
        bubbleCanvas.gameObject.SetActive(false);
    }
    public void PlayQuest(QuestLine questLine)
    {
        description.text = questLine.line;
        bubbleCanvas.gameObject.SetActive(true);


        if (hideCaroutine != null)
        {
            StopCoroutine(hideCaroutine);
        }
        hideCaroutine = HideAfterAWhile();
        StartCoroutine(hideCaroutine);
    }


    public void Hide()
    {
        description.text = "[D]";
        bubbleCanvas.gameObject.SetActive(false);
    }
    IEnumerator HideAfterAWhile()
    {
        UIManager.Instance.SetLatestBubble(this);
        yield return new WaitForSeconds(3.5f);
        Hide();
    }
}
