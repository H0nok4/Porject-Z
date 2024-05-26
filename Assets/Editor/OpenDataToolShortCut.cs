using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class OpenDataToolShortCut
{
    private const string Path = "/DataManager";

    [MenuItem("Tools/������Ŀ¼ %#e")] // ʹ��Ctrl + Shift + E��Ϊ��ݼ�
    static void OpenFolder() {
        string projectPath = Application.dataPath.Replace("/Assets", ""); // ��ȡ��Ŀ��Ŀ¼
        string folderPath = projectPath + Path; // �滻Ϊ����Ҫ�򿪵��ļ�������
        folderPath = folderPath.Replace("/", "\\");
        System.Diagnostics.Process.Start("explorer.exe", folderPath);
    }
}
