using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public class BuildingDesignator : DesignatorDecoratorBase {

    public ThingDefine BuildingDefine;
    public override string Sprite => BuildingDefine.ThingSpritePath;
    public override string Name => BuildingDefine.Name;

    public BuildingDesignator(ThingDefine def) {
        BuildingDefine = def;
    }
    public override void OnClick() {
        //TODO:现在测试用,以后点击后,进入对应建筑的建造模式
        DesignatorManager.Instance.DesignatorType = DesignatorType.Building;
    }
}