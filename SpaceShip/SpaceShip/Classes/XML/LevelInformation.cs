using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SpaceShip.Classes.XML
{

    public class LevelLoader
    {
        
        //Enemy ü Object Spawn
        //load Level

        public LevelInformation LevelInfos { get; set; }

        public LevelLoader()
        {
        }


        public void ReadFromFile(string path)
        {
            XmlSerializer ser = new XmlSerializer(typeof(LevelInformation));
            LevelInfos = null;
            using (XmlReader reader = XmlReader.Create(path))
            {
                LevelInfos = (LevelInformation)ser.Deserialize(reader);
            }
        }

        public void SaveToFile(string path)
        {
            var res = XMLParser.SeralizeObjectToXML(LevelInfos);
            System.IO.StreamWriter file = new System.IO.StreamWriter(@path);
            file.WriteLine(res);
            file.Close();
        }
    }

    [XmlType(TypeName = "LevelInformation")]
    public class LevelInformation
    {
        [XmlArrayItem("Enemy")]
        public List<EnemyObject> EnemyInfos;
    }
}
