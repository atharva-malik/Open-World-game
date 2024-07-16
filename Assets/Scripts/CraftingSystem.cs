using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    public List<string> inventoryItemList = new List<string>();

    // Category Buttons
    Button toolsBTN;

    // Crafting Buttons
    Button craftAxeBTN;

    // Requirement Text
    Text AxeReq1, AxeReq2;

    public bool isOpen = false;

    // All Blueprint

    public static CraftingSystem Instance { get; set; }
    
    private void Awake() {
        if (Instance != null && Instance != this){
            Destroy(gameObject);
        }else{
            Instance = this;
        }
    }

    void Start()
    {
        isOpen = false;
        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate {OpenToolsCategory();});

        // Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe/AxeReq1").GetComponent<Text>();
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
            Debug.Log("Crafting Screen Opened");
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }
}
