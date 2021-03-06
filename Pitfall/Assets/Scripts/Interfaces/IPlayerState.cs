﻿using UnityEngine;
using System.Collections;

/**
 * Interface for player state used in the state machine in PlayerController
 * based on https://unity3d.com/learn/tutorials/topics/scripting/using-interfaces-make-state-machine-ai?playlist=17117
 */
public interface IPlayerState {

    // physics related update
    void fixedUpdate();

    // called every frame
    void update();

    // collision with a trigger
    void onTriggerEnter(Collider2D coll);

    void onTriggerStay(Collider2D coll);

    void onTriggerExit(Collider2D coll);

    // called when state entered
    void enter();

    // called before state left
    void exit();
}
