using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitExporter
{
    public class RoomEdge
    {
        public Curve _Edge { get; set; }
        public XYZ _StartPoint { get; set; }
    }

    public class RoomInfo
    {
        public int _Id { get; set; }
        public string _Sede { get; set; }
        public string _Societa { get; set; }
        public string _Area { get; set; }
        public string _Divisione { get; set; }
        public string _CdC { get; set; }
        public string _Name { get; set; }
        public List<XYZ> _RoomPoints { get; set; }
        public Room _Room { get; set; }
        public string _LevelName { get; set; }
        public string _Number { get; set; }
        public string _RoomType { get; set; }
        public int _OccupiedDesks { get; set; }
        public int _Desks { get; set; }
        public int _TheoreticalDesks { get; set; }
        public int _MeetingRoomDesks { get; set; }
        public int _TrainingRoomDesks { get; set; }
        public double _NetArea { get; set; }
        public int _Windows { get; set; }
        public int _GlassDoors { get; set; }
        public int _OpaqueDoors { get; set; }
        public int _FireRatedDoors { get; set; }
        public string _FloorType { get; set; }
        public string _Notes { get; set; }
        public double _Efficency { get; set; }

    }

    public class ElementLocation
    {
        public ElementId _ElementId { get; set; }
        public XYZ _ElementLocation { get; set; }
    }

    public class ExportStructure
    {
        //public Document _Document { get; set; }
        public string _DocumentName { get; set; }
        //public Category _Category { get; set; }
        public string _CategoryName { get; set; }
        public List<ParameterItems> _Parameters { get; set; }
    }

    public class ParameterItems
    {
        public string _ParameterName { get; set; }
        //public StorageType _ParameterType { get; set; }
        public double _ParameterAsDouble { get; set; }
        public int _ParameterAsInt { get; set; }
        public string _ParameterAsValueString { get; set; }
        public string _ParameterAsString { get; set; }
        public bool IsTypeParameter { get; set; }
    }

    public class ExportedElement
    {
        public int _ELementId { get; set; }
        public List<ParameterItems> _Parameters { get; set; }
        public string _RoomName { get; set; }
    }
}
