﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public AudioSource introMusic;
    public Text startText;
    public float startTextBlinkSpeed = 0.5f;
    public Image pizzaImage;
    public float pizzaRotation;
    bool showingInstructions = false;
    public CanvasGroup introCanvas;
    public CanvasGroup instructionsCanvas;

    // Use this for initialization
    void Start() {
        StartCoroutine(BlinkStartText());
        introCanvas.alpha = 1;
        instructionsCanvas.alpha = 0;
        pizzaRotation = pizzaImage.transform.rotation.z;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (showingInstructions) {
                SceneManager.LoadScene("Game");
            } else {
                introCanvas.alpha = 0;
                instructionsCanvas.alpha = 1;
				showingInstructions = true;
            }
        }
        pizzaImage.transform.Rotate(0f, 0f, 10f * Time.deltaTime);
    }

    IEnumerator BlinkStartText() {
        bool displayText = true;
        while (true) {
            startText.enabled = displayText;
            displayText = !displayText;
            yield return new WaitForSeconds(startTextBlinkSpeed);
        }
    }
}
