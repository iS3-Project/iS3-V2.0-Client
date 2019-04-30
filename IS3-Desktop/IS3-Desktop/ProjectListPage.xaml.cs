//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;
//using System.IO;
//using System.Windows.Markup;
//using System.Diagnostics;

//using System.Reflection;

//using Esri.ArcGISRuntime.Layers;
//using Esri.ArcGISRuntime.Geometry;
//using Esri.ArcGISRuntime.Symbology;

//using iS3.Core;

//namespace iS3.Desktop
//{
//    //************************  Notice  **********************************
//    //** This file is part of iS3
//    //**
//    //** Copyright (c) 2015 Tongji University iS3 Team. All rights reserved.
//    //**
//    //** This library is free software; you can redistribute it and/or
//    //** modify it under the terms of the GNU Lesser General Public
//    //** License as published by the Free Software Foundation; either
//    //** version 3 of the License, or (at your option) any later version.
//    //**
//    //** This library is distributed in the hope that it will be useful,
//    //** but WITHOUT ANY WARRANTY; without even the implied warranty of
//    //** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    //** Lesser General Public License for more details.
//    //**
//    //** In addition, as a special exception,  that plugins developed for iS3,
//    //** are allowed to remain closed sourced and can be distributed under any license .
//    //** These rights are included in the file LGPL_EXCEPTION.txt in this package.
//    //**
//    //**************************************************************************

//    public partial class ProjectListPage : UserControl
//    {
//        static ProjectList Projects;
//        string file = "ProjectList.xml";
//        PictureMarkerSymbol _pinMarkerSymbol;
//        private bool _isHitTesting;

//        public ProjectListPage()
//        {
//            InitializeComponent();

//            ProjectTitle.Text = "";

//            MyMapView.Loaded += MyMapView_Loaded;

//            MyMapView.MouseMove += MyMapView_MouseMove;
//            MyMapView.MouseDown += MyMapView_MouseDown;
//            MyMapView.NavigationCompleted += MyMapView_NavigationCompleted;
//            _isHitTesting = true;

//            InitializePictureMarkerSymbol();
//        }

//        private async void InitializePictureMarkerSymbol()
//        {
//            _pinMarkerSymbol = LayoutRoot.Resources["DefaultMarkerSymbol"]
//                as PictureMarkerSymbol;

//            try
//            {
//                await _pinMarkerSymbol.SetSourceAsync(
//                    new Uri("pack://application:,,,/iS3.Desktop;component/Images/pin_red.png"));
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.Message);
//            }
//        }

//        void MyMapView_NavigationCompleted(object sender, EventArgs e)
//        {
//            MyMapView.NavigationCompleted -= MyMapView_NavigationCompleted;
//            _isHitTesting = false;
//        }

//        async void MyMapView_MouseMove(object sender, MouseEventArgs e)
//        {
//            if (_isHitTesting)
//                return;

//            try
//            {
//                _isHitTesting = true;

//                Point screenPoint = e.GetPosition(MyMapView);
//                Graphic graphic = await ProjectGraphicsLayer.HitTestAsync(MyMapView, screenPoint);
//                if (graphic != null)
//                {
//                    mapTip.DataContext = graphic;
//                    mapTip.Visibility = System.Windows.Visibility.Visible;

//                    ProjectTitle.Text = (string)graphic.Attributes["Description"];
//                }
//                else
//                {
//                    mapTip.Visibility = System.Windows.Visibility.Collapsed;
//                    ProjectTitle.Text = "";
//                }
//            }
//            catch
//            {
//                mapTip.Visibility = System.Windows.Visibility.Collapsed;
//                ProjectTitle.Text = "";
//            }
//            finally
//            {
//                _isHitTesting = false;
//            }
//        }

//        async void MyMapView_MouseDown(object sender, MouseButtonEventArgs e)
//        {
//            try
//            {
//                _isHitTesting = true;

//                Point screenPoint = e.GetPosition(MyMapView);
//                Graphic graphic = await ProjectGraphicsLayer.HitTestAsync(MyMapView, screenPoint);
//                if (graphic != null)
//                {
//                    string definitionFile = graphic.Attributes["DefinitionFile"] as string;
//                    App app = Application.Current as App;

//                    IS3MainWindow mw = (IS3MainWindow)app.MainWindow;
//                    mw.SwitchToMainFrame(definitionFile);
//                }
//            }
//            catch (Exception ex)
//            {
//            }
//            finally
//            {
//                _isHitTesting = false;
//            }
//        }

//        void MyMapView_Loaded(object sender, RoutedEventArgs e)
//        {
//            if (Projects == null)
//            {
//                LoadProjectList();

//                // switch to the default project
//                if (Projects != null)
//                {
//                    ProjectLocation loc =
//                        Projects.Locations.Find(i => i.Default == true);
//                    if (loc != null)
//                    {
//                        App app = Application.Current as App;
//                        IS3MainWindow mw = (IS3MainWindow)app.MainWindow;
//                        mw.SwitchToMainFrame(loc.DefinitionFile);
//                    }
//                }
//            }

//            if (Projects != null)
//            {
//                Envelope projectExtent = new Envelope(Projects.XMin, Projects.YMin,
//                    Projects.XMax, Projects.YMax);

//                AddProjectsToMap();
//                //Map.ZoomTo(ProjectExtent);
//            }
//        }

//        public void LoadProjectList()
//        {
//            if (Projects != null)
//                return;

//            try
//            {
//                string exeLocation = Assembly.GetExecutingAssembly().Location;
//                string exePath = System.IO.Path.GetDirectoryName(exeLocation);
//                DirectoryInfo di = System.IO.Directory.GetParent(exePath);
//                string rootPath = di.FullName+"\\Output";
//                string dataPath = rootPath + "\\Data";
//                string filePath = dataPath + "\\" + file;

//                StreamReader reader = new StreamReader(filePath);
//                object obj = XamlReader.Load(reader.BaseStream);
//                Projects = obj as ProjectList;
//            }
//            catch (Exception error)
//            {
//                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK);
//            }
//        }

//        private void AddOnlineBaseMap()
//        {
//            if (Projects.UseGeographicMap)
//            {
//                ArcGISTiledMapServiceLayer baseLayer =
//                    new ArcGISTiledMapServiceLayer();
//                baseLayer.ID = "BaseLayer";
//                baseLayer.ServiceUri = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer";
//                baseLayer.IsVisible = true;

//                Map.Layers.Insert(0, baseLayer);
//            }
//        }

//        private void AddProjectsToMap()
//        {
//            GraphicsLayer gLayer = 
//                Map.Layers["ProjectGraphicsLayer"] as GraphicsLayer;

//            foreach (ProjectLocation loc in Projects.Locations)
//            {
//                Graphic g = new Graphic()
//                {
//                    Geometry = new MapPoint(loc.X, loc.Y),
//                    Symbol = _pinMarkerSymbol,
//                };
//                g.Attributes["ID"] = loc.ID;
//                g.Attributes["DefinitionFile"] = loc.DefinitionFile;
//                g.Attributes["Description"] = loc.Description;

//                gLayer.Graphics.Add(g);
//            }
//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Markup;
using System.Diagnostics;

using System.Reflection;

using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;

using iS3.Core;
using System.Threading.Tasks;
using Esri.ArcGISRuntime.Controls;
using System.Data.SqlClient;
using System.Xml.Linq;
using iS3.Core.Repository;

namespace iS3.Desktop
{
    //************************  Notice  **********************************
    //** This file is part of iS3
    //**
    //** Copyright (c) 2015 Tongji University iS3 Team. All rights reserved.
    //**
    //** This library is free software; you can redistribute it and/or
    //** modify it under the terms of the GNU Lesser General Public
    //** License as published by the Free Software Foundation; either
    //** version 3 of the License, or (at your option) any later version.
    //**
    //** This library is distributed in the hope that it will be useful,
    //** but WITHOUT ANY WARRANTY; without even the implied warranty of
    //** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    //** Lesser General Public License for more details.
    //**
    //** In addition, as a special exception,  that plugins developed for iS3,
    //** are allowed to remain closed sourced and can be distributed under any license .
    //** These rights are included in the file LGPL_EXCEPTION.txt in this package.
    //**
    //**************************************************************************

    public partial class ProjectListPage : UserControl
    {
        public ProjectListPage()
        {
            InitializeComponent();

            ProjectTitle.Text = "";

            //初始化地图内容
            initMap();

            //初始化图标
            InitializePictureMarkerSymbol();

            //初始化右键菜单
            initialContextMenu();
            AddContextMenu();
        }
        #region IProjectList 接口
        public ProjectList myProjectList { get; set; }
        #endregion
        #region 右键菜单
        Graphic _selectGraphic;
        ContextMenu aMenu;
        void initialContextMenu()
        {
            aMenu = new ContextMenu();
            System.Windows.Controls.MenuItem ViewMenu = new System.Windows.Controls.MenuItem();
            ViewMenu.Header = "进入项目";
            ViewMenu.Click += ViewMenu_Click;
            aMenu.Items.Add(ViewMenu);
        }
        void AddContextMenu()
        {
            MyMapView.ContextMenu = aMenu;
        }
        void RemoveContextMenu()
        {
            MyMapView.ContextMenu = null;
        }
        //右键项目，进入项目详情页
        private void ViewMenu_Click(object sender, RoutedEventArgs e)
        {
            string _projectID = _selectGraphic.Attributes["ID"] as string;
            App app = Application.Current as App;
            IS3MainWindow mw = (IS3MainWindow)app.MainWindow;
            mw.SwitchToMainFrame(_projectID);
        }
        #endregion
        #region GIS Map Operation

        //平常状态图标
        PictureMarkerSymbol _MarkerSymbol_Normal;
        //选中状态图标
        PictureMarkerSymbol _MarkerSymbol_Select;
        private SpatialReference _srEMap;
        private bool _isHitTesting;
        //初始化地图事件
        void initMap()
        {
            _srEMap = Map.SpatialReference;

            MyMapView.Loaded += MyMapView_Loaded;
            MyMapView.MouseMove += MyMapView_MouseMove;
            MyMapView.MouseLeftButtonDown += MyMapView_MouseLeftButtonDown;
            MyMapView.MouseRightButtonDown += MyMapView_MouseRightButtonDown;
            MyMapView.NavigationCompleted += MyMapView_NavigationCompleted;

            _isHitTesting = true;
        }
        //初始化地图图标
        private async void InitializePictureMarkerSymbol()
        {
            _MarkerSymbol_Normal = LayoutRoot.Resources["DefaultMarkerSymbol"]
                as PictureMarkerSymbol;
            _MarkerSymbol_Select = LayoutRoot.Resources["DefaultMarkerSymbol2"]
                as PictureMarkerSymbol;
            try
            {
                await _MarkerSymbol_Normal.SetSourceAsync(
                new Uri("pack://application:,,,/iS3.Desktop;component/Images/pin_red.png"));
                await _MarkerSymbol_Select.SetSourceAsync(
                    new Uri("pack://application:,,,/iS3.Desktop;component/Images/pIcon64.png"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        void MyMapView_Loaded(object sender, RoutedEventArgs e)
        {
            if (myProjectList == null)
            {
                LoadProjectListData();
            }
        }
        public async Task LoadProjectListData()
        {
            try
            {
                myProjectList = new ProjectList();
                myProjectList.Locations = await new RepositoryForProject().RetrieveProjectList();
                // switch to the default project
                if (myProjectList != null)
                {
                    ProjectLocation loc =
                        myProjectList.Locations.ToList().Find(i => i.Default == true);
                    if ((loc != null))
                    {

                        //App app = Application.Current as App;
                        //IS3MainWindow mw = (IS3MainWindow)app.MainWindow;
                        //mw.SwitchToMainFrame(loc.CODE);
                    }
                    projectBox.ItemsSource = myProjectList.Locations;
                }
                if (myProjectList != null)
                {
                    Envelope projectExtent = new Envelope(myProjectList.XMin, myProjectList.YMin,
                        myProjectList.XMax, myProjectList.YMax);

                    AddProjectsToMap();
                    //Map.ZoomTo(ProjectExtent);
                }
            }
            catch (Exception ex)
            {

            }

        }
        GraphicsLayer gLayer;
        private void AddProjectsToMap()
        {
            gLayer = Map.Layers["ProjectGraphicsLayer"] as GraphicsLayer;

            foreach (ProjectLocation loc in myProjectList.Locations)
            {
                Graphic g = new Graphic()
                {
                    Geometry = new MapPoint(loc.X, loc.Y),
                    Symbol = _MarkerSymbol_Normal,
                };
                g.Attributes["ID"] = loc.CODE;
                g.Attributes["DefinitionFile"] = loc.ProjectTitle;
                g.Attributes["Description"] = loc.ProjectTitle;

                gLayer.Graphics.Add(g);
            }
        }

        public void setCoord(MapPoint mapPoint)
        {
            string format = "X = {0}, Y = {1}";
            string coord = string.Format(format,
                Math.Round(mapPoint.X, 2), Math.Round(mapPoint.Y, 2));
            MapCoordsTB.Text = coord;
        }
        MapPoint screenPoint2MapPoint(Point screenPoint)
        {
            MapPoint mapPoint = MyMapView.ScreenToLocation(screenPoint);
            if (mapPoint == null)
                return null;
            if (MyMapView.WrapAround)
                mapPoint = GeometryEngine.NormalizeCentralMeridian(mapPoint) as MapPoint;
            if (_srEMap != null)
            {
                // transform the map point to user defined spatial reference coordinate system
                Esri.ArcGISRuntime.Geometry.Geometry g = GeometryEngine.Project(mapPoint, _srEMap);
                mapPoint = g as MapPoint;
            }
            return mapPoint;
        }
        private void projectBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selectID = myProjectList.Locations[projectBox.SelectedIndex].CODE;
            foreach (Graphic g in gLayer.Graphics)
            {
                if (g.Attributes["ID"].ToString() == selectID)
                {
                    g.Symbol = _MarkerSymbol_Select;
                }
                else
                {
                    g.Symbol = _MarkerSymbol_Normal;
                }
            }
        }
        #endregion
        #region  GIS 事件

        void MyMapView_NavigationCompleted(object sender, EventArgs e)
        {
            MyMapView.NavigationCompleted -= MyMapView_NavigationCompleted;
            _isHitTesting = false;
        }

        async void MyMapView_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isHitTesting)
                return;

            try
            {

                _isHitTesting = true;

                Point screenPoint = e.GetPosition(MyMapView);

                //设置鼠标当前坐标
                MapPoint mapPoint = screenPoint2MapPoint(screenPoint);
                if (mapPoint == null)
                    return;
                setCoord(mapPoint);


                Graphic graphic = await ProjectGraphicsLayer.HitTestAsync(MyMapView, screenPoint);
                if (graphic != null)
                {
                    mapTip.DataContext = graphic;
                    mapTip.Visibility = System.Windows.Visibility.Visible;
                    ProjectTitle.Text = (string)graphic.Attributes["ID"];
                }
                else
                {
                    mapTip.Visibility = System.Windows.Visibility.Collapsed;
                    ProjectTitle.Text = "";
                }
            }
            catch
            {
                mapTip.Visibility = System.Windows.Visibility.Collapsed;
                ProjectTitle.Text = "";

            }
            finally
            {
                _isHitTesting = false;
            }
        }
        async void MyMapView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                RemoveContextMenu();
                //MyMapView.ContextMenu.Visibility = Visibility.Collapsed;

                _isHitTesting = true;

                Point screenPoint = e.GetPosition(MyMapView);
                Graphic graphic = await ProjectGraphicsLayer.HitTestAsync(MyMapView, screenPoint);
                if (graphic != null)
                {
                    _selectGraphic = graphic;
                    AddContextMenu();
                }
            }
            catch
            {
            }
            finally
            {
                _isHitTesting = false;
            }

        }

        async void MyMapView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                AddContextMenu();
                // MyMapView.ContextMenu.Visibility = Visibility.Collapsed;

                _isHitTesting = true;

                Point screenPoint = e.GetPosition(MyMapView);
                Graphic graphic = await ProjectGraphicsLayer.HitTestAsync(MyMapView, screenPoint);
                if (graphic != null)
                {
                    _selectGraphic = graphic;
                    foreach (Graphic g in gLayer.Graphics)
                    {
                        g.Symbol = _MarkerSymbol_Normal;
                    }
                    _selectGraphic.Symbol = _MarkerSymbol_Select;
                    for (int i = 0; i < myProjectList.Locations.Count; i++)
                    {
                        if (myProjectList.Locations[i].ID.ToString() == graphic.Attributes["ID"].ToString())
                        {
                            projectBox.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                _isHitTesting = false;
            }
        }
        #endregion


    }
}
