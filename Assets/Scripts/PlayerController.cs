using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public event EventHandler OnItemChange;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] LayerMask obstacleLayer;

    [SerializeField] BubbleController bubbleController;

    IHasInteract currentHasInteract;

    [SerializeField] ItemSO currentItem;
    float speed = 2f;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        OnItemChange?.Invoke(this, EventArgs.Empty);
    }

    void Update()
    {

        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        float c = speed * Time.deltaTime;
        Vector3 moveDir = (Vector3.right * deltaX +
                                Vector3.up * deltaY);
        if (moveDir != Vector3.zero)
        {
            float angle = Mathf.Atan2(-moveDir.x, moveDir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            animator.SetBool("Walking", true);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            animator.SetBool("Walking", false);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position,
                transform.up,
                .4f,
                obstacleLayer);
        if (!hit)
        {
            transform.position += moveDir * c;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentHasInteract != null)
            {
                currentHasInteract.Interact(this);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (this.HasItem())
            {
                ItemController.DropItem(GetItem(), transform.position + transform.forward);
                ClearItem(transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.GoToMenu();
        }
    }

    public void SetDancing(bool s)
    {
        animator.SetBool("Dancing", s);
    }
    public IHasInteract GetHasInteract()
    {
        return currentHasInteract;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<IHasInteract>(out IHasInteract hasInteract))
        {
            if (currentHasInteract == null || hasInteract is ItemController)
            {
                currentHasInteract = hasInteract;
                currentHasInteract.OnHover(true);
                UIManager.Instance.UpdateAllVisibility();
            }

        }
        // else
        // {
        //     IHasInteract tempInteract = currentHasInteract;
        //     currentHasInteract = null;
        //     tempInteract.OnHover(false);
        //     UIManager.Instance.UpdateAllVisibility();
        // }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.TryGetComponent<IHasInteract>(out IHasInteract hasInteract))
        {
            if (currentHasInteract != null)
            {
                IHasInteract tempInteract = currentHasInteract;
                currentHasInteract = null;
                tempInteract.OnHover(false);
            }
            UIManager.Instance.UpdateAllVisibility();
        }
    }
    public void ClearInteract()
    {
        if (currentHasInteract != null)
        {
            IHasInteract tempInteract = currentHasInteract;
            currentHasInteract = null;
            tempInteract.OnHover(false);
        }
    }

    public void PlayQuestBubble(QuestLine questLine)
    {
        bubbleController.PlayQuest(questLine);
    }

    public bool HasItem()
    {
        return currentItem != null;
    }
    public ItemSO GetItem()
    {
        return currentItem;
    }
    public bool PickupItem(ItemSO itemSO)
    {
        if (!HasItem())
        {
            currentItem = itemSO;
            OnItemChange?.Invoke(this, EventArgs.Empty);
            return true;
        }
        return false;
    }

    public void ClearItem(Transform toTransform)
    {
        currentItem = null;
        OnItemChange?.Invoke(this, EventArgs.Empty);
    }
}
