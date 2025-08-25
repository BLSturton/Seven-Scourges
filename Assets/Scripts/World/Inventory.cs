using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Inventory : MonoBehaviour
{
    [SerializeField] public List<string> itemList;
    [SerializeField] public List<GameObject> itemVisual;


    [SerializeField] public bool inventoryOn;
    [SerializeField] public bool canInventory;
    [SerializeField] GameObject inventoryImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryOn = false;
        inventoryImage.SetActive(false);
    }
    private void Awake()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.J) && !inventoryOn && canInventory && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Battle")) 
        {
            OpenInventory();
         
        }
        if (Input.GetKeyDown(KeyCode.J) && inventoryOn && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Battle")) 
        {
            inventoryImage.SetActive(false);
            inventoryOn = false;
        }
    }
    public void OpenInventory() 
    {
        inventoryImage.SetActive(true);

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
            StartCoroutine(openWait());
        }

    }
    public IEnumerator openWait()
    {
        yield return new WaitForSeconds(.2f);
        inventoryOn = true;

    }
}
