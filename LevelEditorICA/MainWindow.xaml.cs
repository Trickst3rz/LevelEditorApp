﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using DLL_DBWorker.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;

namespace LevelEditorICA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private MapCell[,] map = new MapCell[5, 5];

        private BitmapImage activeTexture;
        private BitmapImage secondTexture;

        #region Implementation data

        private string m_workingDirectory = "";                                       //!< The working directory for default images.
        private string m_terrainInit = "";
        string sFilenames = "";
        private bool Collidable = false;
        private List<Image> Collectionlist = new List<Image>();
        private List<Image> CanvasList = new List<Image>();
        private List<int> mapIndex = new List<int>();
       
        //private LevelGrid m_canvas = null;

        #endregion

        public BitmapImage ActiveTexture
        {
            get { return activeTexture; }
            set { activeTexture = value; }
        }
        public BitmapImage SecondTexture
        {
            get { return secondTexture; }
            set { secondTexture = value; }
        }

        public MainWindow()
        {
            Width = 400;
            for (int i = 0; i < gridHeight; i++)
                for (int j = 0; j < gridWidth; j++)
                {
                    this.map[i, j] = new MapCell();
                    this.map[i, j].X0 = j * 32;
                    this.map[i, j].Y0 = i * 32;
                    this.map[i, j].X1 = (j + 1) * 32;
                    this.map[i, j].Y1 = (i + 1) * 32;
                }
            DataContext = this;
            InitializeComponent();

            #region IMAGES
            //////////////////////////////////////////////////////////////
            // initialise the source of tiles
            SpriteSheetList.ItemsSource = panelImages;
            // initialise the source of map's tiles
            //SpriteSheetList.ItemsSource = mapImages;
            //////////////////////////////////////////////////////////////
            #endregion

            
                
            


            mapTileCanvas.Children.Clear();
            for (int i = 0; i < gridHeight; i++)
                for (int j = 0; j < gridWidth; j++)
                {
                    Button btn = new Button();

                    Canvas can = new Canvas();

                    Rectangle rect_ = new Rectangle();
                    rect_.Width = tileSize;
                    rect_.Height = tileSize;
                    rect_.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    //mapTileCanvas.Children.Add(rect_);
                    //Canvas.SetLeft(rect_, tileSize * j + j * 1);
                    //Canvas.SetTop(rect_, tileSize * i + i * 1);





                }

        }

        // my config file
        //myConfigFile cfgFile = new myConfigFile();

        //// number of columns considered
        //// (shouldnt this be stored in the config file?!)
        //const int cols = 23;
        //// number of rows considered
        //// (shouldnt this be stored in the config file?!)
        //const int rows = 21;

        // size, in pixels, of tiles considered
        // (shouldnt this be stored in the config file?!)
        private int tileSize = 32;

        private int gridHeight = 5;
        private int gridWidth = 5;

        List<Image> images = new List<Image>();

        List<Rectangle> tiles = new List<Rectangle>();

       

        private Rectangle rect = new Rectangle();

        Image GetImg = new Image();

        // this is a special type of collection which automatically updates the UI element it is bound to 
        ObservableCollection<Image> panelImages = new ObservableCollection<Image>();
        ObservableCollection<Image> mapImages = new ObservableCollection<Image>();
        Collection<Image> collectionImages = new Collection<Image>();
        
        // drag and drop
        Point mouseOffset;
        private bool isDragging = false;

        //private int _boundNumber;
        public int BoundNumber
        {
            get { return tileSize; }
            set
            {
                if(tileSize != value)
                {
                    tileSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ShowPending
        {
            get { return this.showPending; }
            set { this.showPending = value; }
        }

        private bool showPending = true;

        private void checkBoxShowPending_CheckedChanged(object sender, EventArgs e)
        {
            //checking showPending.Value here.  It's always false
            showPending = true;
        }


        public int BoundWidth
        {
            get { return gridWidth; }
            set
            {
                if (gridWidth != value)
                {
                    gridWidth = value;
                    mapTileCanvas.Width = gridWidth * tileSize;

                    OnPropertyChanged();

                    //mapTileCanvas.Children.Clear();
                    for (int i = 0; i < gridHeight; i++)
                        for (int j = 0; j < gridWidth; j++)
                        {
                            Button btn = new Button();

                            Canvas can = new Canvas();
                            Grid grid = new Grid();
                            Rectangle rect_ = new Rectangle();
                            rect_.Width = tileSize;
                            rect_.Height = tileSize;
                            rect_.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            //mapTileCanvas.Children.Add(rect_);
                            //Canvas.SetLeft(rect_, tileSize * j + j * 1);
                            //Canvas.SetTop(rect_, tileSize * i + i * 1);

                            




                        }

                    
                    

                }
            }
        }

        public int BoundHeight
        {
            get { return gridHeight; }
            set
            {
                if (gridHeight != value)
                {
                    gridHeight = value;
                    mapTileCanvas.Height = gridHeight * tileSize;

                    OnPropertyChanged();

                    //mapTileCanvas.Children.Clear();
                    for (int i = 0; i < gridHeight; i++)
                        for (int j = 0; j < gridWidth; j++)
                        {
                            Button btn = new Button();

                            Canvas can = new Canvas();

                            Rectangle rect_ = new Rectangle();
                            rect_.Width = tileSize;
                            rect_.Height = tileSize;
                            rect_.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            //mapTileCanvas.Children.Add(rect_);
                            //Canvas.SetLeft(rect_, tileSize * j + j * 1);
                            //Canvas.SetTop(rect_, tileSize * i + i * 1);
                            
                            



                        }
                }
            }
        }

        public int SetGridHeight
        {
            get { return  gridHeight * tileSize; }
            set
            {
            }
        }

        public int SetGridWidth
        {
            get { return gridWidth * tileSize; }
            set
            {
                
            }
        }

        
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
          
        }
        Image img2 = new Image();
       
        private void DrawTexture(object sender, double x, double y)
        {
            if (SpriteSheetList.SelectedItem != null)
            {
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                    {
                        if (x >= this.map[i, j].X0 && x <= this.map[i, j].X1
                            && y >= this.map[i, j].Y0 && y <= this.map[i, j].Y1)
                        {
                            //MessageBox.Show($"({this.map[i, j].X0};{this.map[i, j].Y0}) — ({this.map[i, j].X1};{this.map[i, j].Y1})");
                            Image img = new Image();
                            Image img2 = new Image();
                            
                            //img.Source = 
                            img.Source = ((Image)SpriteSheetList.SelectedItem).Source;
                            //img2.Source = panelImages[1].Source; //how to get the index
                            img.Width = tileSize;
                            img.Height = tileSize;
                            Canvas.SetLeft(img, this.map[i, j].X0);
                            Canvas.SetTop(img, this.map[i, j].Y0);
                            Canvas.SetZIndex(img, 1);
                            this.mapTileCanvas.Children.Add(img);
                            this.CanvasList.Add(img);
                            this.mapIndex.Add(i + j + i * 4);
                            
                            break;

                            

                        }
                    }
                
            }
        }
        private void DeleteTexture(object sender, double x, double y)
        {
            if (SpriteSheetList.SelectedItem != null)
            {
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                    {
                        if (x >= this.map[i, j].X0 && x <= this.map[i, j].X1
                            && y >= this.map[i, j].Y0 && y <= this.map[i, j].Y1)
                        {
                            
                            //MessageBox.Show($"({this.map[i, j].X0};{this.map[i, j].Y0}) — ({this.map[i, j].X1};{this.map[i, j].Y1})");
                            Image img = new Image();
                            Image img2 = new Image();
                            //img.Source = 
                            img.Source = ((Image)SpriteSheetList.SelectedItem).Source;
                            img.Width = tileSize;
                            img.Height = tileSize;
                            Canvas.SetLeft(img, this.map[i, j].X0);
                            Canvas.SetTop(img, this.map[i, j].Y0);
                            Canvas.SetZIndex(img, 0); //layer
                            this.mapTileCanvas.Children.Add(img);
                            

                            break;
                        }
                    }
            }
        }

        private void btn_AddTileToMap(object sender, RoutedEventArgs e)
        {
            if (SpriteSheetList.SelectedItem != null)
            {
                //Image img = new Image();
                ////img.Source = 
                //img.Source = ((Image)SpriteSheetList.SelectedItem).Source; Height = tileSize;
                //mapTileCanvas.Children.Add(img);
                mapTileCanvas.Children.Insert(10,new Image() { Source = ((Image)SpriteSheetList.SelectedItem).Source, Height = tileSize });
                
            }
        }

        private void canvasMap_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var clickedPoint = e.GetPosition((Canvas)sender);
            double x = clickedPoint.X;
            double y = clickedPoint.Y;
            if (e.ChangedButton == MouseButton.Left)
                this.DrawTexture(sender, x, y);
            else if (e.ChangedButton == MouseButton.Right)
                this.DeleteTexture(sender, x, y);
        }
        private void canvasMap_MouseMove(object sender, MouseEventArgs e)
        {
            var clickedPoint = e.GetPosition((Canvas)sender);
            double x = clickedPoint.X;
            double y = clickedPoint.Y;
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DrawTexture(sender, x, y);
            else if (e.RightButton == MouseButtonState.Pressed)
                this.DeleteTexture(sender, x, y);
        }

        private void menuLoadTiles(object sender, RoutedEventArgs e)
        {
            /*if (gridMapTiles != null)
            {
                
                gridMapTiles.Children.Clear();
                gridMapTiles.RowDefinitions.Clear();
                gridMapTiles.ColumnDefinitions.Clear();
                gridMapTiles.ShowGridLines = showPending;

                //if (mapImages.Count < 1)
                //{
                    // images for sprites
                    //BtnOpenFile_Click();
                    //createTilesListOnCanvas();

                    //int gridHeight = int.Parse(MapHeight.Text);
                    //int gridWidth = int.Parse(MapWidth.Text);
                    for (int i = 0; i < gridHeight; i++)
                        gridMapTiles.RowDefinitions.Add(
                            new RowDefinition()
                            { Height = new GridLength(tileSize) }
                          );
                    for (int i = 0; i < gridWidth; i++)
                        gridMapTiles.ColumnDefinitions.Add(new ColumnDefinition()
                        { Width = new GridLength(tileSize) });

                    for (int i = 0; i < gridHeight; i++)
                        for (int j = 0; j < gridWidth; j++)
                        {
                            Button btn = new Button();

                            Canvas can = new Canvas();

                            if (gridWidth < j)
                                gridMapTiles.Children.Remove(can);

                        Grid.SetRow(can, i);
                        Grid.SetColumn(can, j);
                        gridMapTiles.Children.Add(can);*/
                        /*Rectangle rect_ = new Rectangle();
                        rect_.Width = tileSize;
                        rect_.Height = tileSize;
                        rect_.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                        mapTileCanvas.Children.Add(rect_);
                        Canvas.SetLeft(rect_, gridWidth * j + j * 1);
                        Canvas.SetTop(rect_, gridHeight * i + i * 1);*/

                    //}
                
                //}

            //}
        }

        private bool LoadLevel()
        {
            // Create the dialog box.
            OpenFileDialog openBox = new OpenFileDialog();
            openBox.Filter = "XML Files|*.xml";

            // Proceed with saving if necessary.
            if (openBox.ShowDialog() == true)
            {
                try
                {
                    // Attempt to load the XML.
                    XDocument xml = XDocument.Load(openBox.FileName);

                    if (CreateFromXML(xml))
                    {
                        return true;
                    }

                    //throw new FileFormatException("Unable to load from XML, likely a corrupt file.");
                }

                catch (Exception error)
                {
                    string message = "An error occurred whilst attempting to load.\nError details: " + error.Message;
                    MessageBox.Show(this, message, "Error", MessageBoxButton.OK);
                }
            }

            return false;
        }

        private void BtnLoadXML_click(object sender, RoutedEventArgs e)
        {
            LoadLevel();

            Image newimg = new Image();

            Image newimg2 = new Image();

            mapTileCanvas.Children.Clear();

            newimg.Source = panelImages[5].Source;

           
            for (int i = 0; i < gridHeight; i++)
                for (int j = 0; j < gridWidth; j++)
                {
                    Image img = new Image();
                   
                    //img.Source = 
                    //img.Source = panelImages[2].Source;
                    img.Width = tileSize;
                    img.Height = tileSize;
                    Canvas.SetLeft(img, tileSize * j + j * 1);
                    Canvas.SetTop(img, tileSize * i + i * 1);
                    Canvas.SetZIndex(img, 0); //layer
                    mapTileCanvas.Children.Add(img);
                }
            newimg.Width = tileSize;
            newimg.Height = tileSize;
            Canvas.SetLeft(newimg, tileSize);
            Canvas.SetTop(newimg, tileSize);
            mapTileCanvas.Children.Insert(7, newimg);
            
            //mapTileCanvas.Children.Add(newimg);

            //mapTileCanvas.Children.Add(newimg);
            //mapTileCanvas.Children.Insert(0, newimg);
            //Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            ////openFileDialog.InitialDirectory = "c://";
            //openFileDialog.FileName = "";
            //openFileDialog.DefaultExt = ".XML";
            //openFileDialog.Filter = "XML files (.XML)|*.XML";
            //openFileDialog.RestoreDirectory = true;
            //Nullable<bool> result = openFileDialog.ShowDialog();

            //if (result.HasValue && result.Value)
            //{

            //    string sFilenames = "";

            //    foreach (string sFilename in openFileDialog.FileNames)
            //    {
            //        sFilenames += ";" + sFilename;
            //    }
            //    sFilenames = sFilenames.Substring(1);
            //}

        }

        /// <summary>
        /// The save event which writes all data to storage.
        /// </summary>
        private void Menu_SaveClick(object sender, RoutedEventArgs e)
        {
            SaveToXML();
            
        }

        private bool SaveToXML()
        {
            //Create a dialog box.
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML Files|*.xml";

            //Process with saving if necessary.
            if(saveFile.ShowDialog() == true)
            {
                try
                {
                    //Attempt to save the XML.
                    XDocument xml = GenerateXML();
                    xml.Save(saveFile.FileName);
                    return true;
                }

                catch(Exception error)
                {
                    string message = "An error occurred attempting to save:" + error.Message;
                    MessageBox.Show(this, message, "Error", MessageBoxButton.OK);
                }
            }
            return false;
        }

        public bool CreateFromXML(XDocument xml)
        {
            // Attempt the loading process.
            try
            {
                // Obtain the working set of data.
                XElement root = xml.Root;
                UpdateDimensions(Convert.ToInt16(root.Attribute("Width").Value), Convert.ToInt16(root.Attribute("Height").Value));
                

                XElement gameTiles = root.Element("GameTiles");
                SetCanvas(Convert.ToInt16(gameTiles.Attribute("MapIndex").Value), Convert.ToInt16(gameTiles.Attribute("ImageIndex").Value));

                // Attempt to construct the grid.
                //ClearData();
                //FillFromXElement(root.Element("GameTiles"));

                //UpdateGridVisuals();

                return true; // ?
            }

            catch (Exception)
            {
                // Take care of illegal data by simply clearing it.
                //ResetToSafeValues();
                ClearData();
                //UpdateGridVisuals();
            }

            return false;
        }

        private void SetCanvas(int MapIndex, int ImageIndex)
        {
            Image newimg = new Image();

            newimg.Source = panelImages[ImageIndex].Source;

            mapTileCanvas.Children.Insert(MapIndex, newimg);

        }

        private void UpdateDimensions(int newWidth, int newHeight)
        {
            gridWidth = newWidth;
            gridHeight = newHeight;
            
        }

        private void ClearData()
        {
            //m_data.Clear();
            //m_images.Clear();

            //mapTileCanvas.Children.Clear();
            //m_grid.ColumnDefinitions.Clear();
            //m_grid.RowDefinitions.Clear();
        }

        public XDocument GenerateXML()
        {
            // Create the document and add the grid parameters.
            XDocument xml = new XDocument();

            XElement canvas = new XElement("Canvas",
                                            new XAttribute("Width", gridWidth), new XAttribute("Height", gridHeight));

            // Traverse the deep maze of game tiles producing the XML.
            //XElement gameTiles = new XElement("GameTiles", new XAttribute("Sprite", sFilenames), new XAttribute(");


            //XElement gameTiles = new XElement("GameTiles");
            //XElement gameTiles = new XElement("GameTiles");


            CanvasList.Count();





            //for (int k = 0; mapTileCanvas.Children[k] == null; k++)
            //{
            //    XElement gameTiles2 = new XElement("GameTiles");
            //    canvas.Add(gameTiles2);
            //}



            // Traverse the deep maze of game tiles producing the XML.
            for (int i = 0; i < CanvasList.Count(); i++)
            {
                XElement gameTiles = new XElement("GameTiles", new XAttribute("Sprite", sFilenames), new XAttribute("MapIndex", mapIndex[i]),
                   new XAttribute("ImageIndex", 1), new XAttribute("IsCollidable", Collidable), new XAttribute("Layer", 0));

                canvas.Add(gameTiles);

            } 


            //foreach (UIElement element in mapTileCanvas.Children)
            //{
            //    XElement gameTiles2 = new XElement("GameTiles");
            //    canvas.Add(gameTiles2);
            //}



            //}

            //foreach (Nullable in mapTileCanvas.Children)
            //{

            //    gameTiles.c(gameTile);
            //}

            // Finally construct the XML document, ready to return.

            //xml.Add(canvas);

            //canvas.Add(gameTiles);
            xml.Add(canvas);

            return xml;
        }

        private void Mouse_overCanvas(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition((UIElement) sender);

            //Rectangle rect_ = new Rectangle();
            //rect_.Width = tileSize;
            //rect_.Height = tileSize;
            //rect_.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            //SpriteSheetList.Children.RemoveAt(0);
            //SpriteSheetList.Children.Insert(1, rect);
            Image newImg = new Image();
            //mapTileCanvas.Children.Add(new Image() { Source = ((Image)newImg).Source, Height = tileSize});
            
            //images.Insert(1, newImg);

            //mapTileCanvas.Children.Add()
            
        }
            private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            //openFileDialog.InitialDirectory = "c://";
            openFileDialog.FileName = "";
            openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "Image documents (.png)|*.png";
            openFileDialog.RestoreDirectory = true;
            Nullable<bool> result = openFileDialog.ShowDialog();
           
            if (result.HasValue && result.Value)
            {

                

                foreach (string sFilename in openFileDialog.FileNames)
                {
                    sFilenames += ";" + sFilename;
                }
                sFilenames = sFilenames.Substring(1);
                
                BitmapImage bitmap = new BitmapImage(new Uri(sFilenames,UriKind.Relative));

                SpriteSheetList.Height = bitmap.PixelHeight;
                SpriteSheetList.Width = bitmap.PixelWidth;
                string filename = openFileDialog.FileName;
                for (int i = 0; i < bitmap.PixelHeight / tileSize; i++)
                    for (int j = 0; j < bitmap.PixelWidth / tileSize; j++)
                    {
                        panelImages.Add(new Image()
                        {
                           
                            Source = new CroppedBitmap(bitmap,
                                                 new Int32Rect(j * tileSize, i * tileSize, tileSize, tileSize)),
                            Height = tileSize
                        });

                        Collectionlist.Add(new Image()
                        {

                            Source = new CroppedBitmap(bitmap,
                                                 new Int32Rect(j * tileSize, i * tileSize, tileSize, tileSize)),
                            Height = tileSize
                        });

                        SpriteSheetList.ItemsSource = new ObservableCollection<Image>(panelImages);
                        
                        
                        //Canvas.SetTop(newImg, i * tileSize);
                       // Canvas.SetLeft(newImg, j * tileSize); 
                        //images.Add(newImg);
                        //newImg.MouseLeftButtonDown += Image_MouseDown;

                        //GetImg = newImg;
                        
                        
                        //panelImages.Last().MouseLeftButtonDown += Image_MouseDown;
                        //collectionImages.Add(newImg);
                    }
                //SpriteSheetList.ItemsSource = new ObservableCollection<Image>(panelImages);
               
                

            }

        }

       

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Dragging mode begins
            isDragging = true;
            Image img = (Image)sender;

            // Get the position of the click relative to the image
            // so the top-left corner of the image is (0,0)
            mouseOffset = e.GetPosition(img);

            // Watch this image for more mouse events
            img.MouseMove += Image_MouseMove;
            img.MouseLeftButtonUp += Image_MouseUp;

            // Capture the mouse. This way you'll keep receiving
            // the MouseMove event even if the user jerks the mouse
            // off the image
            img.CaptureMouse();
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Image img = (Image)sender;

                // Get the position of the image relative to the Canvas
                Point point = e.GetPosition(SpriteSheetList);

                img.SetValue(Canvas.TopProperty, point.Y - mouseOffset.Y);
                img.SetValue(Canvas.LeftProperty, point.X - mouseOffset.X);
            }
        }
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                Image img = (Image)sender;

                // Don't watch the mouse events any longer
                img.MouseMove -= Image_MouseMove;
                img.MouseLeftButtonUp -= Image_MouseUp;
                img.ReleaseMouseCapture();

                isDragging = false;
            }
        }

        private void ExitEditorCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void menuLoadTiles(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        // add the selected tile to the map
        //private void btn_AddTileToMap(object sender, RoutedEventArgs e)
        //{
        //    if (SpriteSheetList.SelectedItem != null)
        //    {
        //        mapImages.Add(new Image() { Source = ((Image)SpriteSheetList.SelectedItem).Source, Height = tileSize });

        //    }
        //}


    }
}
