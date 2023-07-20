/*  
 * Copyright June 2022 Barkın Zorlu 
 * All rights reserved. 
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
