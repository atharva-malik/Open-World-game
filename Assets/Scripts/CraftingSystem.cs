using System;
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

    // All Blueprints
    private Blueprint AxeBLP = new Blueprint("Axe", "Stone", "Stick", 3, 3, 2);

    public static CraftingSystem Instance { get; set; }
    
    private void Awake() {
        if (Instance != null && Instance != this){
            Destroy(gameObject);
        }else{
            Instance = this;
        }
        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate {OpenToolsCategory();});
        // Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe/req1").GetComponent<Text>();
        // AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe/req2").GetComponent<Text>();
        // AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();

        craftAxeBTN = toolsScreenUI.transform.Find("Axe/Craft").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate {CraftItem(AxeBLP);});
    }

    void Start()
    {
        craftAxeBTN.interactable = false;
        isOpen = false;
    }

    private void CraftItem(Blueprint BLP){
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
        inventoryItemList = InventorySystem.Instance.itemList;
        foreach (string item in inventoryItemList){
            switch (item){
                case "Stone":
                    stone_count++;
                    break;
                case "Stick":
                    stick_count++;
                    break;
            }
        }

        //---- AXE ----//
        AxeReq1.text = "3 Stone [" + stone_count.ToString() + "]";
        AxeReq2.text = "3 Stick [" + stick_count.ToString() + "]";
        if (stone_count >= 3 && stick_count >= 3){
            craftAxeBTN.interactable = true;
        }else
            craftAxeBTN.interactable = false;
    }

    void OpenToolsCategory(){
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
                Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }
}
