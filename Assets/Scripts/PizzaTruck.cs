using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PizzaTruck {
    public List<IngredientBundle> baseIngredientList;
    public List<IngredientBundle> ingredientList;

    public List<TruckUpgrade> upgradeList;
    public enum TruckLocation { School, Park, HipsterStore, OfficeDistrict, RetirementHome }
    public int ingredientLimit = 4;

    static PizzaTruck _instance;

    static System.Random rng = new System.Random();

    private PizzaTruck() { }

    public static PizzaTruck Instance {
        get {
            if (_instance == null) {
                _instance = new PizzaTruck();
            }
            return _instance;
        }
    }

    public void Init() {
        baseIngredientList = new List<IngredientBundle>();
        ingredientList = new List<IngredientBundle>();

		// Add one of each ingredient to the truck's ingredient list
        int ingredientQuantity = 1;
        foreach (Ingredient ingredient in IngredientDatabase.GetList()) {
            IngredientBundle ingredientBundle = new IngredientBundle(ingredient, ingredientQuantity);
            ingredientBundle.quantity = ingredientQuantity;
			AddIngredient(ingredientBundle);
        }
    }

    public void AddIngredient(IngredientBundle ingredientBundle) {
        ingredientList.Add(ingredientBundle);
    }

    public void AddBaseIngredient(IngredientBundle ingredientBundle) {
        baseIngredientList.Add(ingredientBundle);
    }

    public void RemoveIngredient(IngredientBundle ingredientBundle) {
        ingredientList.Remove(ingredientBundle);
    }

    public Ingredient GetRandomIngredient() {
        int r = PizzaTruck.rng.Next(0, ingredientList.Count);
        return ingredientList[r].ingredient;
    }

    public bool HasIngredientSpace {
        get { return ingredientList.Count < ingredientLimit; }
    }
}