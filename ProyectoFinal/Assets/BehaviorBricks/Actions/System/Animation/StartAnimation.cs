using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to play an animation in the GameObject
    /// </summary>
    [Action("Animation/StartAnimation")]
    [Help("Plays an animation in the game object")]
    public class StartAnimation : GOAction
    {
        [InParam("animation")]
        [Help("The clip that must be played")]
        public string animationClip;
        /// <summary>Initialization Method of PlayAnimation.</summary>
        /// <remarks>Associate and Inacialize the animation and the elapsed time.</remarks>
        public override void OnStart()
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.Play(animationClip);
        }
        /// <summary>Method of Update of PlayAnimation.</summary>
        /// <remarks>Increase the elapsed time and check if the animation is over.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
