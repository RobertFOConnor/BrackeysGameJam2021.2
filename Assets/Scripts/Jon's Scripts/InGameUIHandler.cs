using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIHandler : MonoBehaviour
{
    static InGameUIHandler instance;


    [SerializeField]
    GameObject[] pauseMenuLayers;

    [SerializeField]
    GameObject pauseTab;


    private bool paused = false;
    // Start is called before the first frame update

    public static InGameUIHandler GetInstance()
    {
        if (!instance)
        {
            instance = GameObject.FindObjectOfType<InGameUIHandler>();
        }
        return instance;
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnGamePause()
    {
        if (paused)
        {
            Time.timeScale = 1;
            ResetPauseMenu();
        }

        else
        {
            Time.timeScale = 0;
            pauseTab.SetActive(true);

        }

        paused = !paused;
    }

    public void OnLayerToggle(int layerIndex)
    {
        for (int i = 0; i < pauseMenuLayers.Length; i++)
        {
            if (i == layerIndex)
            {
                pauseMenuLayers[i].SetActive(true);
            }
            else
            {
                pauseMenuLayers[i].SetActive(false);
            }
        }
    }


    public void ResetPauseMenu()
    {
        OnLayerToggle(0);
        pauseTab.SetActive(false);
    }
}
