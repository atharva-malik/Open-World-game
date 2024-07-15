using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }
    public GameObject inventoryScreenUI;
    public bool isOpen;
    public Transform player;

    private MouseMovement mouseMovement;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        mouseMovement = player.GetComponent<MouseMovement>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            playerMovement.enabled = false;
            mouseMovement.enabled = false;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            playerMovement.enabled = true;
            mouseMovement.enabled = true;
            isOpen = false;
        }
    }
}