using Doublsb.Dialog;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    public DialogManager dialogManager;
    public int textNumber;
    public bool inText;
    public DialogData dialog;
    public string textName;
    public GameObject dialogBox;

    public bool FirstScene = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Starts the very first cutscene
        inText = true;
        textNumber = 0;
        StartingScene();
        textName = "Starting Scene";
       FirstScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Advances text number when in a scene
        if (Input.GetKeyDown(KeyCode.K) && inText)
        {
            textNumber = textNumber + 1;
            switch (textName) 
            {
                case "Starting Scene":
                    StartingScene();
                    break;
            }
        }

       
    }
    //Text for the first scene
    public void StartingScene() 
    {
        switch (textNumber)
        {
            case 0:
                dialog = new DialogData("Oh I'm scourging it", "Cetus");
                dialogManager.Show(dialog);
                break;
            case 1:
                dialog = new DialogData("Literally scourging my shit rn", "Cetus");
                dialogManager.Show(dialog);
                break;
            case 2:
                dialogBox.SetActive(false);
                textName = "None";
                inText = false;
                break;
        }
    }
}
