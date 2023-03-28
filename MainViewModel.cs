using System;
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
using System.Reflection;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using System.Text.RegularExpressions;
using System.Windows.Media;
using static System.Windows.Forms.LinkLabel;

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

        private String _filePathpoint;
        public String FilePathCI_point
        {
            get => _filePathpoint;
            set
            {
                _filePathpoint = value;
                NotifyPropertyChanged(nameof(FilePathCI_point));
            }
        }

        private String _filePathCIvector;
        public String FilePathCI_vector
        {
            get => _filePathCIvector;
            set
            {
                _filePathCIvector = value;
                NotifyPropertyChanged(nameof(FilePathCI_vector));
            }
        }

        private String _filePathCITI;
        public String FilePathCI_TI
        {
            get => _filePathCITI;
            set
            {
                _filePathCITI = value;
                NotifyPropertyChanged(nameof(FilePathCI_TI));
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

        private String _filePathAxis;
        public String FilePathBasic
        {
            get => _filePathAxis;
            set
            {
                _filePathAxis = value;
                NotifyPropertyChanged(nameof(FilePathBasic));
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
       
        private Structure _couchInterior;
        public Structure CouchInterior
        {
            get => _couchInterior;
            set
            {
                _couchInterior = value;
                NotifyPropertyChanged(nameof(CouchInterior));
            }
        }

        private Structure _couchSurface;
        public Structure CouchSurface
        {
            get => _couchSurface;
            set
            {
                _couchSurface = value;
                NotifyPropertyChanged(nameof(CouchSurface));
            }
        }

        private Structure _crossInterior;
        public Structure CrossInterior
        {
            get => _crossInterior;
            set
            {
                _crossInterior = value;
                NotifyPropertyChanged(nameof(CrossInterior));
            }
        }

        private Structure _crossSurface;
        public Structure CrossSurface
        {
            get => _crossSurface;
            set
            {
                _crossSurface = value;
                NotifyPropertyChanged(nameof(CrossSurface));
            }
        }
        public IReadOnlyList<Structure> couchStructureList { get; }
        public double HSpace { get; set; }
        public double XBaseAxis { get; set; }
        public double YBaseAxis { get; set; }
        public double ZBaseAxis { get; set; }

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

            var MulMarker = StructureSet.Structures.Where(s => s.DicomType == "MARKER").ToList();
            if (MulMarker.Count > 0)
            {
                MarkerNames = new List<String>();
                foreach (Structure Iso in MulMarker)
                {
                    MarkerNames.Add(Iso.Id);
                }
                PositionRenew();
            }
            else throw new Exception("There is no marker.  At least one marker DICOM type is required.");

            FilePathCI = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\CouchInterior.csv";
            FilePathCI_point = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\CouchInterior.csv";
            FilePathCI_vector = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\CouchInterior.csv";
            FilePathCI_TI = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\CouchInterior.csv";

            FilePathCS = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\CouchSurface.csv";
            FilePathCSI = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\CrossInterior.csv";
            FilePathCSS = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\CrossSurface.csv";
            FilePathBasic = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\BasicInformation.csv";
            CouchInterior = StructureSet.Structures.FirstOrDefault(e => e.Id == "CouchInterior");
            CouchSurface = StructureSet.Structures.FirstOrDefault(e => e.Id == "CouchSurface");
            CrossInterior = StructureSet.Structures.FirstOrDefault(e => e.Id == "CrossInterior");
            CrossSurface = StructureSet.Structures.FirstOrDefault(e => e.Id == "CrossSurface");

            double AA_Z = MarkerLocationZZ - ZBaseAxis;

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
                //this is the value of finding z locztion corresponded to z slice
                //MessageBox.Show(Convert.ToInt32((MarkerLocationZZ-ScriptContext.Image.Origin.z)/ ScriptContext.Image.ZRes).ToString());

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

            if (!File.Exists(filePathOuter))
            {
                System.Windows.MessageBox.Show($"No file exists at path {filePathOuter}");
                return;
            }

            ScriptContext.Patient.BeginModifications();
            if (StructureSet.Structures.Any(s => s.Id == "XYZ")) StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "XYZ"));
            Structure XYZ = ScriptContext.StructureSet.AddStructure("CONTROL", "XYZ");
            //MeshGeometry3D mymesh = new MeshGeometry3D();
            //XYZ.MeshGeometry.Positions.Add(new Point3D(-1, -1, 0));
            //XYZ.MeshGeometry.Positions.Add(new Point3D(1, -1, 0));
            //XYZ.MeshGeometry.Positions.Add(new Point3D(-1, 1, 0));
            //XYZ.MeshGeometry.Positions.Add(new Point3D(1, 1, 0));

            //XYZ.MeshGeometry.TriangleIndices.Add(0);
            //XYZ.MeshGeometry.TriangleIndices.Add(1);
            //XYZ.MeshGeometry.TriangleIndices.Add(2);
            //XYZ.MeshGeometry.TriangleIndices.Add(2);
            //XYZ.MeshGeometry.TriangleIndices.Add(3);
            //XYZ.MeshGeometry.TriangleIndices.Add(0);

            //XYZ.MeshGeometry = GetModel(20, new Vector3D(-1, -1, 0), new Point3D(-1, -1, 0), 10, 150, 250);

            // add Mesh from here
            //Positions = "-1 -1 0  1 -1 0  -1 1 0  1 1 0";
            //Normals = "0 0 1  0 0 1  0 0 1  0 0 1";
            //TextureCoordinates = "0 1  1 1  0 0  1 0   ";
            //TriangleIndices = "0 1 2  1 3 2";


            //string[] filelines = File.ReadAllLines(FilePathCI_point);
            //try
            //{
            //    foreach (string line in filelines)
            //    {
            //        string[] splitLine = line.Split(',');
            //        double x = Double.Parse(splitLine[0].Trim());
            //        double y = Double.Parse(splitLine[1].Trim());
            //        double z = Double.Parse(splitLine[2].Trim());
            //        XYZ.MeshGeometry.Positions.Add(new Point3D(x, y, z));
            //    }
            //}
            //catch
            //{
            //    System.Windows.MessageBox.Show("There was an error when reading the file.  Please make sure that all rows are in the form: number, number, number");
            //    return;
            //}

            string[] filelines2 = File.ReadAllLines(FilePathCS);
            List<VVector> outer1 = new List<VVector>(); 
            {
                foreach (string line in filelines2)
                {
                    string[] splitLine = line.Split(',');
                    double x = Double.Parse(splitLine[0].Trim());
                    double y = Double.Parse(splitLine[1].Trim());
                    double z = Double.Parse(splitLine[2].Trim());
                    outer1.Add(new VVector(x, y, z));
                }
            }
            //string[] filelines3 = File.ReadAllLines(FilePathCI_TI);
            //{
            //    foreach (string line in filelines3)
            //    {
            //        Int32 x = Int32.Parse(line.Trim());
            //        XYZ.MeshGeometry.TriangleIndices.Add(x);
            //    }
            //}

            ////double BasicY1 = Double.Parse(File.ReadLines(FilePathBasic).Skip(1).Take(1).First());
            string[] Basiclines = File.ReadAllLines(FilePathBasic);
            List<string> sourceAxis = Basiclines[1].Trim().Split(',').Select(s => s.Trim()).ToList();
            HSpace = Double.Parse(sourceAxis[0]);
            XBaseAxis = Double.Parse(sourceAxis[1]);
            YBaseAxis = Double.Parse(sourceAxis[2]);
            ZBaseAxis = Double.Parse(sourceAxis[3]);

            if (StructureSet.Structures.Any(s => s.Id == "PQR")) StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "PQR"));
            Structure PQR = ScriptContext.StructureSet.AddStructure("CONTROL", "PQR");
            double SSXAdd = MarkerLocationXX - XBaseAxis - MaxMinDetect(outer1)[0];
            double SSYAdd = MarkerLocationYY + YBaseAxis - MaxMinDetect(outer1)[1];
            double SSZAdd = Convert.ToInt32((MarkerLocationZZ - ZBaseAxis - ScriptContext.Image.Origin.z)/ScriptContext.Image.ZRes);
            for (int i = 0; i < SSZAdd; i++)
            {
                PQR.AddContourOnImagePlane(outer1.Select(v => new VVector(v.x + SSXAdd, v.y + SSYAdd, v.z)).ToArray(), i);
            }

            //double[] AddAxis = AxisAlignment(SelectedMarkerPosition, Xmin, Ymin, Zmin);


            //Structure ABC = StructureSet.Structures.FirstOrDefault(e => e.Id == "ABC");
            //Structure DEF = StructureSet.Structures.FirstOrDefault(e => e.Id == "DEF");
            //ABC.SegmentVolume = ABC.SegmentVolume.Sub(DEF.SegmentVolume);


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
                string[] PathCI_point = new string[] { dialog.SelectedPath, "CouchInterior_point.csv" };
                string[] PathCI_vector = new string[] { dialog.SelectedPath, "CouchInterior_vector.csv" };
                string[] PathCI_TI= new string[] { dialog.SelectedPath, "CouchInterior_TI.csv" };
                string[] PathCS = new string[] { dialog.SelectedPath, "CouchSurface.csv" };
                string[] PathCSI = new string[] { dialog.SelectedPath, "CrossInterior.csv" };
                string[] PathCSS = new string[] { dialog.SelectedPath, "CrossSurface.csv" };
                string[] PathBasic = new string[] { dialog.SelectedPath, "BasicInformation.csv" };
                FilePathCI = System.IO.Path.Combine(PathCI);
                FilePathCI_point = System.IO.Path.Combine(PathCI_point);
                FilePathCI_vector = System.IO.Path.Combine(PathCI_vector);
                FilePathCI_TI = System.IO.Path.Combine(PathCI_TI);
                FilePathCS = System.IO.Path.Combine(PathCS);
                FilePathCSI = System.IO.Path.Combine(PathCSI);
                FilePathCSS = System.IO.Path.Combine(PathCSS);
                FilePathBasic = System.IO.Path.Combine(PathBasic);
            }
        }


        public ICommand ButtonCommand_BuildModel { get => new Command(BuildModel); }
        private void BuildModel()
        {
            if (CouchInterior != null)
            {
                Point3DCollection Points = CouchInterior.MeshGeometry.Positions;
                Vector3DCollection Normals = CouchInterior.MeshGeometry.Normals;
                PointCollection TextureCoordinate = CouchInterior.MeshGeometry.TextureCoordinates;
                System.Windows.Media.Int32Collection TriangleIndices = CouchInterior.MeshGeometry.TriangleIndices;
                using (StreamWriter writer = new StreamWriter(FilePathCI_point))
                {
                    foreach (Point3D v in Points) writer.WriteLine(v.X + "," + v.Y + "," + v.Z);
                    //writer.WriteLine();
                }
                using (StreamWriter writer = new StreamWriter(FilePathCI_vector))
                {
                    foreach (Vector3D vv in Normals) writer.WriteLine(vv.X + "," + vv.Y + "," + vv.Z);

                }
                using (StreamWriter writer = new StreamWriter(FilePathCI_TI))
                {
                    foreach (Int32 t in TriangleIndices) writer.WriteLine(t);
                }
                using (StreamWriter writer = new StreamWriter(FilePathCI))
                {
                    foreach (Point p in TextureCoordinate) writer.WriteLine(p.X + "," + p.Y );
                }

                //Positions = "-1 -1 0  1 -1 0  -1 1 0  1 1 0"
                //Normals = "0 0 1  0 0 1  0 0 1  0 0 1"
                //TextureCoordinates = "0 1  1 1  0 0  1 0   "
                //TriangleIndices = "0 1 2  1 3 2" 
            }
            if (CouchSurface != null)
            {
                //VVector contours = CouchSurface.MeshGeometry.Points.Select(e => new VVector(e.x, e.y, e.z));
                using (StreamWriter writer = new StreamWriter(FilePathCS))
                {
                    for (int i = 0; i < ScriptContext.Image.ZSize; i++)
                    {
                        foreach (VVector[] vectors in CouchSurface.GetContoursOnImagePlane(i))
                        {
                            foreach(VVector vector in vectors) writer.WriteLine(vector.x + "," + vector.y + "," + vector.z);
                        }
                    }
                }
            }
            if (CrossInterior != null)
            {
                using (StreamWriter writer = new StreamWriter(FilePathCSI))
                {
                    for (int i = 0; i < ScriptContext.Image.ZSize; i++)
                    {
                        foreach (VVector[] vectors in CrossInterior.GetContoursOnImagePlane(i))
                        {
                            foreach (VVector vector in vectors) writer.WriteLine(vector.x + "," + vector.y + "," + vector.z);
                            //writer.WriteLine(String.Join(",", vectors.Select(v => $"{v.x}, {v.y}, {v.z}\n ")));
                        }
                    }
                }
            }
            if (CrossSurface != null)
            {
                using (StreamWriter writer = new StreamWriter(FilePathCSS))
                {
                    for (int i = 0; i < ScriptContext.Image.ZSize; i++)
                    {
                        foreach (VVector[] vectors in CrossSurface.GetContoursOnImagePlane(i))
                        {
                            foreach (VVector vector in vectors) writer.WriteLine(vector.x + "," + vector.y + "," + vector.z);
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

            for (int i = 0; i < XProfile.Count; i++)
            {
                if (XProfile[i].Value > (XProfiles.Max().Value * 0.5))
                {
                    if (XProfile[i].Value > XProfile[i - 1].Value && XProfile[i].Value > XProfile[i - 1].Value)
                    {
                        Twopoint[a] = XProfile[i].Position;
                        a++;
                    }
                }
            }
            double distance = VVector.Distance(Twopoint[0], Twopoint[1]);
            if (Math.Round(distance) > 0.5)
            {
                var map = new Dictionary<int, string>()
                {
                    {0, "0"},
                    {1, "H1"},
                    {2, "H2"},
                    {3, "H3"},
                    {4, "H4"},
                    {5, "H5"},
                };
                string output;
                return map.TryGetValue(Convert.ToInt32(Math.Round(distance)), out output) ? output : null;
            }
            else return "";

        }
        public double[] AxisAlignment(string LockBarType, double Xmin, double Ymax, double Zmin)
        {
            //The value of YAxis is opposite
            double X = MarkerLocationXX - XBaseAxis - Xmin;
            double Y = -(MarkerLocationYY - YBaseAxis - Ymax);
            double Z = MarkerLocationZZ - ZBaseAxis;
            var mapAxis = new Dictionary<string, double[]>()
            {
                {"0",   new double[] { X, Y, Z - 3*HSpace - Zmin }},
                {"H1",  new double[] { X, Y, Z - 2*HSpace - Zmin }},
                {"H2",  new double[] { X, Y, Z - HSpace - Zmin }},
                {"H3",  new double[] { X, Y, Z - Zmin }},
                {"H4",  new double[] { X, Y, Z + HSpace - Zmin }},
                {"H5",  new double[] { X, Y, Z + 2*HSpace - Zmin }},
            };
            double[] output;
            return mapAxis.TryGetValue(SelectedMarkerPosition, out output) ? output : null;
        }
        public double[] MaxMinDetect(List<VVector> VVectors)
        {
            double[] Final = { VVectors[0].x, VVectors[0].y, VVectors[0].z };
            for (int i = 1; i < VVectors.Count(); i++)
            {
                Final[0] = Math.Min(VVectors[i].x, Final[0]); //Always get the maximum value
                Final[1] = Math.Max(VVectors[i].y, Final[1]);
                Final[2] = Math.Max(VVectors[i].y, Final[2]);
            }
            return Final;
        }

        private MeshGeometry3D GetModel(double radius, Vector3D normal, Point3D center, int resolution, double StartAngle, double EndAngle)
        {
            var geo = new MeshGeometry3D();

            // Generate the circle in the XZ-plane
            // Add the center first
            geo.Positions.Add(new Point3D(0, 0, 0));

            // Iterate from angle 0 to 2*PI
            double dev = (2 * Math.PI) / resolution;
            double thik = 0.02;
            //float spaceangle = StartAngle + 1;
            if (StartAngle != EndAngle)
            {
                for (double i = StartAngle; i < EndAngle; i += dev)
                {
                    geo.Positions.Add(new Point3D(radius * Math.Cos(i), 0, -radius * Math.Sin(i)));
                    geo.Positions.Add(new Point3D((radius - thik) * Math.Cos(i), 0, (-(radius - thik)) * Math.Sin(i)));
                }


                for (int i = 3; i < geo.Positions.Count; i += 1)
                {
                    geo.TriangleIndices.Add(i - 3);
                    geo.TriangleIndices.Add(i - 1);
                    geo.TriangleIndices.Add(i - 2);

                    geo.TriangleIndices.Add(i - 1);
                    geo.TriangleIndices.Add(i);
                    geo.TriangleIndices.Add(i - 2);
                }
            }


            // Create transforms
            var trn = new Transform3DGroup();
            // Up Vector (normal for XZ-plane)
            var up = new Vector3D(0, 1, 0);
            // Set normal length to 1
            normal.Normalize();
            var axis = Vector3D.CrossProduct(up, normal); // Cross product is rotation axis
            var angle = Vector3D.AngleBetween(up, normal); // Angle to rotate
            trn.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(axis, angle)));
            trn.Children.Add(new TranslateTransform3D(new Vector3D(center.X, center.Y, center.Z)));

            return geo;
        }
    }
}
