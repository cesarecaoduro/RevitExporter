using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;
using RevitExporter;
using Autodesk.Revit.DB.Architecture;
using System.Data;

namespace RevitExporter.UI
{
    public partial class MainFormW : System.Windows.Forms.Form
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Application _app;
        private Document _doc;
        private List<Category> _SelectedCategories = new List<Category>();
        private List<Document> _Documents = new List<Document>();
        List<ExportStructure> _ExportStructure;

        private DataTable dt = new DataTable();

        public class CategoryItem : IComparable
        {
            public bool _IsSelected { get; set; } = false;
            public string _CategoryName { get; set; }
            public Category _Category { get; set; }

            public int CompareTo(object obj)
            {
                throw new NotImplementedException();
            }
        }

        public MainFormW(ExternalCommandData commData)
        {
            InitializeComponent();
            _uiapp = commData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _app = _uiapp.Application;
            _doc = _uidoc.Document;

            _ExportStructure = new List<ExportStructure>();

            dt.Columns.Add("Room Name", typeof(string));

            Categories categories = _doc.Settings.Categories; // get a full list of categories
            _Documents = Utilities.GetAllDocuments(_doc); //get all documents including the active document
            _SelectedCategories = Utilities.GetNotEmptyCategories(_Documents, categories); //Get all categories with elements from all linked files
            foreach (Category c in _SelectedCategories)
            {
                lbCategories.Items.Add(c.Name);
            }

        }

        

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void MainFormW_Load(object sender, EventArgs e)
        {
            
            List<Room> rooms = Utilities.GetRooms(_doc);
            foreach(Room r in rooms)
            {
                DataRow row = dt.Rows.Add();
                row[0] = r.Name;
            }
            dgRooms.DataSource = dt;
        }

        private void lbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category cat = _SelectedCategories[lbCategories.SelectedIndex];


            try { lbParameters.Items.Clear(); }
            catch { }

            foreach (Document d in _Documents)
            {
                FilteredElementCollector coll = new FilteredElementCollector(d).OfCategoryId(cat.Id).WhereElementIsNotElementType();
                if (coll.Count() != 0)
                {
                    Element element = coll.FirstElement();
                    Element elementType = d.GetElement(element.GetTypeId()); 
                    IList<Parameter> parameters = element.GetOrderedParameters().
                        OrderBy(x => x.Definition.Name).
                        ToList();

                    IList<Parameter> typeParameters = elementType.GetOrderedParameters().
                        OrderBy(x => x.Definition.Name).
                        ToList();

                    foreach (Parameter p in parameters)
                    {
                        lbParameters.Items.Add(p.Definition.Name, true);
                    }
                    foreach (Parameter p in typeParameters)
                    {
                        lbParameters.Items.Add(p.Definition.Name);
                    }
                    break;
                }
               
            }
        }

        private void lbCategories_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            TaskDialog.Show("Message", "Selezionato");
        }

        private void salvaTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_ExportStructure.Count() != 0)
                Utilities.ExportJson(_ExportStructure);
            else
                TaskDialog.Show("Message", "Nothing to export!");
        }
            

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _ExportStructure.Add(new ExportStructure()
            {
                //_Document = _doc,
                _DocumentName = _doc.Title,
                //_Category = _SelectedCategories[lbCategories.SelectedIndex],
                _CategoryName = _SelectedCategories[lbCategories.SelectedIndex].Name,
            });
            lbExportObjects.Items.Add(_SelectedCategories[lbCategories.SelectedIndex].Name);
        }
    }
}
