using System.Collections;
using UnityEngine;

public class OptionPicker : MonoBehaviour
{
    [SerializeField] public GameObject[] attackOptions;
    //0 is attack, 1 is style, etc.
    [SerializeField] public GameObject selectedOption;
    [SerializeField] public bool selectCooldown;
    [SerializeField] public int optionNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedOption = attackOptions[0];
        optionNumber = 0;
        selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);

    }

    // Update is called once per frame
    void Update()
    {
        //Advance Option
        if (Input.GetKeyDown(KeyCode.D) && !selectCooldown) 
        {
            if(optionNumber == 3) 
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
        if (Input.GetKeyDown(KeyCode.A) && !selectCooldown)
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
