using UnityEngine;
using System.Collections;

/**
 * Listens for events during the crocodile animation and triggers
 * methods on the crocodile object
 */
public class CrocodileBehaviour : StateMachineBehaviour {

    public CrocodileMouthController mouth;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (mouth)
        {
            if (stateInfo.IsName("CrocodileClosed"))
            {
                // remain closed for the set duration
                mouth.Invoke("OpenMouth", mouth.closedDuration);
            }
            else if (stateInfo.IsName("CrocodileOpen"))
            {
                // remain open for the set duration
                mouth.Invoke("CloseMouth", mouth.openDuration);
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
