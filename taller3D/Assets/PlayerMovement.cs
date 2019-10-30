﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour{
   	public Rigidbody rb;
   	public float fowardForce = 2000f;
    public float sidewaysForce = 500f;
    void FixedUpdate(){
        rb.AddForce(0,0,fowardForce*Time.deltaTime);
        
        if(Input.GetKey("d")){
            rb.AddForce(sidewaysForce*Time.deltaTime,0,0,ForceMode.VelocityChange);
        }
        if(Input.GetKey("a")){
            rb.AddForce(-sidewaysForce*Time.deltaTime,0,0,ForceMode.VelocityChange);
        }
    }
}