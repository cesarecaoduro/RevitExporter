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
using OfficeOpenXml;
using System.IO;

namespace RevitExporter.UI
{
    public partial class MainFormW : System.Windows.Forms.Form
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Application _app;
        private Document _doc;
        private List<Category> _SelectedCategories = new List<Category>();
        private List<Category> _ExportCategories = new List<Category>();
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
                cmbCategories.Items.Add(c.Name);
            }
            cmbCategories.SelectedIndex = 0;

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void MainFormW_Load(object sender, EventArgs e)
        {
            dgRooms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgRooms.RowHeadersVisible = false;

            dgDocuments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgDocuments.RowHeadersVisible = false;

            List<Room> rooms = Utilities.GetRooms(_doc);
            foreach(Room r in rooms)
            {
                DataRow row = dt.Rows.Add();
                row[0] = r.Name;
            }
            dgRooms.DataSource = dt;
        }

        private void salvaTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_ExportStructure.Count() != 0)
                Utilities.ExportJson(_ExportStructure);
            else
                TaskDialog.Show("Message", "Nothing to export!");
        }


        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category cat = _SelectedCategories[cmbCategories.SelectedIndex];

            

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool exists = false;
            foreach (string obj in lbExportObjects.Items)
            {
                if (obj == _SelectedCategories[cmbCategories.SelectedIndex].Name) { exists = true; }
            }
            if (!exists)
            {
                lbExportObjects.Items.Add(_SelectedCategories[cmbCategories.SelectedIndex].Name);
                _ExportCategories.Add(_SelectedCategories[cmbCategories.SelectedIndex]);
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try {
                if (lbExportObjects.Items.Count != 0)
                {
                    _ExportCategories.RemoveAt(lbExportObjects.SelectedIndex);
                    lbExportObjects.Items.RemoveAt(lbExportObjects.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                TaskDialog td = new TaskDialog("Error");
                td.MainIcon = TaskDialogIcon.TaskDialogIconError;
                td.MainContent = "Seleziona almeno una categoria da rimuovere";
                td.Show();

            }
            
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                _ExportCategories.Clear();
                lbExportObjects.Items.Clear();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                _ExportCategories.Clear();
                lbExportObjects.Items.Clear();
                foreach(Category c in _SelectedCategories)
                {
                    _ExportCategories.Add(c);
                    lbExportObjects.Items.Add(c.Name);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string str = "";
            //foreach (Category c in _ExportCategories)
            //{
            //    str += c.Name + Environment.NewLine;
            //}
            //TaskDialog.Show("Export categories", str);

            if (_ExportCategories.Count != 0)
            {
                SaveFileDialog file = new SaveFileDialog();
                file.DefaultExt = "xlsx";
                file.Filter = "Excel Files (*.xlsx)|*.xlsx";
                file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (file.ShowDialog() == DialogResult.OK)
                {
                    var filePath = new FileInfo(file.FileName);
                    using (ExcelPackage xlPackage = new ExcelPackage(filePath))
                    {
                        foreach (Category cat in _ExportCategories)
                        {
                            try
                            {
                                ExcelWorksheet ws = xlPackage.Workbook.Worksheets.Add(cat.Name);
                                foreach (Document d in _Documents)
                                {
                                    FilteredElementCollector coll = new FilteredElementCollector(d).OfCategoryId(cat.Id).WhereElementIsNotElementType();
                                    if (coll.Count() != 0)
                                    {

                                        //TaskDialog.Show("Title", d.Title + " / Category: " + coll.Count().ToString());
                                        
                                        ws.Cells[1, 1].Value = "Element ID";
                                        ws.Cells[1, 2].Value = "Element Type ID";

                                        int i = 2;
                                        foreach (Element el in coll)
                                        {
                                            int j = 3;
                                            ws.Cells[i, 1].Value = el.Id.ToString();
                                            ws.Cells[i, 2].Value = el.GetTypeId().ToString();

                                            foreach (Parameter p in el.GetOrderedParameters())
                                            {
                                                if (i == 2) { ws.Cells[i-1, j].Value = p.Definition.Name; }
                                                else
                                                {
                                                    switch (p.StorageType)
                                                    {
                                                        case StorageType.Double: ws.Cells[i, j].Value = p.AsDouble(); break;
                                                        case StorageType.ElementId: ws.Cells[i, j].Value = p.AsValueString(); break;
                                                        case StorageType.Integer: ws.Cells[i, j].Value = p.AsInteger(); break;
                                                        case StorageType.String: ws.Cells[i, j].Value = p.AsString(); break;
                                                    }
                                                }
                                                j++;
                                            }
                                            i++;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                            
                        }
                        
                        xlPackage.Save();
                        xlPackage.Dispose();
                    }
                }
            }
            else
            {
                TaskDialog td = new TaskDialog("Error");
                td.MainIcon = TaskDialogIcon.TaskDialogIconError;
                td.MainContent = "Seleziona almeno una categoria da esportare";
                td.Show();
            }
        }
    }
}
