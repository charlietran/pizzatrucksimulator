﻿using UnityEngine;

public class ArmsEmptyTargettingCheese : ArmsState {
    // Animator animator;
    Camera mainCamera;
    Transform holdingArea;
    Transform cheese;
    Pizza pizza;

    public ArmsEmptyTargettingCheese(Arms _arms, Transform _cheese) : base(_arms) {
        cheese = _cheese;
    }

    public override void OnEnter() {
        // animator = arms.animator;
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
                LiftObject();
        }

    }

    public override void OnExit() {

    }

        void LiftObject() {
            
       // arms.heldObject = target;

        cheese.gameObject.GetComponent<Rigidbody>().useGravity = false;
        cheese.gameObject.GetComponent<BoxCollider>().enabled = false;
        cheese.SetParent(holdingArea);
        cheese.localPosition = Vector3.zero;

        arms.SetState(new ArmsHoldingCheeseState(arms, cheese));
    }

}