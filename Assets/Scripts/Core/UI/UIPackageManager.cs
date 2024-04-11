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
    /// 自己包装一层,方便后面做包引用之类的
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
