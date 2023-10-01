using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour, IHasInteract
{

    [SerializeField] SpriteRenderer spriteRenderer;
    ItemSO itemSO;

    public static void DropItem(ItemSO itemSO, Vector3 from)
    {
        GameObject newItem = Instantiate(GameManager.Instance.itemPrefab, from, Quaternion.identity);
        ItemController itemController = newItem.GetComponent<ItemController>();
        Vector3 loc = (PlayerController.Instance.transform.position - from) * 1f;
        loc += from;
        loc += new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f), 0);
        GameManager.Instance.ItemCreated(itemSO, loc);
        itemController.Setup(itemSO);
        itemController.MoveTo(loc);
    }

    public static void SpawnItem(ItemSO itemSO, Vector3 loc)
    {
        GameObject newItem = Instantiate(GameManager.Instance.itemPrefab, loc, Quaternion.identity);
        ItemController itemController = newItem.GetComponent<ItemController>();
        Debug.Log("itemController.transform.name " + itemController.gameObject.scene.name);

        itemController.Setup(itemSO);
        itemController.MoveTo(loc);
    }

    Vector3 targetPos;
    bool isMoving;
    void Update()
    {
        if (isMoving)
        {
            if (Vector3.Distance(transform.position, targetPos) < 0.02f)
            {
                isMoving = false;
                transform.position = targetPos;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 3f);
            }
        }
    }
    public void MoveTo(Vector3 pos)
    {
        targetPos = pos;
        isMoving = true;
    }
    public void Setup(ItemSO itemSO_)
    {
        itemSO = itemSO_;
        spriteRenderer.sprite = itemSO_.itemSprite;
    }


    public void OnHover(bool state)
    {
        UIManager.Instance.UpdateItemHintVisibility();
    }
    public void Interact(PlayerController player)
    {
        if (PlayerController.Instance.PickupItem(itemSO))
        {
            GameManager.Instance.ItemDestroyed(itemSO);
            Destroy(gameObject);
        }
    }


    public string GetName()
    {
        return itemSO.itemName;
    }
    public ItemSO GetItemSO()
    {
        return itemSO;
    }
}
