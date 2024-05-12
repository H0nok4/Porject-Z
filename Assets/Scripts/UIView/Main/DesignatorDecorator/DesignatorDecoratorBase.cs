using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class DesignatorDecoratorBase {
    //TODO:需要 图标 名称 点击事件

    public abstract string Sprite { get; }
    public abstract string Name { get; }
    public abstract void OnClick();
}