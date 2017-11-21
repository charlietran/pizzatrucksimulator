﻿using UnityEngine;

public class ArmsEmptyTargettingOven : ArmsState {

    Animator animator;
    Camera mainCamera;
    Transform holdingArea;
    Transform target;

    public ArmsEmptyTargettingOven(Arms _arms, Transform _target) : base(_arms) {

        target = _target;

    }

    public override void OnEnter() {

        animator = arms.animator;
        mainCamera = arms.mainCamera;
        holdingArea = arms.holdingArea;
    }

    public override void Tick() {

        RaycastHit objectInfo;
        int layerMask = 1 << 10;

        bool hitSomething = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out objectInfo, 10f, layerMask);

        if (!hitSomething) {
            arms.SetState (new ArmsEmptyState(arms));
            return;
        }

        if (Input.GetMouseButtonDown(0)){
		
           Debug.Log("It's an oven, mane.");
        }

    }

    public override void OnExit() {

    }

}
