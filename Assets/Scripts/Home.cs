﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    bool fire;
    public Player owner;

    public int fireTime;

    // Start is called before the first frame update
    void Start()
    {
       fire = false;
       owner.setHome(owner.getHome() + 1);
    }

     void OnTriggerEnter2D(Collider2D col){
        if (fire){
           if (col.tag == "Home"){
               Debug.Log("vai da merda...");
               if (col.gameObject.GetComponent<Home>().fire == false){
                   Debug.Log("Deu merda!!!");
                   StartCoroutine(SetFire(col.gameObject.GetComponent<Home>()));
               }
           }
        } else {
            if (col.tag == "Boom"){
            StartCoroutine(OnFire(fireTime));
            Debug.Log("Ta pegando fogo bixo!!!");
            }
        }
    } 
    
    IEnumerator OnFire(int timer){
        fire = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(timer);
        owner.setHome(owner.getHome() - 1);
        Destroy(gameObject);
    }

    IEnumerator SetFire(Home target){
        yield return new WaitForSeconds(fireTime/5);
        target.GotFire();
    }

    public void GotFire(){
        StartCoroutine(OnFire(fireTime));
    }

}
