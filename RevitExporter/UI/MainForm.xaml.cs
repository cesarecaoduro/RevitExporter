using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RevitExporter;

using Autodesk.Revit.DB;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Architecture;
using System.Collections.ObjectModel;

namespace RevitExporter.UI
{
    public class CategoryItem : IComparable
    {
        public bool _IsSelected { get; set; } = false;
        public string _CategoryName { get; set; }
        public Category _Category { get; set;  }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public partial class MainForm : Window
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Application _app;
        private Document _doc;
        private List<Category> selCat = new List<Category>();

        public MainForm(ExternalCommandData commData)
        {
            InitializeComponent();
            _uiapp = commData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _app = _uiapp.Application;
            _doc = _uidoc.Document;

            Categories categories = _doc.Settings.Categories;
            List<CategoryItem> catItems = new List<CategoryItem>();
            
            foreach (Category cat in categories)
            {
                if (cat.CategoryType == CategoryType.Model)

                    catItems.Add(new CategoryItem()
                    {
                        _Category = cat,
                        _CategoryName = cat.Name,
                        _IsSelected = false
                    });
            }
            dgCategories.ItemsSource = catItems.OrderBy(x => x._CategoryName);

        }

        private void chkCatSelected_Click(object sender, RoutedEventArgs e)
        {
            CheckBox source = e.Source as CheckBox;
            if (source.IsChecked == true)
                MessageBox.Show("Selezionato");
            else
                MessageBox.Show("Non Selezionato");
        }



        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    #region Extract Rooms
        //    List<Room> rooms = Utilities.GetRooms(_doc); //get rooms from active document
        //    MessageBox.Show(rooms.Count.ToString());

        //    List<List<XYZ>> roomPolygons = new List<List<XYZ>>();
        //    List<RoomInfo> roomInfos = new List<RoomInfo>();
        //    string str = "";
        //    foreach (SpatialElement r in rooms)
        //    {
        //        RoomInfo roomInfo = new RoomInfo();
        //        roomInfo._Name = r.Name;
        //        List<List<RoomEdge>> roomEdges = Utilities.GetRoomEdges(_doc, r);
        //        List<XYZ> roomPolygonPoints = new List<XYZ>();
        //        foreach (List<RoomEdge> l1 in roomEdges)
        //        {
        //            foreach (RoomEdge rEdge in l1)
        //            {
        //                roomPolygonPoints.Add(rEdge._StartPoint);
        //            }
        //        }
        //        roomPolygons.Add(roomPolygonPoints);
        //        roomInfos.Add(new RoomInfo()
        //        {
        //            _Name = r.Name,
        //            _RoomPoints = roomPolygonPoints
        //        });
        //        str += r.Name + Environment.NewLine;
        //    }
        //    MessageBox.Show(str);
        //    #endregion

        //    #region Get Linked Documents
        //    List<Document> linkedDocs = Utilities.GetLinkedDocuments(_doc); //get all loaded linked documents
        //    linkedDocs.Add(_doc); //add the active document to the list
        //    #endregion

        //    #region Get furniture locations from Linked documents
        //    List<ElementLocation> elLocations = new List<ElementLocation>();
        //    str = "";
        //    foreach (Document d in linkedDocs)
        //    {
        //        FilteredElementCollector collector = new FilteredElementCollector(d).
        //            OfCategory(BuiltInCategory.OST_Furniture).WhereElementIsNotElementType();
        //        foreach (Element el in collector)
        //        {
        //            elLocations.Add(new ElementLocation()
        //            {
        //                _ElementId = el.Id,
        //                _ElementLocation = Utilities.GetElementLocation(d, el)
        //            });
        //            str += el.Id.ToString() + Environment.NewLine;
        //        }
        //    }
        //    MessageBox.Show(str);
        //    #endregion

        //    #region Check if element is in room
        //    str = "";

        //    foreach (ElementLocation elLoc in elLocations)
        //    {
        //        bool isInside = false;
        //        foreach (RoomInfo rInfo in roomInfos)
        //        {
        //            if (Utilities.PointInPolygon(rInfo._RoomPoints, elLoc._ElementLocation) == true)
        //            {
        //                str += "Element " + elLoc._ElementId.ToString() + " is inside room " + rInfo._Name + Environment.NewLine;
        //                isInside = true;
        //                continue;
        //            }
        //        }
        //        if (!isInside) { str += "Element " + elLoc._ElementId.ToString() + " is not inside a room" + Environment.NewLine; }
        //    }


        //    MessageBox.Show(str);
        //    #endregion

        //}
    }
}
