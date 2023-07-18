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

public class UIManager : MonoBehaviour
{
    public BuildingType buildingType = BuildingType.None;
    public VehicleRoadType vehicleRoadType = VehicleRoadType.None;
    public PedestrianRoadType pedestrianRoadType = PedestrianRoadType.None;

    /// <summary>
    /// Selects building type for ui buttons, int parameter as enum parameters are not supported.
    /// </summary>
    /// <param name="selection"></param>
    public void OnBuildingButtonClick(int selection)
    {
        switch (selection)
        {
            case 0: buildingType = BuildingType.None; break;
            case 1: buildingType = BuildingType.Residental; break;
            case 2: buildingType = BuildingType.Commercial; break;
            case 3: buildingType = BuildingType.Unique; break;
        }
        BuildingManager.buildingType = buildingType;

    }

    /// <summary>
    /// Selects vehicle road type.
    /// </summary>
    /// <param name="selection"></param>
    public void OnVehicleRoadButtonClick(int selection)
    {
        switch (selection)
        {
            case 0: vehicleRoadType = VehicleRoadType.None; break;
            case 1: vehicleRoadType = VehicleRoadType.None; break;
            case 2: vehicleRoadType = VehicleRoadType.None; break;
            case 3: vehicleRoadType = VehicleRoadType.None; break;
            case 4: vehicleRoadType = VehicleRoadType.None; break;
            case 5: vehicleRoadType = VehicleRoadType.None; break;
        }
        RoadManager.vehicleRoadType = vehicleRoadType;

    }

    /// <summary>
    /// Selection for pedestrian type.
    /// </summary>
    /// <param name="selection"></param>
    public void OnPedestrianRoadButtonClick(int selection)
    {
        switch (selection)
        {
            case 0: pedestrianRoadType = PedestrianRoadType.None; break;
            case 1: pedestrianRoadType = PedestrianRoadType.Small; break;
            case 2: pedestrianRoadType = PedestrianRoadType.Medium; break;
            case 3: pedestrianRoadType = PedestrianRoadType.Large; break;
        }
        RoadManager.pedestrianRoadType = pedestrianRoadType;
    }

}
