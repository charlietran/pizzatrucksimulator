﻿using System.Collections;
using UnityEngine;

public class WorkPhase : Phase {
	public override void Begin() {
		print("WorkPhase Begin()");
		base.Begin();
		GameManager.Instance.firstPersonController.enabled = true;
		Arms.Instance.controlsEnabled = true;
		SetupWorkArea();
	}

	void SetupWorkArea() {
		IngredientSpawner.Instance.SpawnTruckIngredients();
	}

	public override void End() {
		print("WorkPhase End()");
		GameManager.Instance.firstPersonController.enabled = false;
		Arms.Instance.controlsEnabled = false;
		base.End();
	}
}