﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim 
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType{
    SINGLE,
    MULTIPE
}

public enum WeaponBulletType{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    private Animator anim;

    public WeaponAim weapon_Aim;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shoot_Sound, reload_Sound;

    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public GameObject attack_Point;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ShootAnimation(){
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Aim(bool canAim){
        anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    void Turn_On_MuzzleFlash(){
        muzzleFlash.SetActive(true);
    }

    void Turn_Off_MuzzleFlash(){
        muzzleFlash.SetActive(false);
    }

    void Play_ShootSound() {

        shoot_Sound.Play();
    }

    void Play_ReloadSound() {

        reload_Sound.Play();
    }

    void Turn_On_AttackPoint() {
        attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint() {
        attack_Point.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
