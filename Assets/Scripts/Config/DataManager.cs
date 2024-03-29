using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ConfigType
{
    public partial class DataManager : Singleton<DataManager>
    {
        public List<WorkGiverDefineDefine> WorkGiverDefineDefineList = new List<WorkGiverDefineDefine>();
        public Dictionary<int, WorkGiverDefineDefine> WorkGiverDefineDefineDic = new Dictionary<int, WorkGiverDefineDefine>();
        public WorkGiverDefineDefine GetWorkGiverDefineDefineByID(int ID)
        {
            return WorkGiverDefineDefineDic[ID];
        }

        public void InitConfigs()
        {
            string ConfigPath = "Assets/Resources/Config/xml/";
            FileStream WorkGiverDefineStream = File.OpenRead(ConfigPath + "WorkGiverDefine.xml");
            XmlSerializer WorkGiverDefineDefineserializer = new XmlSerializer(typeof(List<WorkGiverDefineDefine>));
            WorkGiverDefineDefineList = (List<WorkGiverDefineDefine>)WorkGiverDefineDefineserializer.Deserialize(WorkGiverDefineStream);
            InitDictionary();
        }

        public void InitDictionary()
        {
            foreach (var i in WorkGiverDefineDefineList)
            {
                WorkGiverDefineDefineDic.Add(i.ID, i);
            }
        }
    }
}