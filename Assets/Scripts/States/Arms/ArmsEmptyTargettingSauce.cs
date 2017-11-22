﻿using UnityEngine;

public class ArmsEmptyTargettingSauce : ArmsState {
    // Animator animator;
    Camera mainCamera;
    Transform holdingArea;
    Transform sauce;
    Pizza pizza;

    public ArmsEmptyTargettingSauce(Arms _arms, Transform _sauce) : base(_arms) {
        sauce = _sauce;
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
            arms.SetState(new ArmsEmptyState(arms));
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            LiftObject();
        }

    }

    public override void OnExit() {

    }

    void LiftObject() {
        sauce.gameObject.GetComponent<Rigidbody>().useGravity = false;
        sauce.gameObject.GetComponent<BoxCollider>().enabled = false;
        sauce.SetParent(holdingArea);
        sauce.localPosition = Vector3.zero;
        arms.SetState(new ArmsHoldingSauceState(arms, sauce));
    }
}