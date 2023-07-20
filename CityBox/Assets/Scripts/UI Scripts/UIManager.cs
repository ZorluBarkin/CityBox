/*  
 * Copyright June 2022 Barkın Zorlu 
 * All rights reserved. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Managers
    public ObjectManager objectManager = null;

    public BuildingManager buildingManager = null;
    public RoadManager roadManager = null;
    #endregion

    public GameObject menuPanel = null;
    public GameObject menuButton = null;

    [Header("Object Types")]
    public BuildingType buildingType = BuildingType.None;
    public VehicleRoadType vehicleRoadType = VehicleRoadType.None;
    public PedestrianRoadType pedestrianRoadType = PedestrianRoadType.None;

    private void Awake()
    {
        if(menuPanel == null)
            menuPanel = transform.GetChild(0).gameObject;
        
        if(menuButton == null)
            menuButton = transform.GetChild(1).gameObject;
    }

    /// <summary>
    /// Opens or closes menu panel
    /// </summary>
    public void OnMenuButtonClick()
    {
        if (menuPanel.activeInHierarchy)
        {
            menuPanel.SetActive(false);
            menuButton.transform.GetChild(0).GetComponent<Text>().text = "^";
            objectManager.selectedObject = null;
            objectManager.mode = ObjectMode.None;
        }
        else
        {
            menuPanel.SetActive(true);
            menuButton.transform.GetChild(0).GetComponent<Text>().text = "v";
        }      
    }

    /// <summary>
    /// Sets the object placer mode to Remove (demolish roads, buildings, etc.)
    /// </summary>
    public void OnDemolishButtonClick()
    {
        objectManager.mode = ObjectMode.Remove;
    }

    /// <summary>
    /// Selects building type for ui buttons, int parameter as enum parameters are not supported.
    /// </summary>
    /// <param name="selection"></param>
    public void OnBuildingButtonClick(int selection)
    {
        switch (selection)
        {
            case 0: buildingType = BuildingType.None; objectManager.selectedObject = null; break;
            case 1: 
                buildingType = BuildingType.Residental; 
                objectManager.selectedObject = buildingManager.residentalBase; 
                break;
            case 2: buildingType = BuildingType.Commercial; objectManager.selectedObject = buildingManager.commercialBase; break;
            case 3: buildingType = BuildingType.Unique; objectManager.selectedObject = buildingManager.uniqueBase; break;
        }
        buildingManager.buildingType = buildingType;
        objectManager.mode = ObjectMode.Add;
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
        roadManager.vehicleRoadType = vehicleRoadType;
        objectManager.mode = ObjectMode.Add;
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
        roadManager.pedestrianRoadType = pedestrianRoadType;
        objectManager.mode = ObjectMode.Add;
    }

}
