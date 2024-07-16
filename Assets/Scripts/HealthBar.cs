using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthSlider;
    public Text healthCounter;
    private float currentHealth, maxHealth;
    public GameObject playerState;

    void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

    void Update()
    {
        currentHealth = playerState.GetComponent<PlayerState>().currentHealth;
        maxHealth = playerState.GetComponent<PlayerState>().maxHealth;
        float fillValue = currentHealth / maxHealth;
        healthSlider.value = fillValue;

        healthCounter.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
