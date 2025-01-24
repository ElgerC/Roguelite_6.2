using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int[] maxRunes;
    public int curRunes = 0;

    [SerializeField] private int levelIndex;

    [SerializeField] private List<string> levels = new List<string>();

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }

    public void AddRune()
    {
        if(curRunes == maxRunes[levelIndex])
        {
            SceneManager.LoadScene(levels[levelIndex]);
            levelIndex++;
        }
    }
}