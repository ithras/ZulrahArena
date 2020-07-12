using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public Button inventoryButton;

    public GameObject prayerUI;
    public Button prayerButton;

    public GameObject combatUI;
    public Button combatButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            activateInventory();
        }
        else if(Input.GetButtonDown("Prayer"))
        {
            activatePrayer();
        }
        else if (Input.GetButtonDown("Combat"))
        {
            activateCombat();
        }
    }

    public void activateInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        inventoryButton.Select();

        prayerUI.SetActive(false);
        combatUI.SetActive(false);
    }

    public void activatePrayer()
    {
        inventoryUI.SetActive(false);

        prayerUI.SetActive(!prayerUI.activeSelf);
        prayerButton.Select();

        combatUI.SetActive(false);
    }

    public void activateCombat()
    {
        inventoryUI.SetActive(false);
        prayerUI.SetActive(false);

        combatUI.SetActive(!combatUI.activeSelf);
        combatButton.Select();
    }
}

