using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UI;
using UnityEditor.Rendering;
using UnityEngine;


namespace ConfigType
{
    public partial class DataManager : Singleton<DataManager>
    {
        private Dictionary<string, Sprite> _spriteCacheDic = new Dictionary<string, Sprite>();

 

        private Sprite _frameSprite;

        public string FrameSpritePath = "Sprite/GeneralFrame";

        public Sprite FrameSprite {
            get {
                return GetSpriteByPath(FrameSpritePath);
            }
        }

        private GameObject _thingObject;
        public GameObject ThingObject {
            get {
                if (_thingObject == null) {
                    _thingObject = Resources.Load<GameObject>("GameObject/ThingObject");
                }

                return _thingObject;
            }
        }

        private GameObject _buildingObject;
        public GameObject BuildingObject {
            get {
                if (_buildingObject == null) {
                    _buildingObject = Resources.Load<GameObject>("GameObject/BuildingObject");
                }

                return _buildingObject;
            }
        }

        private GameObject _unitObject;
        public GameObject UnitObject {
            get {
                if (_unitObject == null) {
                    _unitObject = Resources.Load<GameObject>("GameObject/UnitObject");
                }

                return _unitObject;
            }
        }

        private PreviewObject _previewObject;

        public PreviewObject PreviewObject
        {
            get
            {
                if (_previewObject == null)
                {
                    _previewObject = Resources.Load<GameObject>("GameObject/PreviewObject").GetComponent<PreviewObject>();
                }

                return _previewObject;
            }
        }


        public Type MainPanelType => typeof(MainPanel);

        public Type ThingDesViewType => typeof(ThingDesView);

        public Type DesignViewType => typeof(DesignView);

        public Sprite GetSpriteByPath(string path)
        {
            if (_spriteCacheDic.ContainsKey(path))
            {
                return _spriteCacheDic[path];
            }

            var sprite = Resources.Load<Sprite>(path);

            if (sprite != null)
            {
                _spriteCacheDic.Add(path,sprite);
                return sprite;
            }

            Logger.Instance?.LogError($"找不到对应路径的Sprite,使用的路径为:{path}");
            return null;
        }
    }

}
