using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;


namespace RevitExporter
{
    public class Utilities
    {
        public List<Document> GetLinkedDocuments(Document doc)
        {
            List<Document> linkedDocs = new List<Document>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).
                OfCategory(BuiltInCategory.OST_RvtLinks).
                WhereElementIsElementType();
  
            foreach(Element e in collector)
                linkedDocs.Add(((RevitLinkInstance)e).GetLinkDocument());
            
            return linkedDocs;
        }

        public List<Room> GetRooms(Document doc)
        {
            List<Room> rooms = new List<Room>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).
                OfCategory(BuiltInCategory.OST_Rooms).
                WhereElementIsElementType();

            foreach (Element e in collector)
                rooms.Add((Room)e);

            return rooms;
        }

        public void GetRoomEdges(Document doc, Room room)
        {
            //Extract the boundary curves for a room based on the center of the enclosing element
            SpatialElementBoundaryOptions opt = new SpatialElementBoundaryOptions();
            opt.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Center;
            IList<IList<BoundarySegment>> boundarySegments = room.GetBoundarySegments(opt);

            foreach (List<BoundarySegment> l1 in boundarySegments)
            {
                List<XYZ> polygonPoints = new List<XYZ>(); //get all points for a room polygon
                foreach (BoundarySegment l2 in l1)
                {
                    Curve crv = l2.GetCurve();
                    polygonPoints.Add(crv.GetEndPoint(0)); //get initial point only
                }
            }
        }

    }
}
