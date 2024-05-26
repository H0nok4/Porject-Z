using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class OpenDataToolShortCut
{
    private const string Path = "/DataManager";

    [MenuItem("Tools/打开配置目录 %#e")] // 使用Ctrl + Shift + E作为快捷键
    static void OpenFolder() {
        string projectPath = Application.dataPath.Replace("/Assets", ""); // 获取项目根目录
        string folderPath = projectPath + Path; // 替换为您想要打开的文件夹名称
        folderPath = folderPath.Replace("/", "\\");
        System.Diagnostics.Process.Start("explorer.exe", folderPath);
    }
}
