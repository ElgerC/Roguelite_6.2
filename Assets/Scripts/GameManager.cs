using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int[] maxRunes;
    public int curRunes = 0;

    [SerializeField] private int levelIndex;

    [SerializeField] private Vector3[] positions;

    [SerializeField] private List<string> levels = new List<string>();

    public static GameManager instance;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetPos;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetPos;
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.FindWithTag("Canvas"));
    }

    private void SetPos(Scene scene, LoadSceneMode mode)
    {
        GameObject.Find("Player").transform.position = positions[levelIndex];
    }

    public void AddRune()
    {
        DontDestroyOnLoad(GameObject.Find("Player"));

        curRunes++;
        if(levelIndex >= levels.Count)
        {
            Application.Quit();
        }

        if(curRunes >= maxRunes[levelIndex])
        {
            SceneManager.LoadScene(levels[levelIndex]);
            levelIndex++;
        }
    }
}