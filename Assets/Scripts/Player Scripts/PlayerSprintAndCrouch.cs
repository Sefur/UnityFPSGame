﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    // Start is called before the first frame update
    //
    private PlayerMovement playerMovement;

    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f; 

    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height = 1f;

    private bool is_Crouching;

    private PlayerFootsteps player_Footsteps;

    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.1f;
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;

    private float walk_Step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.25f;
    private float crouch_Step_Distance = 0.5f;

    private float sprint_Value = 100f;
    public float sprint_Treshold = 10f;

    private PlayerStats player_Stats;


    void Awake(){
        playerMovement = GetComponent<PlayerMovement>();

        look_Root = transform.GetChild(0);

        player_Footsteps = GetComponentInChildren<PlayerFootsteps>();
        player_Stats = GetComponent<PlayerStats>();
    }

    void Start() {

        player_Footsteps.volume_Min = walk_Volume_Min;
        player_Footsteps.volume_Max = walk_Volume_Max;
        player_Footsteps.step_Distance = walk_Step_Distance;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
        
    }

    void Sprint(){
        if(Input.GetKeyDown(KeyCode.LeftControl) && !is_Crouching) {
            // if we have stamina we can sprint
            if(sprint_Value > 0f  ) {

                playerMovement.speed = sprint_Speed;

                player_Footsteps.volume_Min = sprint_Volume;
                player_Footsteps.volume_Max = sprint_Volume;
                player_Footsteps.step_Distance = sprint_Step_Distance;

                
            } 
               
        }

        if(Input.GetKeyUp(KeyCode.LeftControl) && !is_Crouching) {
            playerMovement.speed = move_Speed;
            player_Footsteps.volume_Min = walk_Volume_Min;
            player_Footsteps.volume_Max = walk_Volume_Max;
            player_Footsteps.step_Distance = walk_Step_Distance;
        }

        if(Input.GetKey(KeyCode.LeftControl) && !is_Crouching){
            sprint_Value -= sprint_Treshold * Time.deltaTime;

            if(sprint_Value <=0f) {
                sprint_Value = 0f;
                // reset the speed and sound
                playerMovement.speed = move_Speed;
                player_Footsteps.step_Distance = walk_Step_Distance;
                player_Footsteps.volume_Min = walk_Volume_Min;
                player_Footsteps.volume_Max = walk_Volume_Max;
                
            }

            player_Stats.Display_StaminaStats(sprint_Value);
        } else {
              
                if(sprint_Value != 100f) {
                     sprint_Value +=(sprint_Treshold/2f)*Time.deltaTime;
                     player_Stats.Display_StaminaStats(sprint_Value);
                }
                if(sprint_Value >100f) {
                    sprint_Value = 100f;
                }
        }
    }

    void Crouch(){

        if(Input.GetKeyDown(KeyCode.C)){

            // if we are crouching - stand up
            if(is_Crouching){

                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
                playerMovement.speed = move_Speed;

                player_Footsteps.volume_Min = walk_Volume_Min;
                player_Footsteps.volume_Max = walk_Volume_Max;
                player_Footsteps.step_Distance = walk_Step_Distance;

                is_Crouching = false;

            } else {
                // if we are not crouching -crouch
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                playerMovement.speed = crouch_Speed;

                player_Footsteps.volume_Min = crouch_Volume;
                player_Footsteps.volume_Max = crouch_Volume;
                player_Footsteps.step_Distance = crouch_Step_Distance;
                
                is_Crouching = true;
            }
        }
    }
}
