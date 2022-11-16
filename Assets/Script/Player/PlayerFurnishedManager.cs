using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFurnishedManager : MonoBehaviour
{
    private Animator playerAni;

    public AudioClip[] playerFurnishedAudioClip;

    private bool onceFurnishedAttackAudioControl;

    private bool twiceFurnishedAttackAudioControl;

    private bool furnishedIsChecked = false;

    private int clickFurnishedCount = 0;

    private float furnishedTimeCounter = 0f;

    private float furnishedClickTimeCounter = 0f;

    private float furnishedDamage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        playerAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerAttackManager.isHoldingGun==false)
        {
            PlayerFurnishedManage();
            PlayerFurnishedAudioController();
        }
    }
    private void PlayerFurnishedManage()
    {
        furnishedTimeCounter = Time.time;
        onceFurnishedAttackAudioControl = false;
        twiceFurnishedAttackAudioControl = false;
        if (furnishedTimeCounter - furnishedClickTimeCounter >= 0.8f)
        {
            furnishedIsChecked = true;
        }
        else
        {
            furnishedIsChecked = false;
        }
        if (Input.GetMouseButtonDown(0) && furnishedIsChecked)
        {
            clickFurnishedCount = Random.Range(1, 3);
            furnishedClickTimeCounter = Time.time;
        }
        if (clickFurnishedCount == 1)
        {
            playerAni.SetInteger("FurnishedAttackCount", clickFurnishedCount);
            onceFurnishedAttackAudioControl = true;
            clickFurnishedCount = 0;
        }
        else if (clickFurnishedCount == 2)
        {
            playerAni.SetInteger("FurnishedAttackCount", clickFurnishedCount);
            twiceFurnishedAttackAudioControl = true;
            clickFurnishedCount = 0;
        }
        else
        {
            clickFurnishedCount = 0;
            playerAni.SetInteger("FurnishedAttackCount", clickFurnishedCount);
        }
    }
    private void PlayerFurnishedAudioController()
    {
        if (onceFurnishedAttackAudioControl)
        {
            AudioSource.PlayClipAtPoint(playerFurnishedAudioClip[0], transform.position);
        }
        if (twiceFurnishedAttackAudioControl)
        {
            AudioSource.PlayClipAtPoint(playerFurnishedAudioClip[1], transform.position);
        }
    }
}
