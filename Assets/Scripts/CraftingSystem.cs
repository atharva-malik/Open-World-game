using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI, survivalScreenUI, refineScreenUI;

    public List<string> inventoryItemList = new List<string>();

    // Category Buttons
    Button toolsBTN, survivalBTN, refineBTN;

    // Crafting Buttons
    Button craftAxeBTN, craftPlankBTN;

    // Requirement Text
    Text AxeReq1, AxeReq2, PlankReq1;

    public bool isOpen = false;

    // All Blueprints
    private Blueprint AxeBLP = new Blueprint(1, "Axe", "Stone", "Stick", 3, 3, 2);
    private Blueprint PlankBLP = new Blueprint(2, "Plank", "Log", "", 1, 0, 1);

    public static CraftingSystem Instance { get; set; }
    
    private void Awake() {
        if (Instance != null && Instance != this){
            Destroy(gameObject);
        }else{
            Instance = this;
        }
        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate {OpenToolsCategory();});
        
        survivalBTN = craftingScreenUI.transform.Find("SurvivalButton").GetComponent<Button>();
        survivalBTN.onClick.AddListener(delegate {OpenSurvivalCategory();});
        
        refineBTN = craftingScreenUI.transform.Find("RefineButton").GetComponent<Button>();
        refineBTN.onClick.AddListener(delegate {OpenRefineCategory();});

        // Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe/req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe/req2").GetComponent<Text>();

        craftAxeBTN = toolsScreenUI.transform.Find("Axe/Craft").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate {CraftItem(AxeBLP);});

        // Plank
        PlankReq1 = refineScreenUI.transform.Find("Planks/req1").GetComponent<Text>();

        craftPlankBTN = refineScreenUI.transform.Find("Planks/Craft").GetComponent<Button>();
        craftPlankBTN.onClick.AddListener(delegate {CraftItem(PlankBLP);});
    }

    void Start()
    {
        craftAxeBTN.interactable = false;
        isOpen = false;
    }

    private void CraftItem(Blueprint BLP){
        for (int i = 0; i < BLP.numberOfItemsToProduce; i++)
            InventorySystem.Instance.AddToInventory(BLP.itemName);
        
        InventorySystem.Instance.RemoveItem(BLP.Req1, BLP.Req1Amt);
        if (BLP.numberOfRequirements == 2)
            InventorySystem.Instance.RemoveItem(BLP.Req2, BLP.Req2Amt);

        StartCoroutine(calculate());

        RefreshNeededItems();

    }

    public IEnumerator calculate(){
        yield return new WaitForSeconds(1f);
        InventorySystem.Instance.ReCalculateList();
    }

    public void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;
        int log_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;
        
        foreach (string item in inventoryItemList){
            switch (item){
                case "Stone":
                    stone_count++;
                    break;
                case "Stick":
                    stick_count++;
                    break;
                case "Log":
                    log_count++;
                    break;
            }
        }

        //---- AXE ----//
        AxeReq1.text = "3 Stone [" + stone_count.ToString() + "]";
        AxeReq2.text = "3 Stick [" + stick_count.ToString() + "]";
        if (stone_count >= 3 && stick_count >= 3 && InventorySystem.Instance.checkSlotsAvailable(1)){
            craftAxeBTN.interactable = true;
        }else
            craftAxeBTN.interactable = false;

        //---- Plank ----//
        PlankReq1.text = "1 Log [" + log_count.ToString() + "]";
        if (log_count >= 1 && InventorySystem.Instance.checkSlotsAvailable(2)){
            craftPlankBTN.interactable = true;
        }else
            craftPlankBTN.interactable = false;
    }

    void OpenToolsCategory(){
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
        survivalScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
    }

    void OpenSurvivalCategory(){
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
        survivalScreenUI.SetActive(true);
        refineScreenUI.SetActive(false);
    }

    void OpenRefineCategory(){
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        refineScreenUI.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            Cursor.visible = true;
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
            SelectionManager.Instance.disableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            survivalScreenUI.SetActive(false);
            refineScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen){
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SelectionManager.Instance.enableSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
            }
            isOpen = false;
        }
    }
}
