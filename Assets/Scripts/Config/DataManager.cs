using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ConfigType
{
    public partial class DataManager : Singleton<DataManager>
    {
        public void InitConfigs()
        {
            string ConfigPath = "Assets/Resources/Config/xml/";
            InitDictionary();
        }

        public void InitDictionary()
        {
        }
    }
}