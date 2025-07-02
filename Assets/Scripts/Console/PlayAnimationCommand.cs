using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationCommand : ICommand
{
    public string Name => "playanimation";
    public List<string> Aliasses => new List<string> { "play", "anim", "/play", "animation" };
    public string Description => "Plays an animation on all characters.";

    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            Debug.Log("Usage: playanimation <animation_name>");
            return;
        }

        string animName = args[0];
        var characters = GameObject.FindObjectsByType<CharacterAnimator>(FindObjectsSortMode.None);

        foreach (var character in characters)
        {
            Animator animator = character.GetComponent<Animator>();
            if (animator != null && HasAnimation(animator, animName))
            {
                animator.Play(animName);
                Debug.Log($"Playing '{animName}' on {character.name}");
            }
            else
            {
                Debug.LogWarning($"'{animName}' not found on {character.name}");
            }
        }
    }

    private bool HasAnimation(Animator animator, string animationName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
                return true;
        }
        return false;
    }
}
