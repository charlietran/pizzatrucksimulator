﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour {
    public Transform[] baseIngredientSpawningPoints;
    public Transform[] ingredientSpawningPoints;
    #region Singleton
    public static IngredientSpawner Instance;

    PizzaTruck pizzaTruck;

    void Awake()
    {
        if (Instance != null) {
            Debug.LogWarning("More than one IngredientSpawner instance!");
            return;
        }
        Instance = this;
        pizzaTruck = PizzaTruck.Instance;
    }
    #endregion

    public void SpawnTruckIngredients() {
        foreach (Ingredient ingredient in pizzaTruck.baseIngredientList) {
            Instantiate(ingredient.ingredientObject, ingredient.spawnPoint);
        }

        int spawnIndex = 0;
        foreach (Ingredient ingredient in pizzaTruck.ingredientList) {

            for (int i = 0; i < ingredient.quantity; i++)
            {
                Instantiate(
                    ingredient.ingredientObject, // prefab
                    ingredientSpawningPoints[spawnIndex].position + Vector3.up * .15f * i, //position 
                    Quaternion.identity, // rotation
                    ingredientSpawningPoints[spawnIndex] // parent
                );
            }
            spawnIndex++;

        }
    }
	void Start () {
		
	}
	
	void Update () {
		
	}
}
