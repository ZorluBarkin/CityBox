/*  
 * Copyright 2023 Barkın Zorlu 
 * All rights reserved.
 * 
 * This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
 * To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/ or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject buildingMenu = null;
    public GameObject roadMenu = null;

    public GameObject[] menuArray; // populate in scene

    private void Awake()
    {
        if(menuArray.Length > 0)
            for(int i = 0; i < menuArray.Length; i++)
                menuArray[i].SetActive(false);
    }

    /// <summary>
    /// Used for opening the building menu when Buildings Button is pressed.
    /// </summary>
    public void OnBuildingsButtonClick()
    {
        for (int i = 0; i < menuArray.Length; i++)
            menuArray[i].SetActive(false);

        buildingMenu.SetActive(true);
    }

    /// <summary>
    /// Used for opening the road menu when Roads Button is pressed.
    /// </summary>
    public void OnRoadsButtonClick()
    {
        for (int i = 0; i < menuArray.Length; i++)
            menuArray[i].SetActive(false);

        roadMenu.SetActive(true);
    }
}
