using UnityEngine;

public class HellfallFangs : MonoBehaviour
{
    [SerializeField] public GameObject[] swords;
    [SerializeField] public GameObject goal;
    [SerializeField] public float gravityScale;
    [SerializeField] public GameObject spawn1;
    [SerializeField] public GameObject spawn2;
    [SerializeField] public OptionPicker cetusOptionPicker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartAttack() 
    {
        foreach (GameObject obj in swords) 
        {
            if(obj == swords[0]) 
            {
                obj.transform.position = spawn1.transform.position;
            }
            if (obj == swords[1])
            {
                obj.transform.position = spawn2.transform.position;
            }
            obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
}
