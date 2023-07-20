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

public enum BuildingType
{
    None,
    Residental,
    Commercial,
    Unique
}

public class BuildingManager : MonoBehaviour
{
    public int residenceCount = 0;
    public int commercialCount = 0;
    public int uniqueCount = 0;

    public BuildingType buildingType = BuildingType.None;

    public GameObject residentalBase = null;
    public GameObject residentalUpper = null;
    public int residencePopulationLimit = 4;

    public GameObject commercialBase = null;
    public GameObject commercialUpper = null;

    public GameObject uniqueBase = null;

    //public string currentBuildingType = ""; // debug

    //private void Update()
    //{
    //    currentBuildingType = buildingType.ToString();
    //}

}
