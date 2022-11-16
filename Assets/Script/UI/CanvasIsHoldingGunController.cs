using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasIsHoldingGunController : MonoBehaviour
{
    private Animator gunCanvasAni;

    private void Start()
    {
        gunCanvasAni = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(PlayerAttackManager.isHoldingGun)
        {
            gunCanvasAni.SetBool("IsHoldingGun", true);
        }
        else
        {
            gunCanvasAni.SetBool("IsHoldingGun", false);
        }
    }
}
