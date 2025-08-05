using NUnit.Framework;
using System.Collections;
using System.Linq;
using UnityEngine;

public class OptionPicker : MonoBehaviour
{
    [SerializeField] public GameObject[] attackOptions;
    //0 is attack, 1 is style, etc.
    [SerializeField] public GameObject selectedOption;
    [SerializeField] public bool selectCooldown;
    [SerializeField] public int optionNumber;


    [SerializeField] public BattleManager battleManager;

    [SerializeField] public bool actionPicked;
    [SerializeField] public bool attackOn;

    //Attack
    [SerializeField] public int enemyNumber;
    [SerializeField] public int maxEnemy;
    [SerializeField] public GameObject enemyPicker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedOption = attackOptions[0];
        optionNumber = 0;
        selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);
        enemyPicker.SetActive(false);
        //Debug
        maxEnemy = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Advance Option
        if (Input.GetKeyDown(KeyCode.D) && !selectCooldown && !actionPicked)
        {
            if (optionNumber == 3)
            {
                selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y - 1);
                selectedOption = attackOptions[0];
                optionNumber = 0;
                selectCooldown = true;
                selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);
                StartCoroutine(AttackWait());
            }
            else
            {
                selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y - 1);
                selectedOption = attackOptions[optionNumber + 1];
                optionNumber++;
                selectCooldown = true;
                selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);

                StartCoroutine(AttackWait());
            }

        }
        //Reverse Option
        if (Input.GetKeyDown(KeyCode.A) && !selectCooldown && !actionPicked)
        {
            if (optionNumber == 0)
            {
                selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y - 1);
                selectedOption = attackOptions[3];
                optionNumber = 3;
                selectCooldown = true;
                StartCoroutine(AttackWait());
                selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);

            }
            else
            {
                selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y - 1);
                selectedOption = attackOptions[optionNumber - 1];
                optionNumber--;
                selectCooldown = true;
                StartCoroutine(AttackWait());
                selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);

            }

        }

        //Confirm Option
        if (Input.GetKeyDown(KeyCode.K))
        {
            switch (optionNumber)
            {
                case 0:
                    attackOn = true;
                    break;
            }
            foreach (GameObject obj in attackOptions)
            {
                if (obj != null)
                {
                    obj.SetActive(false); // Deactivates the GameObject
                }
            }
            actionPicked = true;
            
        }
        if (attackOn)
        {
            enemyPicker.SetActive(true);
            //Advance Option
            if (Input.GetKeyDown(KeyCode.D) && !selectCooldown)
            {
                if (enemyNumber == maxEnemy)
                {

                    selectedOption = battleManager.enemyList[0];
                    enemyNumber = 0;
                    selectCooldown = true;
                    StartCoroutine(AttackWait());
                }
                else
                {
                    selectedOption = battleManager.enemyList[enemyNumber + 1];
                    enemyNumber++;
                    selectCooldown = true;

                    StartCoroutine(AttackWait());
                }

            }
            //Reverse Option
            if (Input.GetKeyDown(KeyCode.A) && !selectCooldown)
            {
                if (enemyNumber == 0)
                {

                    selectedOption = battleManager.enemyList[maxEnemy];
                    enemyNumber = 2;
                    selectCooldown = true;
                    StartCoroutine(AttackWait());
                }
                else
                {
                    selectedOption = battleManager.enemyList[enemyNumber - 1];
                    enemyNumber--;
                    selectCooldown = true;

                    StartCoroutine(AttackWait());
                }

            }

            //Moves selector
            switch (enemyNumber) 
            {
                case 0:
                    enemyPicker.transform.position = new Vector2(4.75f, .2f);
                    break;
                case 1:
                    enemyPicker.transform.position = new Vector2(6f, -1.2f);
                    break;
                case 2:
                    enemyPicker.transform.position = new Vector2(4.5f, -2.5f);
                    break;
            }
        }
    }

    public IEnumerator AttackWait() 
    {
        yield return new WaitForSeconds(.2f);
        selectCooldown = false;
    }
    //Starts a new turn by having attack selected
    public void newTurn() 
    {
        selectedOption = attackOptions[0];
        optionNumber = 0;
        selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);
    }
}
