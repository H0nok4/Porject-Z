using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEditor.Rendering;
using UnityEngine;


namespace ConfigType
{
    public partial class DataManager : Singleton<DataManager>
    {
        private Dictionary<string, Sprite> _spriteCacheDic = new Dictionary<string, Sprite>();

        private GameObject _thingObject;

        private Sprite _frameSprite;

        public string FrameSpritePath = "Sprite/GeneralFrame";

        public Sprite FrameSprite {
            get {
                return GetSpriteByPath(FrameSpritePath);
            }
        }

        public GameObject ThingObject {
            get {
                if (_thingObject == null) {
                    _thingObject = Resources.Load<GameObject>("GameObject/ThingObject");
                }

                return _thingObject;
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

        private MainPanel _mainPanel;

        public Type MainPanelType => typeof(MainPanel);

        public void Init() {

        }

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

            Debug.LogError($"找不到对应路径的Sprite,使用的路径为:{path}");
            return null;
        }
    }

}
