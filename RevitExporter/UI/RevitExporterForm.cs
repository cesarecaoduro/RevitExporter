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
    public partial class RevitExporterForm : System.Windows.Forms.Form
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Application _app;
        private Document _doc;

        private DataTable _Dt = new DataTable();

        private List<Document> _Documents = new List<Document>();
        List<ElementType> _ElementTypes = new List<ElementType>();
        List<Parameter> _ElementParameters = new List<Parameter>();

        private List<RoomInfo> _Rooms = new List<RoomInfo>();
        private List<Room> _rooms = new List<Room>();
        Dictionary<string, Parameter> roomParameters = new Dictionary<string, Parameter>();

        public RevitExporterForm(ExternalCommandData commData)
        {
            InitializeComponent();
            _uiapp = commData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _app = _uiapp.Application;
            _doc = _uidoc.Document;
            _rooms = Utilities.GetRooms(_doc);
            _Documents = Utilities.GetAllDocuments(_doc);
        }

        private void RevitExporterForm_Load(object sender, EventArgs e)
        {
            PopulateDocuments(_Documents);
            PopolateRoomParameters();
            //PopulateFurnitureParameters(_Documents);
            //PopulateFamilyTypes(_Documents);
        }

        private void PopulateDocuments(List<Document> documents)
        {
            lbDocuments.Items.Clear();
            foreach(Document d in documents)
            {
                lbDocuments.Items.Add(d.Title);
            }
        }

        private void PopulateFamilyTypes(List<Document> documents)
        {
            lbELementTypes.Items.Clear();
            lbELementParameters.Items.Clear();
            _ElementTypes = new List<ElementType>();
            _ElementParameters = new List<Parameter>();
            foreach (Document d in documents)
            {
                FilteredElementCollector coll = new FilteredElementCollector(d).
                    OfCategory(BuiltInCategory.OST_Furniture).
                    WhereElementIsNotElementType();

                //ElementType elType = coll.FirstElement() as ElementType;
                List<ElementType> elements = new List<ElementType>();
                foreach (Element et in coll)
                {
                    ElementId eTypeId = et.GetTypeId();
                    ElementType eType = d.GetElement(eTypeId) as ElementType;
                    elements.Add(eType);
                }

                foreach (var dict in elements.GroupBy(x => x.FamilyName))
                {
                    lbELementTypes.Items.Add(dict.Key);
                }

                coll = new FilteredElementCollector(d).
                OfCategory(BuiltInCategory.OST_Furniture).
                WhereElementIsNotElementType();

                try
                {
                    Element el = coll.FirstElement() as Element;
                    ParameterSet elParameters = el.Parameters;

                    foreach (Parameter p in elParameters)
                    {
                        try
                        {
                            _ElementParameters.Add(p);
                        }
                        catch (Exception ex) { }
                    }

                    _ElementParameters.Select(x => x.Definition.Name).Distinct();
                    foreach (Parameter p in _ElementParameters)
                    {
                        lbELementParameters.Items.Add(p.Definition.Name);
                    }
                }
                catch { }
            }

            lbELementTypes.Sorted = true;
            lbELementParameters.Sorted = true;
        }

        private void PopolateRoomParameters()
        {
            roomParameters.Clear();
            lbRoomParameters.Items.Clear();
            FilteredElementCollector coll = new FilteredElementCollector(_doc).
                OfCategory(BuiltInCategory.OST_Rooms).
                WhereElementIsNotElementType();

            Element el = coll.FirstElement() as Element;
            ParameterSet parameters = el.Parameters;
            foreach(Parameter p in parameters)
            {
                try
                {
                    roomParameters.Add(p.Definition.Name, p);
                    lbRoomParameters.Items.Add(p.Definition.Name);
                }
                catch(Exception ex) { }
            }
            lbRoomParameters.Sorted = true;
        }

        private void lbDocuments_SelectedValueChanged(object sender, EventArgs e)
        {
            List<Document> docs = new List<Document>();
            foreach (int indexChecked in lbDocuments.CheckedIndices)
            {
                docs.Add(_Documents[indexChecked]);
            }
            PopulateFamilyTypes(docs);
        }
    }
}
