using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Newtonsoft.Json;


namespace RevitExporter
{
    class DistinctItemComparer : IEqualityComparer<Category>
    {

        public bool Equals(Category x, Category y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Category obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public class Utilities
    {
        public static List<Document> GetAllDocuments(Document doc)
        {
            List<Document> linkedDocs = new List<Document>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).
                OfClass(typeof(RevitLinkInstance));

            foreach (RevitLinkInstance e in collector)
            {
                linkedDocs.Add(e.GetLinkDocument());
            }
            linkedDocs.Add(doc);
            return linkedDocs;
        }

        public static List<Room> GetRooms(Document doc)
        {
            List<Room> rooms = new List<Room>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).
                OfCategory(BuiltInCategory.OST_Rooms).
                WhereElementIsNotElementType();

            foreach (Room e in collector)
            {
                rooms.Add(e);
            }

            return rooms;
        }

        public static List<List<RoomEdge>> GetRoomEdges(Document doc, SpatialElement room)
        {
            //Extract the boundary curves for a room based on the center of the enclosing element
            SpatialElementBoundaryOptions opt = new SpatialElementBoundaryOptions();
            opt.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Center;
            IList<IList<BoundarySegment>> boundarySegments = room.GetBoundarySegments(opt);
            List<List<RoomEdge>> allRoomEdges = new List<List<RoomEdge>>();

            foreach (List<BoundarySegment> l1 in boundarySegments)
            {
                List<RoomEdge> roomEdges = new List<RoomEdge>();
                foreach (BoundarySegment l2 in l1)
                {
                    RoomEdge roomEdge = new RoomEdge()
                    {
                        _Edge = l2.GetCurve(),
                        _StartPoint = l2.GetCurve().GetEndPoint(0),
                    };
                    roomEdges.Add(roomEdge);
                }
                allRoomEdges.Add(roomEdges);
            }

            return allRoomEdges;
        }

        public static XYZ GetElementLocation(Document doc, Element el)
        {
            try
            {
                LocationPoint loc = el.Location as LocationPoint;
                return loc.Point;
            }
            catch(Exception ex) { return null;  }

        }

        public static bool PointInPolygon(List<XYZ> polyPoints, XYZ point)
        {
            int polyCorners = polyPoints.Count;
            int i, j = polyPoints.Count - 1;
            bool oddNodes = false;

            for (i = 0; i < polyCorners; i++)
            {
                if ((polyPoints[i].Y < point.Y && polyPoints[j].Y >= point.Y
                || polyPoints[j].Y < point.Y && polyPoints[i].Y >= point.Y)
                && (polyPoints[i].X <= point.X || polyPoints[j].X <= point.X))
                {
                    if (polyPoints[i].X + (point.Y - polyPoints[i].Y) / (polyPoints[j].Y - polyPoints[i].Y) * (polyPoints[j].X - polyPoints[i].X) < point.X)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                j = i;
            }

            return oddNodes;
        }

        public static List<Category> GetNotEmptyCategories(List<Document> documents, Categories categories)
        {
            
            ElementCategoryFilter f1 = new ElementCategoryFilter(BuiltInCategory.OST_Rooms, true);
            ElementCategoryFilter f2 = new ElementCategoryFilter(BuiltInCategory.OST_PipeSegments, true);
            ElementCategoryFilter f3 = new ElementCategoryFilter(BuiltInCategory.OST_Materials, true);

            List<ElementFilter> filters = new List<ElementFilter>()
            {
                f1, f2, f3
            };
            LogicalAndFilter filter = new LogicalAndFilter(filters);


            List<Category> categoryList = new List<Category>();
            foreach (Document d in documents)
            {
                foreach (Category c in categories)
                {
                    BuiltInCategory bCat = (BuiltInCategory)Enum.Parse(typeof(BuiltInCategory), c.Id.ToString());
                    if (c.CategoryType == CategoryType.Model)
                    {
                        FilteredElementCollector coll = new FilteredElementCollector(d).OfCategoryId(c.Id).WhereElementIsNotElementType().WherePasses(filter);
                        if (coll.Count() > 1) { categoryList.Add(c); }
                    }
                }
            }
            return categoryList.Distinct(new DistinctItemComparer()).ToList();
        }

        internal static void ExportJson(object obj)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "Project Template Files (*.json)|*.json";
            sd.DefaultExt = "json";
            sd.AddExtension = true;
            string initialPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
            if (sd.ShowDialog() == DialogResult.OK)
            {
                string jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
                File.WriteAllText(sd.FileName, jsonString);
            }
        }
    }

    
}
