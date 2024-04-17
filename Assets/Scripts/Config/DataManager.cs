using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ConfigType
{
    public partial class DataManager : Singleton<DataManager>
    {
        public List<DesignTypeDefine> DesignTypeDefineList = new List<DesignTypeDefine>();
        public Dictionary<int, DesignTypeDefine> DesignTypeDefineDic = new Dictionary<int, DesignTypeDefine>();
        public DesignTypeDefine GetDesignTypeDefineByID(int ID)
        {
            return DesignTypeDefineDic[ID];
        }

        public List<JobDefine> JobDefineList = new List<JobDefine>();
        public Dictionary<int, JobDefine> JobDefineDic = new Dictionary<int, JobDefine>();
        public JobDefine GetJobDefineByID(int ID)
        {
            return JobDefineDic[ID];
        }

        public List<ThingDefine> ThingDefineList = new List<ThingDefine>();
        public Dictionary<int, ThingDefine> ThingDefineDic = new Dictionary<int, ThingDefine>();
        public ThingDefine GetThingDefineByID(int ID)
        {
            return ThingDefineDic[ID];
        }

        public List<WorkGiverDefine> WorkGiverDefineList = new List<WorkGiverDefine>();
        public Dictionary<int, WorkGiverDefine> WorkGiverDefineDic = new Dictionary<int, WorkGiverDefine>();
        public WorkGiverDefine GetWorkGiverDefineByID(int ID)
        {
            return WorkGiverDefineDic[ID];
        }

        public void InitConfigs()
        {
            string ConfigPath = "Assets/Resources/Config/xml/";
            FileStream DesignTypeStream = File.OpenRead(ConfigPath + "DesignType.xml");
            XmlSerializer DesignTypeDefineserializer = new XmlSerializer(typeof(List<DesignTypeDefine>));
            DesignTypeDefineList = (List<DesignTypeDefine>)DesignTypeDefineserializer.Deserialize(DesignTypeStream);
            FileStream JobStream = File.OpenRead(ConfigPath + "Job.xml");
            XmlSerializer JobDefineserializer = new XmlSerializer(typeof(List<JobDefine>));
            JobDefineList = (List<JobDefine>)JobDefineserializer.Deserialize(JobStream);
            FileStream ThingStream = File.OpenRead(ConfigPath + "Thing.xml");
            XmlSerializer ThingDefineserializer = new XmlSerializer(typeof(List<ThingDefine>));
            ThingDefineList = (List<ThingDefine>)ThingDefineserializer.Deserialize(ThingStream);
            FileStream WorkGiverStream = File.OpenRead(ConfigPath + "WorkGiver.xml");
            XmlSerializer WorkGiverDefineserializer = new XmlSerializer(typeof(List<WorkGiverDefine>));
            WorkGiverDefineList = (List<WorkGiverDefine>)WorkGiverDefineserializer.Deserialize(WorkGiverStream);
            InitDictionary();
        }

        public void InitDictionary()
        {
            foreach (var i in DesignTypeDefineList)
            {
                DesignTypeDefineDic.Add(i.ID, i);
            }

            foreach (var i in JobDefineList)
            {
                JobDefineDic.Add(i.ID, i);
            }

            foreach (var i in ThingDefineList)
            {
                ThingDefineDic.Add(i.ID, i);
            }

            foreach (var i in WorkGiverDefineList)
            {
                WorkGiverDefineDic.Add(i.ID, i);
            }
        }
    }
}