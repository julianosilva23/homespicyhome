﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public int bombCooldown;
    public int blastCooldown;
    public int resource;
    public int home;
    public string typeInput;
    bool alive;
    bool hasBomb;

    public GameObject bomb;
    public GameObject builtHome;

    // Start is called before the first frame update
    void Start()
    {
        setResource(0);

        setHome(0);

        alive = true;

        hasBomb = true;
    }

    void OnTriggerEnter2D(Collider2D col){
        
        if (col.tag == "Resource"){
            
            setResource(resource + 1);

            Destroy(col.gameObject);
        }

        if (col.tag == "Boom"){
            alive = false;
            StartCoroutine(BlastCooldown(blastCooldown));
        }
    }

    IEnumerator BlastCooldown(int timer){
        yield return new WaitForSeconds(timer);
        alive = true;
    }

    IEnumerator BombCooldown(int timer){
        while (timer > 0){
            yield return new WaitForSeconds(1);
            timer --;
        }
        hasBomb = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive){
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2
            (moveSpeed * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal" + typeInput), moveSpeed * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical" + typeInput));
        } else {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        if (hasBomb && alive){
            if (Input.GetButtonDown("Fire1" + typeInput)){
                Debug.Log("Fire1" + typeInput);
                hasBomb = false;
                Instantiate(bomb, transform.position, transform.rotation);
                StartCoroutine(BombCooldown(bombCooldown));
            }
        }
        if (getResource() >= 3){
            if (Input.GetButtonDown("Fire2" + typeInput)){
                StartCoroutine(BuildHome(bombCooldown + 3));
            }
        }
    }

    IEnumerator BuildHome(int buildTime){
        alive = false;
        GameObject building = Instantiate(builtHome,transform.position, transform.rotation);
        Home house = building.GetComponent<Home>();
        house.owner = gameObject.GetComponent<Player>();
        house.setBuildTime(buildTime);
        setResource(getResource() - 3); 
        yield return new WaitForSeconds(buildTime);
        alive = true;
    }

    public int getResource(){
        return resource;
    }

    public int setResource(int value){
        resource = value;

        return resource;
    }

    public int getHome(){

        return home;
    }

    public int setHome(int value){

        home = value;

        return home;
    }
}