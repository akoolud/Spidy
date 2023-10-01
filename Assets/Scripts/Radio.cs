using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : QuestGiver
{

    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;

    bool isPlaying;

    void Start()
    {
        animator.SetBool("Playing", isPlaying);
    }
    public override void Interact(PlayerController player)
    {
        isPlaying = !isPlaying;
        if (isPlaying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
        PlayerController.Instance.SetDancing(isPlaying);
        animator.SetBool("Playing", isPlaying);
    }
}
