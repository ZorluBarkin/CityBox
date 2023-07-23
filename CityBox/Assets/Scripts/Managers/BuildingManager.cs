/*  
 * Copyright June 2022 Barkın Zorlu 
 * All rights reserved. 
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

    [Header("Building GameObjects")]
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
