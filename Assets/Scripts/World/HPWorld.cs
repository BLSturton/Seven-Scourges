using UnityEngine;
using UnityEngine.UI;

public class HPWorld : MonoBehaviour
{
    [SerializeField] public int HP;
    [SerializeField] public int MaxHP;
    [SerializeField] public int SP;
    [SerializeField] public int MaxSP;

    [SerializeField] public GameObject HPCounter;
    [SerializeField] public GameObject SPCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HPCounter.GetComponent<Text>().text = HP.ToString();
        SPCounter.GetComponent<Text>().text = SP.ToString();

    }
}
