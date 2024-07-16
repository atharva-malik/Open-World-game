using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
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
    }
}
