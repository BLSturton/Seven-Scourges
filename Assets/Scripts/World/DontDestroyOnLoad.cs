using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] public bool isBattle;
    [SerializeField] public GameObject[] childMove;
    public static DontDestroyOnLoad Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Battle"))
            {
                foreach (GameObject obj in childMove)
                {
                    GameObject newParentGameObject = GameObject.FindGameObjectWithTag("BattleUI");
                    obj.transform.SetParent(newParentGameObject.transform, true);
                    Debug.Log("Moved");
                }
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void sceneMove() 
    {
    }
}
