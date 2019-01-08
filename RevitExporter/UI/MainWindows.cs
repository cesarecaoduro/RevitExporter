using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace RevitExporter.UI
{
    public partial class MainWindows : System.Windows.Forms.Form
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Application _app;
        private Document _doc;

        private DataTable _Dt = new DataTable();
        private List<RoomInfo> _Rooms = new List<RoomInfo>();
        private List<Room> _rooms = new List<Room>();

        public MainWindows(ExternalCommandData commData)
        {
            InitializeComponent();
            _uiapp = commData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _app = _uiapp.Application;
            _doc = _uidoc.Document;
            _rooms = Utilities.GetRooms(_doc);
            foreach(Room room in _rooms)
            {
                RoomInfo r = new RoomInfo();
                r._Id = room.Id.IntegerValue;
                r._Sede = _doc.GetElement(room.Id).LookupParameter("Sede").AsString();
                r._Societa = _doc.GetElement(room.Id).LookupParameter("Societa").AsString();
                r._Area = _doc.GetElement(room.Id).LookupParameter("Area").AsString();
                r._Divisione = _doc.GetElement(room.Id).LookupParameter("Divisione").AsString();
                r._CdC = _doc.GetElement(room.Id).LookupParameter("CdC").AsString();
                r._Name = _doc.GetElement(room.Id).get_Parameter(BuiltInParameter.ROOM_NAME).AsString();
                r._LevelName = _doc.GetElement(room.Id).get_Parameter(BuiltInParameter.ROOM_LEVEL_ID).AsValueString();
                r._Number = _doc.GetElement(room.Id).get_Parameter(BuiltInParameter.ROOM_NUMBER).AsString();
                r._FloorType = _doc.GetElement(room.Id).get_Parameter(BuiltInParameter.ROOM_FINISH_FLOOR).AsString();
                r._OccupiedDesks = _doc.GetElement(room.Id).LookupParameter("Postazioni.Occupate").AsInteger();
                r._TheoreticalDesks = _doc.GetElement(room.Id).LookupParameter("Postazioni.Potenziali").AsInteger();
                r._Desks = _doc.GetElement(room.Id).LookupParameter("Postazioni").AsInteger();
                r._RoomType = _doc.GetElement(room.Id).LookupParameter("Tipo.Locale").AsString();
                r._MeetingRoomDesks = _doc.GetElement(room.Id).LookupParameter("Postazioni.Sala.Riunioni").AsInteger();
                r._Desks = _doc.GetElement(room.Id).LookupParameter("Postazioni").AsInteger();
                r._MeetingRoomDesks = _doc.GetElement(room.Id).LookupParameter("Postazioni.Sala.Corsi.Collaudi").AsInteger();
                r._NetArea = UnitUtils.ConvertFromInternalUnits(room.Area, DisplayUnitType.DUT_SQUARE_METERS);
                r._OpaqueDoors = _doc.GetElement(room.Id).LookupParameter("Porte.Opache").AsInteger();
                r._FireRatedDoors = _doc.GetElement(room.Id).LookupParameter("Porte.REI").AsInteger();
                r._GlassDoors = _doc.GetElement(room.Id).LookupParameter("Porte.Vetrate").AsInteger();
                r._Efficency = _doc.GetElement(room.Id).LookupParameter("Porte.Vetrate").AsDouble();

                _Rooms.Add(r);
            }
            
        }

        private void MainWindows_Load(object sender, EventArgs e)
        {
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (_rooms.Count != 0)
            {
                Utilities.ExportToExcel(_Rooms);
            }
            DialogResult = DialogResult.OK;
        }

        private void btnExportJSON_Click(object sender, EventArgs e)
        {
            if (_rooms.Count != 0)
            {
                Utilities.ExportJson(_Rooms);
            }
            DialogResult = DialogResult.OK;
        }
    }
}
