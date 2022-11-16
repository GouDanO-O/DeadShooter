using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public AudioClip[] playerHealthAudioClip;

    public TextMeshProUGUI playerHp;

    public static float playerCurrentHealth;
    


    private float playerMaxHealth=100;

    private bool playerIsDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealthManage();
        Debug.Log(playerCurrentHealth);
    }
    private void PlayerHealthManage()
    {        
        playerHp.GetComponent<TextMeshProUGUI>().text =""+playerCurrentHealth;
        PlayerIsDeath();
    }
    private void PlayerIsDeath()
    {
        if(playerCurrentHealth==0)
        {
            playerIsDeath = true; 
        }
    }
}
