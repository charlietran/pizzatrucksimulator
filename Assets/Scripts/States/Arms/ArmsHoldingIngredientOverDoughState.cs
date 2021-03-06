using UnityEngine;

public class ArmsHoldingIngredientOverDoughState : ArmsState {
    Pizza pizza;
    Transform dough;
    Transform ingredient;

    public ArmsHoldingIngredientOverDoughState(Arms _arms, Transform _ingredient, Transform _dough) : base(_arms) {
        ingredient = _ingredient;
        dough = _dough;
        pizza = dough.GetComponent<Pizza>();
    }

    public override void OnEnter() { }
    public override void OnExit() { }
    public override void Tick() {
        RaycastHit hitInfo;

        int layerMask = 1 << 10;
        bool hitSomething = Physics.Raycast(
            arms.mainCamera.transform.position,
            arms.mainCamera.transform.forward,
            out hitInfo,
            10f,
            layerMask
        );

        string ingredientType = pizza.GetIngredientType(ingredient);

        if (!hitSomething) {
            switch (ingredientType) {
                case "Sauce":
                    arms.SetState(new ArmsHoldingSauceState(arms, ingredient));
                    break;
                case "Cheese":
                    arms.SetState(new ArmsHoldingCheeseState(arms, ingredient));
                    break;
                case "Topping":
                    arms.SetState(new ArmsHoldingToppingState(arms, ingredient));
                    break;
            }
            return;
        }

        if (!pizza.CanReceiveIngredient(ingredient)) {
            // arms.ShowWarning("Pizza needs Sauce and Cheese");
            return;
        }
        // arms.HideWarning();

        if (Input.GetMouseButtonDown(0)) {
            Arms.Instance.GetComponent<AudioSource>().clip = Arms.Instance.Sprinkle;
            Arms.Instance.GetComponent<AudioSource>().pitch = Random.Range(.9f,1.2f);
            Arms.Instance.GetComponent<AudioSource>().Play();
            pizza.AddToPizza(ingredient);
            arms.SetState(new ArmsEmptyState(arms));
        }
    }
}