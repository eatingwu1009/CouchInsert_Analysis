using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Xml;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using System.Windows.Forms;
using System.Windows.Shapes;


namespace CouchInsert
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
        public List<string> MarkerNames { get; }
        public List<string> MarkerPositions { get; }

        private string _selectedMarkerName;
        public string SelectedMarkerName
        {
            get => _selectedMarkerName;
            set
            {
                _selectedMarkerName = value;
                NotifyPropertyChanged(nameof(SelectedMarkerName));
            }
        }
        private string _selectedMarkerPosition;
        public string SelectedMarkerPosition
        {
            get => _selectedMarkerPosition;
            set
            {
                _selectedMarkerPosition = value;
                NotifyPropertyChanged(nameof(SelectedMarkerPosition));
            }
        }

        private double? _markerLocationX;
        public double? MarkerLocationX
        {
            get => _markerLocationX;
            set
            {
                _markerLocationX = value;
                NotifyPropertyChanged(nameof(MarkerLocationX));
            }
        }
        private double? _markerLocationY;
        public double? MarkerLocationY
        {
            get => _markerLocationY;
            set
            {
                _markerLocationY = value;
                NotifyPropertyChanged(nameof(MarkerLocationY));
            }
        }
        private double? _markerLocationZ;
        public double? MarkerLocationZ
        {
            get => _markerLocationZ;
            set
            {
                _markerLocationZ = value;
                NotifyPropertyChanged(nameof(MarkerLocationZ));
            }
        }

        private double _markerLocationXX;
        public double MarkerLocationXX
        {
            get => _markerLocationXX;
            set
            {
                _markerLocationXX = value;
                NotifyPropertyChanged(nameof(MarkerLocationXX));
            }
        }
        private double _markerLocationYY;
        public double MarkerLocationYY
        {
            get => _markerLocationYY;
            set
            {
                _markerLocationYY = value;
                NotifyPropertyChanged(nameof(MarkerLocationYY));
            }
        }
        private double _markerLocationZZ;
        public double MarkerLocationZZ
        {
            get => _markerLocationZZ;
            set
            {
                _markerLocationZZ = value;
                NotifyPropertyChanged(nameof(MarkerLocationZZ));
            }
        }

        private Structure _markerLocationItem;
        public Structure MarkerLocationItem
        {
            get => _markerLocationItem;
            set
            {
                _markerLocationItem = value;
                NotifyPropertyChanged(nameof(MarkerLocationItem));
            }
        }

        private String _filePath;
        public String FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                NotifyPropertyChanged(nameof(FilePath));
            }
        }

        private String _filePathCI;
        public String FilePathCI
        {
            get => _filePathCI;
            set
            {
                _filePathCI = value;
                NotifyPropertyChanged(nameof(FilePathCI));
            }
        }

        private String _filePathCS;
        public String FilePathCS
        {
            get => _filePathCS;
            set
            {
                _filePathCS = value;
                NotifyPropertyChanged(nameof(FilePathCS));
            }
        }

        private String _filePathCSI;
        public String FilePathCSI
        {
            get => _filePathCSI;
            set
            {
                _filePathCSI = value;
                NotifyPropertyChanged(nameof(FilePathCSI));
            }
        }

        private String _filePathCSS;
        public String FilePathCSS
        {
            get => _filePathCSS;
            set
            {
                _filePathCSS = value;
                NotifyPropertyChanged(nameof(FilePathCSS));
            }
        }

        private double[] _alignment;
        public double[] Alignment
        {
            get => _alignment;
            set
            {
                _alignment = value;
                NotifyPropertyChanged(nameof(Alignment));
            }
        }

        public ScriptContext ScriptContext { get; }
        public StructureSet StructureSet { get; }
        public VVector SIU { get; }
        public ImageProfile XProfile { get; }
        public Structure CouchInterior { get; set; }
        public Structure CouchSurface { get; set; }
        public IReadOnlyList<Structure> couchStructureList { get; }
        public bool IsAddCouch { get; set; }

        public MainViewModel()
        {
            MarkerPositions = new List<string>();
            MarkerNames = new List<string>();
            MarkerPositions.Add("AA");
            MarkerNames.Add("BB");
            MarkerNames.Add("CC");
        }

        public MainViewModel(ScriptContext scriptContext)
        {
            ScriptContext = scriptContext;
            SIU = scriptContext.Image.UserOrigin;
            StructureSet = scriptContext.StructureSet;

            //Structure Circle = StructureSet.Structures.FirstOrDefault(e => e.Id == "Circle");
            //string filePath = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\contour.csv";
            //if (Circle != null)
            //{
            //    for (int i = 0; i < ScriptContext.Image.ZSize; i++)
            //    {
            //        foreach (VVector[] vectors in Circle.GetContoursOnImagePlane(i))
            //        {
            //            using (StreamWriter writer = new StreamWriter(filePath))
            //            {
            //                writer.WriteLine(String.Join(",", vectors.Select(v => $"{v.x}, {v.y}, {v.z}\n ")));
            //            }
            //        }
            //    }
            //}

            var MulMarker = StructureSet.Structures.Where(s => s.DicomType == "MARKER").ToList();
            if (MulMarker != null)
            {
                MarkerNames = new List<String>();
                foreach (Structure Iso in MulMarker)
                {
                    MarkerNames.Add(Iso.Id);
                }
                PositionRenew();
            }
            else System.Windows.MessageBox.Show("There is no marker");


            VVector start = new VVector(-ScriptContext.Image.XSize, MarkerLocationItem.CenterPoint.y, MarkerLocationItem.CenterPoint.z);
            VVector stop = new VVector(ScriptContext.Image.XSize, MarkerLocationItem.CenterPoint.y, MarkerLocationItem.CenterPoint.z);
            //double[] preallocatedBuffer = null;
            //XProfile = scriptContext.Image.GetImageProfile(start, stop, preallocatedBuffer);
            //MessageBox.Show(PeakDetect(XProfile));

            MarkerPositions = new List<String>();
            MarkerPositions.Add("H5");
            MarkerPositions.Add("H4");
            MarkerPositions.Add("H3");
            MarkerPositions.Add("H2");
            MarkerPositions.Add("H1");
            MarkerPositions.Add("0");

        }

        public ICommand PositionRenewCommand { get => new Command(PositionRenew); }
        private void PositionRenew()
        {
            string markerId = "Marker";
            if (SelectedMarkerName != null) markerId = SelectedMarkerName;
            MarkerLocationItem = StructureSet.Structures.Where(s => s.DicomType == "MARKER").ToList().Where(a => a.Id == markerId).FirstOrDefault();

            if (MarkerLocationItem != null)
            {
                MarkerLocationX = Math.Round((MarkerLocationItem.CenterPoint.x - SIU.x) / 10, 2);
                MarkerLocationY = Math.Round((MarkerLocationItem.CenterPoint.y - SIU.y) / 10, 2);
                MarkerLocationZ = Math.Round((MarkerLocationItem.CenterPoint.z - SIU.z) / 10, 2);
                MarkerLocationXX = MarkerLocationItem.CenterPoint.x;
                MarkerLocationYY = MarkerLocationItem.CenterPoint.y;
                MarkerLocationZZ = MarkerLocationItem.CenterPoint.z;
            }
            else
            {
                MarkerLocationX = null;
                MarkerLocationY = null;
                MarkerLocationZ = null;
            }
        }

        public ICommand ButtonCommand_AddCouch { get => new Command(AddCouch); }
        private void AddCouch()
        {

            string filePathOuter = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\contour.csv";
            //string filePathInner = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\contour.csv";

            if (!File.Exists(filePathOuter))
            {
                System.Windows.MessageBox.Show($"No file exists at path {filePathOuter}");
                return;
            }

            string[] filelines = File.ReadAllLines(filePathOuter);
            List<VVector> outer = new List<VVector>();
            try
            {
                foreach (string line in filelines)
                {
                    string[] splitLine = line.Split(',');
                    double x = Double.Parse(splitLine[0].Trim());
                    double y = Double.Parse(splitLine[1].Trim());
                    double z = Double.Parse(splitLine[2].Trim());
                    outer.Add(new VVector(x, y, z));
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("There was an error when reading the file.  Please make sure that all rows are in the form: number, number, number");
                return;
            }
            double Xmin = Math.Min(outer.);

            double[] AddAxis = AxisAlignment(SelectedMarkerPosition, Xmin, Ymin, Zmin);
            ScriptContext.Patient.BeginModifications();
            Structure CrossSurface = ScriptContext.StructureSet.AddStructure("CONTROL", "CrossSurface");
            for (int i = 0; i < Zmin + AddAxis[2]; i++)
            {
                foreach (VVector[] vectors in CouchInterior.GetContoursOnImagePlane(i))
                {
                    using (StreamWriter writer = new StreamWriter(FilePathCI))
                    {
                        writer.WriteLine(String.Join(",", vectors.Select(v => $"{v.x}, {v.y}, {v.z}\n ")));
                    }
                }
                CrossSurface.AddContourOnImagePlane(outer.Select(v => new VVector(v.x + AddAxis[0], v.y + AddAxis[1], v.z + AddAxis[2])).ToArray(), 10);
            }
            


            //Structure CouchInterior = StructureSet.Structures.FirstOrDefault(e => e.Id == "CouchInterior");
            //Structure CouchSurface = StructureSet.Structures.FirstOrDefault(e => e.Id == "CouchSurface");
            //PatientOrientation orientation = ScriptContext.Image.ImagingOrientation;
            ////IReadOnlyList<Structure> couchStructureList = new List<Structure>();
            //bool imageResized = true;
            //string errorCouch = "error";
            //ScriptContext.Patient.BeginModifications();
            //if (CouchInterior == null && CouchSurface == null)
            //{
            //    //bool IsAddCouch = StructureSet.AddCouchStructures("Exact IGRT Couch, medium", orientation, 0, 0, -300, -1000, null, out addedStructures, out imageResized, out error);
            //    StructureSet.AddCouchStructures("Exact IGRT Couch, medium", orientation, RailPosition.In, RailPosition.In, null, null, null, out IReadOnlyList<Structure> couchStructureList, out imageResized, out errorCouch);
            //}
            //else
            //{
            //    ScriptContext.StructureSet.RemoveStructure(CouchInterior);
            //    ScriptContext.StructureSet.RemoveStructure(CouchSurface);
            //    StructureSet.AddCouchStructures("Exact IGRT Couch, medium", PatientOrientation.HeadFirstSupine, RailPosition.In, RailPosition.In, null, null, null, out IReadOnlyList<Structure> couchStructureList, out imageResized, out errorCouch);
            //}
            ////MessageBox.Show(ScriptContext.IonPlanSetup.PatientSupportDevice.ReadXml(XmlReader)) ;
            ////MessageBox.Show("ButtonCommand_AddCouch");

        }
        public ICommand ButtonCommand_FilePath { get => new Command(GetFilePath); }
        private void GetFilePath()
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "Please Choose the input Foler for Couch Model";
            FilePath = string.Empty;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FilePath = dialog.SelectedPath;
                string[] PathCI = new string[] { dialog.SelectedPath, "CouchInterior.csv" };
                string[] PathCS = new string[] { dialog.SelectedPath, "CouchSurface.csv" };
                string[] PathCSI = new string[] { dialog.SelectedPath, "CrossInterior.csv" };
                string[] PathCSS = new string[] { dialog.SelectedPath, "CrossSurface.csv" };
                FilePathCI = System.IO.Path.Combine(PathCI);
                FilePathCS = System.IO.Path.Combine(PathCS);
                FilePathCSI = System.IO.Path.Combine(PathCSI);
                FilePathCSS = System.IO.Path.Combine(PathCSS);
            }
        }


        public ICommand ButtonCommand_BuildModel { get => new Command(BuildModel); }
        private void BuildModel()
        {
            Structure CouchInterior = StructureSet.Structures.FirstOrDefault(e => e.Id == "CouchInterior");
            Structure CouchSurface = StructureSet.Structures.FirstOrDefault(e => e.Id == "CouchSurface");
            Structure CrossInterior = StructureSet.Structures.FirstOrDefault(e => e.Id == "CrossInterior");
            Structure CrossSurface = StructureSet.Structures.FirstOrDefault(e => e.Id == "CrossSurface");
            if (CouchInterior != null)
            {
                for (int i = 0; i < ScriptContext.Image.ZSize; i++)
                {
                    foreach (VVector[] vectors in CouchInterior.GetContoursOnImagePlane(i))
                    {
                        using (StreamWriter writer = new StreamWriter(FilePathCI))
                        {
                            writer.WriteLine(String.Join(",", vectors.Select(v => $"{v.x}, {v.y}, {v.z}\n ")));
                        }
                    }
                }
            }
            if (CouchSurface != null)
            {
                for (int i = 0; i < ScriptContext.Image.ZSize; i++)
                {
                    foreach (VVector[] vectors in CouchSurface.GetContoursOnImagePlane(i))
                    {
                        using (StreamWriter writer = new StreamWriter(FilePathCS))
                        {
                            writer.WriteLine(String.Join(",", vectors.Select(v => $"{v.x}, {v.y}, {v.z}\n ")));
                        }
                    }
                }
            }
            if (CrossInterior != null)
            {
                for (int i = 0; i < ScriptContext.Image.ZSize; i++)
                {
                    foreach (VVector[] vectors in CrossInterior.GetContoursOnImagePlane(i))
                    {
                        using (StreamWriter writer = new StreamWriter(FilePathCSI))
                        {
                            writer.WriteLine(String.Join(",", vectors.Select(v => $"{v.x}, {v.y}\n ")));
                        }
                    }
                }
            }
            if (CrossSurface != null)
            {
                for (int i = 0; i < ScriptContext.Image.ZSize; i++)
                {
                    foreach (VVector[] vectors in CrossSurface.GetContoursOnImagePlane(i))
                    {
                        using (StreamWriter writer = new StreamWriter(FilePathCSS))
                        {
                            writer.WriteLine(String.Join(",", vectors.Select(v => $"{v.x}, {v.y}\n ")));
                        }
                    }
                }
            }
        }

        public ICommand ButtonCommand_AddCouchLine { get => new Command(AddCouchLine); }
        private void AddCouchLine()
        {
            ScriptContext.Patient.BeginModifications();
            if (StructureSet.Structures.Any(s => s.Id == "CouchLine")) StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CouchLine"));
            Structure CouchLine = StructureSet.AddStructure("CONTROL", "CouchLine");

            for (int i = 0; i < ScriptContext.Image.ZSize; i++)
            {
                CouchLine.AddContourOnImagePlane(GetpseudoLine(MarkerLocationYY), i);
            }

            //string filePath = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\contour.txt";

            //foreach (VVector vector in GetpseudoLine(MarkerLocationYY, MarkerLocationXX))
            //{
            //    using (StreamWriter writer = new StreamWriter(filePath))
            //    {
            //        writer.WriteLine(String.Join(",", $"{vector.x}, {vector.y},\n "));
            //    }
            //}
        }
        public VVector[] GetpseudoLine(double yPlane)
        {
            List<VVector> vvectors = new List<VVector>();
            vvectors.Add(new VVector(-ScriptContext.Image.XSize, yPlane, 0));
            vvectors.Add(new VVector(ScriptContext.Image.XSize, yPlane, 0));
            vvectors.Add(new VVector(ScriptContext.Image.XSize, ScriptContext.Image.YSize, 0));
            vvectors.Add(new VVector(-ScriptContext.Image.XSize, ScriptContext.Image.YSize, 0));
            return vvectors.ToArray();
        }

        public String PeakDetect(ImageProfile XProfiles)
        {
            VVector[] Twopoint = null;
            int a = 0;
            
            for (int i = 0; i < XProfile.Count ; i++) 
            {
                if(XProfile[i].Value > (XProfiles.Max().Value*0.5))
                {
                    if (XProfile[i].Value > XProfile[i-1].Value && XProfile[i].Value > XProfile[i - 1].Value) 
                    {
                        Twopoint[a] = XProfile[i].Position ;
                        a++;
                    }
                }
            }
            double distance = VVector.Distance(Twopoint[0], Twopoint[1]);
            if( Math.Round(distance) > 0.5)
            {
                var map = new Dictionary<int, string>()
                {
                    {1, "H1"},
                    {2, "H2"},
                    {3, "H3"},
                    {4, "H4"},
                    {5, "H5"},
                };
                string output;
                return map.TryGetValue(Convert.ToInt32(Math.Round(distance)), out output) ? output : null;
            }
            else return "0";

        }
        public double[] AxisAlignment(string LockBarType, double Xmin, double Ymin, double Zmin)
        {
            Double HSpace = 140;//mm
            var mapAxis = new Dictionary<string, double[] >()
            {
                {"0",   new double[] { MarkerLocationXX - 257.8 - Xmin, MarkerLocationYY - 94.5 - Ymin, MarkerLocationZZ - 40.2 - 3*HSpace - Zmin }},
                {"H1",  new double[] { MarkerLocationXX - 257.8 - Xmin, MarkerLocationYY - 94.5 - Ymin, MarkerLocationZZ - 40.2 - 2*HSpace - Zmin }},
                {"H2",  new double[] { MarkerLocationXX - 257.8 - Xmin, MarkerLocationYY - 94.5 - Ymin, MarkerLocationZZ - 40.2 - HSpace - Zmin }},
                {"H3",  new double[] { MarkerLocationXX - 257.8 - Xmin, MarkerLocationYY - 94.5 - Ymin, MarkerLocationZZ - 40.2 - Zmin }},
                {"H4",  new double[] { MarkerLocationXX - 257.8 - Xmin, MarkerLocationYY - 94.5 - Ymin, MarkerLocationZZ - 40.2 + HSpace - Zmin }},
                {"H5",  new double[] { MarkerLocationXX - 257.8 - Xmin, MarkerLocationYY - 94.5 - Ymin, MarkerLocationZZ - 40.2 + 2*HSpace - Zmin }},
            };
            double[] output;
            return mapAxis.TryGetValue(SelectedMarkerPosition, out output) ? output : null;
        }
    }
}
