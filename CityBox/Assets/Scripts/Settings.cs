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
using UnityEngine.Rendering.Universal;

public class Settings : MonoBehaviour
{
    #region Camera Settings
    // Camera zoom settings
    public float maxHFOV = 90f; // for UI, static override
    public static float _MaxHFOV = 90f;
    public float minHFOV = 20f; // for UI, static override
    public static float _MinHFOV = 20f;

    public Camera cam = null;
    private UniversalAdditionalCameraData cameraData = null;
    AntialiasingMode antialiasingMode = AntialiasingMode.None;
    public bool doPostProcessing = false;
    
    public bool apply = false; // Debug, change this to an event when UI is implemnented

    public int maxFrameRate = 60;
    
    public bool isVsyncOn = false; // for ui 
    private bool vsyncOn = false;

    public enum VsyncSetting
    {
        Off,
        One,
        Two,
        Three,
        Four
    }
    public VsyncSetting vsyncSetting = VsyncSetting.Off;
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        if (cam == null)
            cam = Camera.main;

        if(cameraData == null)
            cameraData = cam.GetComponent<UniversalAdditionalCameraData>();

        ApplySettings();
    }

    // Update is called once per frame
    void Update()
    {
        if (apply)
        {
            apply = false;

            ApplySettings();
        }
    }

    /// <summary>
    /// Applies settings game settings.
    /// </summary>
    private void ApplySettings()
    {
        // Render settings
        if (vsyncSetting != VsyncSetting.Off && isVsyncOn)
            vsyncOn = true;

        if (vsyncOn)
            QualitySettings.vSyncCount = (int)vsyncSetting;
        else
            Application.targetFrameRate = maxFrameRate + 1;

        // Camera Settings
        cameraData.antialiasing = antialiasingMode;
        cameraData.renderPostProcessing = doPostProcessing;

    }
}
