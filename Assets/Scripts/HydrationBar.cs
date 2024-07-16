using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour
{
    private Slider hydrationSlider;
    public Text hydrationCounter;
    private float currentHydration, maxHydration;
    public GameObject playerState;

    void Awake()
    {
        hydrationSlider = GetComponent<Slider>();
    }

    void Update()
    {
        currentHydration = playerState.GetComponent<PlayerState>().currentHydration;
        maxHydration = playerState.GetComponent<PlayerState>().maxHydration;
        float fillValue = currentHydration / maxHydration;
        hydrationSlider.value = fillValue;

        hydrationCounter.text = currentHydration.ToString() + " / " + maxHydration.ToString();
    }
}
