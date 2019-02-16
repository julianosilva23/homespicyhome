﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    Button target;
    public GameObject targetChar;
    public GameObject selectedChar;
    bool onButton;
    bool onChar;

    public MenuNew menuManager;
    public string typeInput;
    public int playerNumber;
    public Color playerColor;

    void Start(){
        onButton = false;
        target = null;
        targetChar = null;
        selectedChar = null;
    }

    void FixedUpdate(){
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
            280 * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal" + typeInput),
            280 * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical" + typeInput)
        );
        if ((Input.GetButtonDown("Submit") || Input.GetButtonDown("Fire1" + typeInput)) && onButton){
            target.onClick.Invoke();
        }
        if (Input.GetButtonDown("Fire1" + typeInput) && onChar){
            if (!targetChar.GetComponent<CharButton>().isSelected){
                if (selectedChar != null){
                    selectedChar.GetComponent<Image>().color = new Color (0,0,0);
                    selectedChar.GetComponent<CharButton>().isSelected = false;
                    selectedChar.GetComponent<Button>().interactable = true;
                    Debug.Log("pt2");
                } else {
                    menuManager.playersReady ++;
                    Debug.Log("pt3");
                }
                selectedChar = targetChar;
                selectedChar.GetComponent<Image>().color = Color.red;
                selectedChar.GetComponent<Button>().interactable = true;
                selectedChar.GetComponent<CharButton>().isSelected = true;
                Keyboard.NumberChar[playerNumber] = targetChar.GetComponent<CharButton>().charNumber;
                Debug.Log(selectedChar.GetComponent<Image>().color.ToString());
                Debug.Log(playerColor.ToString());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Button"){
            target = col.GetComponent<Button>();
            target.Select();
            onButton = true;
        }
        if (col.tag == "CharButton"){
            targetChar = col.gameObject;
            //targetChar.GetComponent<Button>().Select();
            onChar = true;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.tag == "Button"){
            target = null;
            onButton = false;
        }
        if (col.tag == "CharButton"){
            targetChar = null;
            onChar = false;
        }
    }

    void ActivateButton(){
        if (Input.GetButtonDown("Submit")){
            target.onClick.Invoke();
        }
    }
}