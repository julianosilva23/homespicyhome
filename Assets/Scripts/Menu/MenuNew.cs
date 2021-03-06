﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNew : MonoBehaviour
{
    public GameObject[] cursors;
    public Button start;
    public Button goToMap;

    public GameObject[] levelSelectScreens;

    public int playersReady;
    bool ready;
    

    void Start()
    {
        start.Select();
        ready = false;
        playersReady = 0;
    }

    void FixedUpdate(){
        if (playersReady == Keyboard.CountPlayer){
            goToMap.interactable = true;
        }
    }

    public void Key2(){
        string[] inputs = {"Key1", "Key2"};
		Keyboard.TypeInput = inputs;	
		Keyboard.CountPlayer = 2;
		Keyboard.Control = "keyboard";
        CharSelectScreen();
    }

    public void Joy2(){
        string[] inputs = {"Joy1", "Joy2"};
		Keyboard.TypeInput = inputs;	
		Keyboard.CountPlayer = 2;
		Keyboard.Control = "joystick";
        CharSelectScreen();
    }

    public void Joy4(){
        string[] inputs = {"Joy1", "Joy2", "Joy3", "Joy4"};
		Keyboard.TypeInput = inputs;	
		Keyboard.CountPlayer = 4;
		Keyboard.Control = "joystick";
        CharSelectScreen();
    }

    public void KeyJoy(){
        string[] inputs = {"Key1", "Key2", "Joy1", "Joy2"};
		Keyboard.TypeInput = inputs;	
		Keyboard.CountPlayer = 4;
		Keyboard.Control = "mixed";
        CharSelectScreen();
    }

    public void CharSelectScreen(){
        for (int i = 0; i < Keyboard.CountPlayer; i ++){
            Debug.Log(cursors[i]);
            cursors[i].SetActive(true);
            cursors[i].GetComponent<Cursor>().typeInput = Keyboard.TypeInput[i];
        }
    }

    public void NoCursor(string action){
        if (action == "back"){
            for (int i = 0; i < Keyboard.CountPlayer; i ++){
                cursors[i].GetComponent<Cursor>().UnselectChar();
                playersReady = 0;
                //Keyboard.CountPlayer = 0;
            }
        }
        for (int i = 0; i < Keyboard.CountPlayer; i ++){
            cursors[i].SetActive(false);
        }
    }

    public void SetGameMode(int mode){
        Keyboard.gamemode = mode;
    }

    public void LevelSelectScreen(){
        levelSelectScreens[Keyboard.gamemode].SetActive(true);
    }

    public void SelectButton(){
        GameObject[] allFirstButtons = GameObject.FindGameObjectsWithTag("FirstButton");
        foreach (GameObject buttonObject in allFirstButtons){
            if (buttonObject.activeSelf){
                buttonObject.GetComponent<Button>().Select();
                return;
            }
        }
    }


//--------------------------------------------------------------------------------------------------------------------//
//================================================LEVEL SELLECT=======================================================//
//--------------------------------------------------------------------------------------------------------------------//

    public void GoToLevel(string levelName){
        SceneManager.LoadScene(levelName);
    }

    void TournamentStart(string[] torunamentLevels){
        Keyboard.currentRound = 0;
        Keyboard.levelsList = torunamentLevels;
        Keyboard.playersWins = new int[] {0, 0, 0, 0};
        GoToLevel(Keyboard.levelsList[0]);
    }

    public void HillTournament(){
        string[] torunamentLevels = {"Hill1", "Hill2", "Hill3"};
        TournamentStart(torunamentLevels);
    }

    public void SandTournament(){
        string[] torunamentLevels = {"Sand1", "Sand2", "Sand3"};
        TournamentStart(torunamentLevels);
    }

    public void ForestTournament(){
       string[] torunamentLevels = {"Forest1", "Forest2", "Forest3"};
        TournamentStart(torunamentLevels);
    }

    public void Main_Exit(){
		Application.Quit();
	}
}
