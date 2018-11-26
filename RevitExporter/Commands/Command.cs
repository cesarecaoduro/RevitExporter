#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion

namespace RevitExporter
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            #region Extract Rooms
            List<Room> rooms = Utilities.GetRooms(doc); //get rooms from active document
            MessageBox.Show(rooms.Count.ToString());

            List<List<XYZ>> roomPolygons = new List<List<XYZ>>();
            List<RoomInfo> roomInfos = new List<RoomInfo>();
            string str = "";
            foreach (SpatialElement r in rooms)
            {
                RoomInfo roomInfo = new RoomInfo();
                roomInfo._Name = r.Name;
                List<List<RoomEdge>> roomEdges = Utilities.GetRoomEdges(doc, r);
                List<XYZ> roomPolygonPoints = new List<XYZ>();
                foreach(List<RoomEdge> l1 in roomEdges)
                {
                    foreach(RoomEdge rEdge in l1)
                    {
                        roomPolygonPoints.Add(rEdge._StartPoint);
                    }
                }
                roomPolygons.Add(roomPolygonPoints);
                roomInfos.Add(new RoomInfo()
                {
                    _Name = r.Name,
                    _RoomPoints = roomPolygonPoints
                });
                str += r.Name + Environment.NewLine;
            }
            MessageBox.Show(str);
            #endregion

            #region Get Linked Documents
            List<Document> linkedDocs = Utilities.GetLinkedDocuments(doc); //get all loaded linked documents
            linkedDocs.Add(doc); //add the active document to the list
            #endregion

            #region Get furniture locations from Linked documents
            List<ElementLocation> elLocations = new List<ElementLocation>();
            str = "";
            foreach (Document d in linkedDocs)
            {
                FilteredElementCollector collector = new FilteredElementCollector(d).
                    OfCategory(BuiltInCategory.OST_Furniture).WhereElementIsNotElementType();
                foreach(Element e in collector)
                {
                    elLocations.Add(new ElementLocation()
                    {
                        _ElementId = e.Id,
                        _ElementLocation = Utilities.GetElementLocation(d, e)
                    });
                    str += e.Id.ToString() + Environment.NewLine;
                }
            }
            MessageBox.Show(str);
            #endregion

            #region Check if element is in room
            str = "";

            foreach (ElementLocation elLoc in elLocations)
            {
                bool isInside = false;
                foreach (RoomInfo rInfo in roomInfos)
                {
                    if (Utilities.PointInPolygon(rInfo._RoomPoints, elLoc._ElementLocation) == true)
                    {
                        str += "Element " + elLoc._ElementId.ToString() + " is inside room " + rInfo._Name + Environment.NewLine;
                        isInside = true;
                        continue;
                    }
                }
                if (!isInside) { str += "Element " + elLoc._ElementId.ToString() + " is not inside a room" + Environment.NewLine; }
            }

            //foreach (RoomInfo rInfo in roomInfos)
            //{
            //    bool isInside = false;
            //    foreach (ElementLocation elLoc in elLocations)
            //    {
            //        if (Utilities.PointInPolygon(rInfo._RoomPoints, elLoc._ElementLocation) == true)
            //        {
            //            str += "Element " + elLoc._ElementId.ToString() + " is inside room " + rInfo._Name + Environment.NewLine;
            //            isInside = true;
            //            continue;
            //        }
            //    }

            //}
            MessageBox.Show(str);
            #endregion

            return Result.Succeeded;
        }
    }
}
