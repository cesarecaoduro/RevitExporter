using Autodesk.Revit.DB;
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
        public string _Name { get; set; }
        public List<XYZ> _RoomPoints { get; set; }
    }

    public class ElementLocation
    {
        public ElementId _ElementId { get; set; }
        public XYZ _ElementLocation { get; set; }
    }
}
