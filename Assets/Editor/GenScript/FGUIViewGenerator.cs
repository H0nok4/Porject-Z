using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FGUIViewTemplateGenerator : EditorWindow {
    private string _name;

    private string controllerPath = "Assets\\Scripts\\GameMain\\MVC\\Controller\\";
    private string modelPath = "Assets\\Scripts\\GameMain\\MVC\\Model\\";
    private string controllerRegisterPath = "Assets\\Scripts\\GameMain\\MVC\\Controller\\Base\\ControllerRegister.cs";
    private string modelRegisterPath = "Assets\\Scripts\\GameMain\\MVC\\Model\\Base\\ModelRegister.cs";

    private string controllerName => _name + "Controller";

    private string modelName => _name + "Model";

    [MenuItem("GenScript/快捷添加Controller")]
    public static void ShowWindow() {
        GetWindow<FGUIViewTemplateGenerator>("快捷添加Controller");
        //TODO:读取FGUI文件夹下面的所有包和View,然后整理成列表之类的数据
    }

    private void OnGUI() {
        GUILayout.Label("输入Controller的名称(不需要后缀,Model也会用这个名称)", EditorStyles.boldLabel);
        _name = EditorGUILayout.TextField("Controller 名称:", _name);

        if (GUILayout.Button("生成")) {
            RunFunction();
            Debug.Log("生成成功");
        }
    }

    private void RunFunction() {

    }
}