using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject uiScreen;
    public GameObject player;
    public GameObject gameManager;
    public GameObject audioManager;
    public GameObject battleManager;

    private void Awake() 
    {
        if (UIFade.instance == null)
        {
            Instantiate(uiScreen);
        }

        if (PlayerController.instance == null)
        {
            Instantiate(player);
        }

        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        if (AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }

        if (BattleManager.instance == null)
        {
            Instantiate(battleManager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
