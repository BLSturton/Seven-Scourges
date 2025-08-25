using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<string> itemList;
    [SerializeField] public List<GameObject> itemVisual;
    [SerializeField] public GameObject inventoryPicker;

    [SerializeField] public bool inventoryOn;
    [SerializeField] public bool inventoryPickerOn;
    [SerializeField] public bool canInventory;
    [SerializeField] GameObject inventoryImage;
    [SerializeField] PlayerMove playerMove;

    [SerializeField] public int selectedItem;
    [SerializeField] public int maxItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryOn = false;
        inventoryPickerOn = false;
        inventoryImage.SetActive(false);
        inventoryPicker.SetActive(false);
    }
    private void Awake()
    {



    }

    // Update is called once per frame
    void Update()
    {
        maxItem = itemList.Count;
        if (Input.GetKeyDown(KeyCode.D) && inventoryPickerOn)
        {
            if (selectedItem == maxItem)
            {
                selectedItem = 1;
                inventoryPicker.transform.position = itemVisual[selectedItem - 1].transform.GetChild(0).transform.position;
                
            }
            else
            {
                selectedItem++;
                inventoryPicker.transform.position = itemVisual[selectedItem - 1].transform.GetChild(0).transform.position;

            }
        }
        if (Input.GetKeyDown(KeyCode.A) && inventoryPickerOn)
        {
            if (selectedItem == 1)
            {
                selectedItem = maxItem;
                inventoryPicker.transform.position = itemVisual[selectedItem - 1].transform.GetChild(0).transform.position;

            }
            else
            {
                selectedItem--;
                inventoryPicker.transform.position = itemVisual[selectedItem - 1].transform.GetChild(0).transform.position;

            }
        }
        if (Input.GetKeyDown(KeyCode.J) && !inventoryOn && canInventory && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Battle"))
            {
            selectedItem = 1;
            inventoryPicker.transform.position = itemVisual[selectedItem - 1].transform.GetChild(0).transform.position;

            playerMove.canMove = false;
                inventoryOn = true;
                inventoryImage.SetActive(true);
                inventoryPicker.SetActive(true);
                inventoryPickerOn = true;
                foreach (GameObject obj in itemVisual)
                {
                    if (obj == itemVisual[0] && itemList.Count >= 0)
                    {
                        obj.GetComponent<TextMeshProUGUI>().text = itemList[0];

                    }
                    if (obj == itemVisual[1] && itemList.Count >= 1)
                    {
                        obj.GetComponent<TextMeshProUGUI>().text = itemList[1];

                    }
                    if (obj == itemVisual[2] && itemList.Count >= 2)
                    {
                        obj.GetComponent<TextMeshProUGUI>().text = itemList[2];
                    }
                    if (obj == itemVisual[3] && itemList.Count >= 3)
                    {
                        obj.GetComponent<TextMeshProUGUI>().text = itemList[3];
                    }
                    if (obj == itemVisual[4] && itemList.Count >= 4)
                    {
                        obj.GetComponent<TextMeshProUGUI>().text = itemList[4];
                    }
                    if (obj == itemVisual[5] && itemList.Count >= 5)
                    {
                        obj.GetComponent<TextMeshProUGUI>().text = itemList[5];
                    }
                    if (obj == itemVisual[6] && itemList.Count >= 6)
                    {
                        obj.GetComponent<TextMeshProUGUI>().text = itemList[6];
                    }
                    if (obj == itemVisual[7] && itemList.Count >= 7)
                    {
                        obj.GetComponent<TextMeshProUGUI>().text = itemList[7];
                    }
                    if (obj == itemVisual[8] && itemList.Count >= 8)
                    {
                        obj.GetComponent<TextMeshProUGUI>().text = itemList[8];
                    }
                    if (obj == itemVisual[9] && itemList.Count >= 9)
                    {
                        obj.GetComponent<TextMeshProUGUI>().text = itemList[9];
                    }

                }

            }
            if (Input.GetKeyDown(KeyCode.J) && inventoryOn && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Battle"))
            {
                inventoryImage.SetActive(false);
                inventoryPicker.SetActive(false);

                inventoryOn = false;
                inventoryPickerOn = false;
                playerMove.canMove = true;
            }
        
    }
}
