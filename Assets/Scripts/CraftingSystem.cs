using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public Transform player;

    private MouseMovement mouseMovement;
    private PlayerMovement playerMovement;

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    public List<string> inventoryItemList = new List<string>();

    // Category Buttons
    Button toolsBTN;

    // Crafting Buttons
    Button craftAxeBTN;

    // Requirement Text
    Text AxeReq1, AxeReq2;

    bool isOpen = false;

    // All Blueprint

    public static CraftingSystem Instance { get; set; }
    
    private void Awake() {
        if (Instance == null && Instance!= this){
            Destroy(gameObject);
        }else{
            Instance = this;
        }
        mouseMovement = player.GetComponent<MouseMovement>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Start()
    {
        isOpen = false;
        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate {OpenToolsCategory();});
    }

    void OpenToolsCategory(){
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            playerMovement.enabled = false;
            mouseMovement.enabled = false;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            playerMovement.enabled = true;
            mouseMovement.enabled = true;
            isOpen = false;
        }
    }
}
