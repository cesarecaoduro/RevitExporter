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
using OfficeOpenXml;
using OfficeOpenXml.Style;

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
                if(e.Area != 0)
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

        internal static void ExportToExcel(List<RoomInfo> rooms)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "Excel Files (*.xlsx)|*.xlsx";
            sd.DefaultExt = "xlsx";
            sd.AddExtension = true;
            string initialPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
            if (sd.ShowDialog() == DialogResult.OK)
            {
                var fi = new FileInfo(sd.FileName);
                using (ExcelPackage package = new ExcelPackage(fi))
                {
                    ExcelWorksheet ws;
                    try
                    {
                        ws = package.Workbook.Worksheets.Add("Ambienti");
                    }
                    catch
                    {
                    }

                    ws = package.Workbook.Worksheets["Ambienti"];

                    ws.Cells[1, 1].Value = "Sede";
                    ws.Cells[1, 2].Value = "Società";
                    ws.Cells[1, 3].Value = "Divisione";
                    ws.Cells[1, 4].Value = "Direzione";
                    ws.Cells[1, 5].Value = "CdC";
                    ws.Cells[1, 6].Value = "Piano";
                    ws.Cells[1, 7].Value = "Ambiente";
                    ws.Cells[1, 8].Value = "Tipo";
                    ws.Cells[1, 9].Value = "Postazioni";
                    ws.Cells[1, 10].Value = "Postazioni Occupate";
                    ws.Cells[1, 11].Value = "Postazioni Potenziali";
                    ws.Cells[1, 12].Value = "Postazioni sale riunioni";
                    ws.Cells[1, 13].Value = "Postazioni sale corsi e collaudi";
                    ws.Cells[1, 14].Value = "mq netti";
                    ws.Cells[1, 15].Value = "n. finestre";
                    ws.Cells[1, 16].Value = "n. porte vetrate";
                    ws.Cells[1, 17].Value = "n. porte opache";
                    ws.Cells[1, 18].Value = "n. porte REI";
                    ws.Cells[1, 19].Value = "Tipo Pavimento";
                    ws.Cells[1, 20].Value = "Note";
                    ws.Cells[1, 21].Value = "Efficienza";
                    ws.Cells[1, 22].Value = "Id";

                    ws.Cells.Style.Font.Name = "Arial";
                    using (ExcelRange rng = ws.Cells["A1:V1"])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkRed);
                        rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    }

                    int rowIndex = 2;
                    foreach (RoomInfo r in rooms)
                    {
                        ws.Cells[rowIndex, 1].Value = r._Sede;
                        ws.Cells[rowIndex, 2].Value = r._Societa;
                        ws.Cells[rowIndex, 3].Value = r._Divisione;
                        ws.Cells[rowIndex, 4].Value = r._Area;
                        ws.Cells[rowIndex, 5].Value = r._CdC;
                        ws.Cells[rowIndex, 6].Value = r._LevelName;
                        ws.Cells[rowIndex, 7].Value = r._Name;
                        ws.Cells[rowIndex, 8].Value = r._RoomType;
                        ws.Cells[rowIndex, 9].Value = r._Desks;
                        ws.Cells[rowIndex, 10].Value = r._OccupiedDesks;
                        ws.Cells[rowIndex, 11].Value = r._TheoreticalDesks;
                        ws.Cells[rowIndex, 12].Value = r._MeetingRoomDesks;
                        ws.Cells[rowIndex, 13].Value = r._TrainingRoomDesks;
                        ws.Cells[rowIndex, 14].Value = Math.Round(r._NetArea,3);
                        ws.Cells[rowIndex, 15].Value = r._Windows;
                        ws.Cells[rowIndex, 16].Value = r._GlassDoors;
                        ws.Cells[rowIndex, 17].Value = r._OpaqueDoors;
                        ws.Cells[rowIndex, 18].Value = r._FireRatedDoors;
                        ws.Cells[rowIndex, 19].Value = r._FloorType;
                        ws.Cells[rowIndex, 20].Value = r._Notes;
                        ws.Cells[rowIndex, 21].Value = r._Efficency;
                        ws.Cells[rowIndex, 22].Value = r._Id;
                        rowIndex++;
                    }
                    package.Save();
                    MessageBox.Show("Dati exportati!");
                }

            }
        }
    }

    
}
