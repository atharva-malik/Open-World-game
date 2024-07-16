using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }

    // ---- Player Health ---- //
    public float currentHealth;
    public float maxHealth;

    // ---- Player Calories ---- //
    public float currentCalories;
    public float maxCalories;

    float distanceTravelled = 0;
    Vector3 lastPosition;
    public GameObject player;

    // ---- Player Hydration ---- //
    public float currentHydration;
    public float maxHydration;
    public bool isHydrationActive;

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydration = maxHydration;

        StartCoroutine(decreaseHydration());
    }

    IEnumerator decreaseHydration()
    {
        while (true){
            currentHydration -= 1;
            yield return new WaitForSeconds(10);
        }
    }

    void Update()
    {
        distanceTravelled += Vector3.Distance(player.transform.position, lastPosition);
        lastPosition = player.transform.position;

        if(distanceTravelled >= 10){
            distanceTravelled = 0;
            currentCalories -= 1;
        }
    }

    public void setHydration(float maxHydration)
    {
        currentHydration = maxHydration;
    }

    public void setCalories(float maxCalories)
    {
        currentCalories = maxCalories;
    }

    public void setHealth(float maxHealth)
    {
        currentHealth = maxHealth;
    }
}
