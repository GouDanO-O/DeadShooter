using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    private Animator playerAni;

    public static bool isHoldingGun=false;

    public AudioClip[] changeGunsAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeGun();
    }

    private void ChangeGun()
    {
        playerAni.SetBool("IsHoldingGun", isHoldingGun); 
        if(Input.GetButtonUp("ChangeWeapon"))
        {
            isHoldingGun = !isHoldingGun; 
        }
        if(Input.GetButtonUp("ChangeWeapon") &&isHoldingGun)
        {
            AudioSource.PlayClipAtPoint(changeGunsAudio[0],transform.position);
        }
        else if(Input.GetButtonUp("ChangeWeapon")&&!isHoldingGun)
        {
            AudioSource.PlayClipAtPoint(changeGunsAudio[1], transform.position);
        }
    }
}
