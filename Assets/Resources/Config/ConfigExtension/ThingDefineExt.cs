using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.Utilities;
using UnityEngine;

namespace ConfigType {
    public partial class ThingDefine {
        private int _uniqueID = -1;
        public int UniqueID {
            get {
                if (_uniqueID < 0) {
                    _uniqueID = UniqueIDUtility.GetUID();
                }

                return _uniqueID;
            }
        }
        public IntVec2 Size => new IntVec2(this.SizeX, this.SizeY);

        public bool IsFrame;

        public bool IsBlueprint;

        private ThingDefine _blueprintDefInstance;

        public ThingDefine BlueprintDef {
            get {
                if (_blueprintDefInstance == null)
                    ThingUtility.CreateBlueprintDefToThingDef(this);

                return _blueprintDefInstance;
            }
            set => _blueprintDefInstance = value;
        }

        private ThingDefine _frameDefInstance;
        public ThingDefine FrameDef {
            get {
                if (_frameDefInstance == null) {
                    ThingUtility.CreateFrameDefToThingDef(this);
                }


                return _frameDefInstance;
            }
            set => _frameDefInstance = value;
        }



        public ThingDefine EntityBuildDef;

        public Sprite BlueprintSprite {
            get
            {
                return DataManager.Instance.GetSpriteByPath(BlueprintSpritePath);
            }
        }

        public Sprite ThingSprite {
            get {
                return DataManager.Instance.GetSpriteByPath(ThingSpritePath);
            }
        }

        public Sprite FrameSprite {
            get {
                return DataManager.Instance.GetSpriteByPath(FrameSpritePath);
            }
        }

        private List<DefineThingClassCount> _costList;
        public List<DefineThingClassCount> CostList
        {
            get
            {
                if (_costList == null || (_costList.IsNullOrEmpty() && (BuildCostThingID.Count != 0 || BuildCostThingNum.Count != 0)))
                {
                    _costList = new List<DefineThingClassCount>();
                    for (int i = 0; i < BuildCostThingID.Count; i++)
                    {
                        var thingDef = DataManager.Instance.GetThingDefineByID(BuildCostThingID[i]);
                        _costList.Add(new DefineThingClassCount(){Def = thingDef,Count = BuildCostThingNum[i],DefineName = thingDef.Name});
                    }
                }

                return _costList;
            }   
        }


    }

}
