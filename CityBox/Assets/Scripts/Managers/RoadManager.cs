/*  
 * Copyright 2023 Barkın Zorlu 
 * All rights reserved.
 * 
 * This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
 * To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/ or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.
 */

using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public enum VehicleRoadType
{
    None,
    TwoWay,
    OneWay,
    Asymetric,
    Highway,
    County
}

public class VehicleRoad
{
    public VehicleRoadType type;
    public int[] laneSize;

    private bool emergencyLane;
    private bool bikeLane;
    private bool bussLane;
    private bool lights;
    private bool trafficLights;
    private bool tram;
    private bool sideWalk;
    private bool wooded;
    
    public VehicleRoad(VehicleRoadType type)
    {
        switch (type)
        {
            case VehicleRoadType.TwoWay:
                {
                    laneSize = new int[3];
                    laneSize[0] = 2;
                    laneSize[1] = 4; // 2 towards 2 outwards
                    laneSize[2] = 6;

                    emergencyLane = true;
                    bikeLane = false;
                    bussLane = false;
                    lights = true;
                    trafficLights = false;
                    tram = false;
                    sideWalk = true;
                    if(sideWalk)
                        wooded = false;
                    
                    break;
                }
            case VehicleRoadType.OneWay:
                {
                    laneSize = new int[4];
                    laneSize[0] = 1;
                    laneSize[1] = 2; // 2 towards
                    laneSize[2] = 3;
                    laneSize[2] = 4;

                    emergencyLane = true;
                    bikeLane = false;
                    bussLane = false;
                    lights = true;
                    trafficLights = false;
                    tram = false;
                    sideWalk = true;
                    if (sideWalk)
                        wooded = false;
                    
                    break;
                }
            case VehicleRoadType.Asymetric:
                {
                    laneSize = new int[2];
                    laneSize[0] = 3; // 2 towards 1 outwards
                    laneSize[1] = 5; // 3 towards 2 outwards

                    emergencyLane = true;
                    bikeLane = false;
                    bussLane = false;
                    lights = true;
                    trafficLights = false;
                    tram = false;
                    sideWalk = true;
                    if (sideWalk) 
                        wooded = false;
                    
                    break;
                }
            case VehicleRoadType.Highway:
                {
                    laneSize = new int[3];
                    laneSize[0] = 3;
                    laneSize[1] = 4; // 2 towards 2 outwards
                    laneSize[2] = 5;

                    emergencyLane = true;
                    bikeLane = false;
                    bussLane = false;
                    lights = true;
                    trafficLights = false;
                    tram = false;
                    sideWalk = false;
                    if (sideWalk) 
                        wooded = false;
                    
                    break;
                }
            case VehicleRoadType.County:
                {
                    laneSize = new int[1];
                    laneSize[0] = 2;

                    emergencyLane = false; 
                    bikeLane = true; // may be on but hidden on model
                    bussLane = false;
                    lights = false;
                    trafficLights = false;
                    tram = false; // always false
                    sideWalk = true; // may be on but hidden on model
                    wooded = false; // unlike others, this is always false
                    break;
                }
        }
    }

}

public enum PedestrianRoadType
{
    None,
    Small, // 2 lane road thick
    Medium, // 4 lane road thich
    Large // 6 lane road thich
}

public class PedestrianRoad
{
    PedestrianRoadType type;

    public bool[] size = new bool[3];

    public bool tram;
    // only pedestrian road, bike and people always allowed.

    // heavy vehicles will be able to enter
    // at specified times of the day

    // emergencyLane is always on
    private bool emergencyLane;

    public PedestrianRoad(PedestrianRoadType type)
    {
        int sizeValue = (int)type;
        if (sizeValue > 0)
        {
            size[0] = false;
            size[1] = false;
            size[2] = false;

            switch (sizeValue)
            {
                case 1: size[0] = true; break;
                case 2: size[1] = true; break;
                case 3: size[2] = true; break;
            }
        }

        emergencyLane = true; // always true
        tram = false;
    }
}

public class RoadManager : MonoBehaviour
{
    public static VehicleRoadType vehicleRoadType = VehicleRoadType.None;
    public string currentVehicleRoadType = ""; // debug
    public static PedestrianRoadType pedestrianRoadType = PedestrianRoadType.None;
    public string currentPedestrianRoadType = ""; // debug

    private void Update()
    {
        currentVehicleRoadType = vehicleRoadType.ToString();
        currentPedestrianRoadType = pedestrianRoadType.ToString();
    }

}
