using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using Main;
using UnityEngine;

public static class UIPackageManager
{
    public const string PackagePath = "FGUI/";

    private static string GetPackagePathByPackageName(string packageName)
    {
        return $"{packageName}/{packageName}";
    }
    /// <summary>
    /// �Լ���װһ��,���������������֮���
    /// </summary>
    /// <param name="name"></param>
    public static void AddPackage(string name)
    {
        UIPackage.AddPackage(PackagePath + GetPackagePathByPackageName(name));
    }

    public static void RemovePackage(string name)
    {
        UIPackage.RemovePackage(PackagePath + GetPackagePathByPackageName(name));
    }

    public static void FGUIBindAll()
    {
        MainBinder.BindAll();
    }
}
