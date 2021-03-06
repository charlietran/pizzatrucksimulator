using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {
    public Transform customerPrefab;
    public Transform spawnArea;
    public Transform truckWindow;

    public event System.Action OnCustomerServed;
    public event System.Action OnCustomerFailed;

    Turn currentTurn;
    int customersRemainingInWave;
    int wave = 1;

    #region Singleton
    public static CustomerSpawner Instance;

    void Awake()
    {
        if (Instance != null) {
            Debug.LogWarning("More than one CustomerSpawner instance!");
            return;
        }
        Instance = this;
    }
    #endregion

    public void Begin() {
        Debug.Log("CustomerSpawner Begin()");
        currentTurn = GameManager.Instance.currentTurn;
        customersRemainingInWave = currentTurn.customerCount;
        StartCoroutine(SpawnCustomers());
        // get turn info
        // start spawning customers
    }

    void Update() {
        if (customersRemainingInWave == 0) {
            customersRemainingInWave = currentTurn.customerCount + wave;
            wave++;
            StartCoroutine(SpawnCustomers());
        }
    }

    IEnumerator SpawnCustomers() {
        Debug.Log("CustomerSpawner SpawnCustomers(), spawning customers: " + customersRemainingInWave);
        // float spawningRadius = spawnArea.localScale.y;

        for(int i = 0; i < customersRemainingInWave; i++) {
            // Debug.Log("CustomerSpawner Spawning Customer " + i);
            Vector3 randomPosition = new Vector3(
                                         Random.Range(-7f, 7f), 
                                         0, 
                                         Random.Range(-1f, 4f)
                                     );
            Transform customerObject = Instantiate(customerPrefab);
            customerObject.parent = transform;
            customerObject.localPosition = randomPosition;
            Customer customer = customerObject.GetComponent<Customer>();
            customer.SetDestination(truckWindow.position);
            customer.OnPizzaReceive += OnCustomerPizzaReceive;
            customer.OnSuccess += OnCustomerServed;
            customer.OnFailure += OnCustomerFailed;
            yield return new WaitForSeconds(1);
        }
    }

    void OnCustomerPizzaReceive() {
        customersRemainingInWave--;
    }
}