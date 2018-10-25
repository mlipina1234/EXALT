﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{


    [SerializeField] private Button _resume;
    [SerializeField] private Button _options;
    [SerializeField] private Button _screenshot;
    [SerializeField] private Button _mainmenu;
    [SerializeField] private Button _quit;
    [SerializeField] private GameObject _menuPanel;



    private bool _isPaused = false;
    private Player _controller;


    // Use this for initialization
    void Start ()
    {
        _controller = ReInput.players.GetPlayer(0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_controller.GetButtonDown("Pause"))
        {
            _isPaused = !_isPaused;
            if (_isPaused)
            {
                Time.timeScale = 0;
                _menuPanel.SetActive(true);
                EventSystem.current.SetSelectedGameObject(_resume.gameObject, new BaseEventData(EventSystem.current));
                _resume.Select();
                
            }
            else
            {
                Time.timeScale = 1;
                _menuPanel.SetActive(false);
            }
        }
    }


    //==============================| Button Press Methods

    public void ResumePressed()
    {
        Time.timeScale = 1;
        _menuPanel.SetActive(false);
    }

    public void OptionsPressed()
    {
        //do something
    }

    public void QuitPressed()
    {
        //quit
    }

    public void ScreenshotPressed()
    {
        //take screenshot
    }

    public void MainMenuPressed()
    {
        //go to main menu
    }
}