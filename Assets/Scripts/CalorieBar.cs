using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalorieBar : MonoBehaviour
{
    private Slider calorieSlider;
    public Text calorieCounter;
    private float currentCalorie, maxCalorie;
    public GameObject playerState;

    void Awake()
    {
        calorieSlider = GetComponent<Slider>();
    }

    void Update()
    {
        currentCalorie = playerState.GetComponent<PlayerState>().currentCalories;
        maxCalorie = playerState.GetComponent<PlayerState>().maxCalories;
        float fillValue = currentCalorie / maxCalorie;
        calorieSlider.value = fillValue;

        calorieCounter.text = currentCalorie.ToString() + " / " + maxCalorie.ToString();
    }
}
