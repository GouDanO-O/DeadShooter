using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerGunManager : MonoBehaviour
{
    //component
    public GameObject aimPoint;
    
    public ParticleSystem shootFlame;

    public GameObject shootPointHole;

    public GameObject bullet;

    public AudioClip[] playerMusketAudioClip;

    public TextMeshProUGUI gunBulletCountTextMeshPro;

    public GameObject enemyBeantenParticleSystem;

    private Animator playerAni;

    private AudioSource playerAudio;

    private Camera playerCamera;


    //bulletManager
    private int musketBulletMag = 30;    

    private int musketMaxBulletStandbyMag = 120;

    private int musketCurrentBulletCount;

    private float musketReloadTime = 1f;

    private bool musketIsReloading;

    //musketFireManager
    public Transform musketFirePoint;

    public static RaycastHit shootHit;

    private Ray fireRay;

    private int musketFireRange = 100;

    private float musketFireRate=15f;

    private float musketNextTimeToFire = 1f;

    public static float musketBulletDamage = 15f;

    private float bulletSpeed = 50f;

    private float impactForce = 500f;

    public static bool enemyIsBeaten;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAni = GameObject.FindWithTag("Player").GetComponent<Animator>();
        playerAudio = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
        playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        musketCurrentBulletCount = musketBulletMag;   
    }
    private void Update()
    {
        enemyIsBeaten = false;
        if (PlayerAttackManager.isHoldingGun)
        {
            GunManager();
        }
    }
    private void GunManager()
    {
        BulletManager();
        GunShooter();       
    }
    private void GunShooter()
    {
        if (Input.GetButton("Fire")&&Time.time>=musketNextTimeToFire&&!musketIsReloading&&musketCurrentBulletCount!=0)
        { 
            MusketFiring();
            BulletFlying();
        }
        if(Input.GetButtonUp("Fire"))
        {
            playerAni.SetBool("GunAttack", false);
        }
        if(musketCurrentBulletCount==0&& Input.GetButton("Fire") && Time.time >= musketNextTimeToFire && !musketIsReloading||(musketMaxBulletStandbyMag==0&&Input.GetButton("Fire") && Time.time >= musketNextTimeToFire))
        {
            musketNextTimeToFire = Time.time + 2.2f / musketFireRate;
            AudioSource.PlayClipAtPoint(playerMusketAudioClip[5], transform.position);
        }
        gunBulletCountTextMeshPro.GetComponent<TextMeshProUGUI>().text = musketCurrentBulletCount + "/" + musketMaxBulletStandbyMag;
    }
    private void MusketFiring()
    {        

        musketNextTimeToFire = Time.time + 2.2f / musketFireRate;
        musketCurrentBulletCount--;

        shootFlame.Play(); 
        playerAni.SetBool("GunAttack",true);
        AudioSource.PlayClipAtPoint(playerMusketAudioClip[1], transform.position);

        fireRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(fireRay, out shootHit, musketFireRange))
        {
            Debug.Log(shootHit.transform.name);
            if(shootHit.transform.gameObject.layer==7)
            {
                GameObject shootHolePartS = Instantiate(shootPointHole, shootHit.point, Quaternion.LookRotation(shootHit.normal));
                AudioSource.PlayClipAtPoint(playerMusketAudioClip[4], shootHit.point);
                Destroy(shootHolePartS, 1f);
            }   
            if(shootHit.transform.gameObject.layer == 9)
            {
                enemyIsBeaten = true;
                GameObject bulletBeantenBodyPartsy = GameObject.Instantiate(enemyBeantenParticleSystem, PlayerGunManager.shootHit.point,
            Quaternion.LookRotation(PlayerGunManager.shootHit.normal));
                Destroy(bulletBeantenBodyPartsy, 1f);                
            }
            if(shootHit.rigidbody!=null)
            {
                shootHit.rigidbody.AddForce(-impactForce * shootHit.normal);
            }          
        }                
    }
    private void BulletManager()
    {
        if (musketIsReloading)
            return;
        if (Input.GetButtonUp("Reload")&&musketMaxBulletStandbyMag!=0)
        {
                StartCoroutine(Reload());
                return;
         }       
    }
    IEnumerator Reload()
    {
        musketIsReloading = true;
        if(musketCurrentBulletCount>=0&&musketMaxBulletStandbyMag-musketBulletMag>=0)
        {
            musketMaxBulletStandbyMag = musketMaxBulletStandbyMag-musketBulletMag+musketCurrentBulletCount;
            musketCurrentBulletCount = musketBulletMag;
        }
        else if(musketMaxBulletStandbyMag-musketBulletMag<=0&&musketCurrentBulletCount>0)
        {
            if(musketCurrentBulletCount+musketMaxBulletStandbyMag>musketBulletMag)
            {
                musketMaxBulletStandbyMag = musketMaxBulletStandbyMag + musketCurrentBulletCount - musketBulletMag;
                musketCurrentBulletCount = musketBulletMag;
            }
            else
            {
                musketCurrentBulletCount += musketMaxBulletStandbyMag;
                musketMaxBulletStandbyMag = 0;               
            }
        }
        else
        {
            musketCurrentBulletCount = musketMaxBulletStandbyMag;
            musketMaxBulletStandbyMag = 0;
        }
        playerAni.SetBool("GunAttack", false);
        playerAni.SetBool("Reload", true);
        AudioSource.PlayClipAtPoint(playerMusketAudioClip[2], transform.position);
        yield return new WaitForSeconds(musketReloadTime);
        playerAni.SetBool("Reload", false);
        yield return new WaitForSeconds(0.25f);
        
        musketIsReloading = false;        
    } 
    private void BulletFlying()
    {
        GameObject bulletPref = Instantiate(bullet,musketFirePoint);
        float temp_spreadPercent = 50 / playerCamera.fieldOfView;
        Vector3 bulletSpread = Random.insideUnitCircle * temp_spreadPercent;
        bulletPref.transform.eulerAngles += bulletSpread;
        bulletPref.GetComponent<Rigidbody>().AddForce((fireRay.direction+ bulletPref.transform.eulerAngles) * bulletSpeed, ForceMode.Impulse);
    }
}
