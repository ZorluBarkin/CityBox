using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelector : MonoBehaviour
{
    // buttons, assign in editor
    [SerializeField] private Button residental;
    [SerializeField] private Button commercial;
    [SerializeField] private Button road;

    // setting up the buttons
    private void Start()
    {

        residental.onClick.AddListener(SelectResidental);
        commercial.onClick.AddListener(SelectCommercial);
        road.onClick.AddListener(SelectRoad);
        
    }

    private void SelectResidental()
    {
        ObjectPlacer.useResidental = true;
        ObjectPlacer.useCommercial = false;
        ObjectPlacer.useRoad = false;
    }

    private void SelectCommercial()
    {
        ObjectPlacer.useResidental = false;
        ObjectPlacer.useCommercial = true;
        ObjectPlacer.useRoad = false;
    }

    private void SelectRoad()
    {
        ObjectPlacer.useResidental = false;
        ObjectPlacer.useCommercial = false;
        ObjectPlacer.useRoad = true;
    }

}
