using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretDesign standardTurret;
    public TurretDesign missileLauncher;
    public TurretDesign LaserBeamer;
    public void OnPurseStandardTurret()
    {
        Debug.Log("¹ºÂòStandardTurret");
        BuildManager.Instance.SelectedTurret = standardTurret;
    }

    public void OnPurseMissileLauncher()
    {
        Debug.Log("¹ºÂòMissileLauncher");
        BuildManager.Instance.SelectedTurret = missileLauncher;
    }

    public void OnPurseLaserBeamer()
    {
        Debug.Log("¹ºÂòLaserBeamer");
        BuildManager.Instance.SelectedTurret = LaserBeamer;
    }
}
