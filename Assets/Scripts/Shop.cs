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
        Debug.Log("����StandardTurret");
        BuildManager.Instance.SelectedTurret = standardTurret;
    }

    public void OnPurseMissileLauncher()
    {
        Debug.Log("����MissileLauncher");
        BuildManager.Instance.SelectedTurret = missileLauncher;
    }

    public void OnPurseLaserBeamer()
    {
        Debug.Log("����LaserBeamer");
        BuildManager.Instance.SelectedTurret = LaserBeamer;
    }
}
