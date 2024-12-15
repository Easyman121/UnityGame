using UnityEngine;
using System.Collections;
public class StarFishAnimation : MonoBehaviour
{
    public Animator starfishAnimator; // Reference to the Animator component
    public string animationName = "Armature|StarDance"; // Name of the animation to play

    private void Start()
    {
        if (starfishAnimator == null)
        {
            Debug.LogError("Animator not assigned!");
            return;
        }

        // Start the coroutine to play animation every 20 seconds
        StartCoroutine(PlayAnimationEvery20Seconds());
    }

    private IEnumerator PlayAnimationEvery20Seconds()
    {
        while (true) // Infinite loop to keep playing the animation
        {
            // Play the animation
            float range = Random.Range(10f, 70f);
            starfishAnimator.Play(animationName, -1, 0f);
            
            // Wait for 20 seconds
            yield return new WaitForSeconds(range);
        }
    }
}
