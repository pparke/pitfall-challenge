using UnityEngine;
using System.Collections;

/**
 * Invokes open and close methods on the associated pit when a state change is detected
 */
public class PitBehaviour : StateMachineBehaviour {

    public PitController pit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (pit)
        {
            if (stateInfo.IsName("PitClosed"))
            {
                // open the pit again after the closed duration has elapsed
                pit.Invoke("OpenPit", pit.closedDuration);
            }
            else if (stateInfo.IsName("PitOpen"))
            {
                // close the pit again after the open duration has elapsed
                pit.Invoke("ClosePit", pit.openDuration);
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
