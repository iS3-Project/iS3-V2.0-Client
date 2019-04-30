using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using iS3;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using iS3.Core;
using iS3.Core.Geometry;
using iS3.Core.Graphics;
using System.Windows.Controls;

namespace iS3.Unity.EXE
{
    public class U3DViewModel : IView
    {
        UnityPanel panel;
        public U3DViewModel(UnityPanel panel)
        {
            this.panel = panel;
        }
        public Project prj { get; set; }

        public ViewType type { get { return ViewType.General3DView; } }

        public string name { get; set; }

        public EngineeringMap eMap { get; set; }

        public IEnumerable<IGraphicsLayer> layers
        {
            get { return null; }
        }

        public ISpatialReference spatialReference { get{ return null; } }

        public IGraphicsLayer drawingLayer
        {
            get { return null; }
        }

        public string ViewName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ViewID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool DefaultShow => throw new NotImplementedException();

        public ViewLocation ViewPos => throw new NotImplementedException();

        public event EventHandler<ObjSelectionChangedEventArgs> objSelectionChangedTrigger;
        public event EventHandler<DrawingGraphicsChangedEventArgs> drawingGraphicsChangedTrigger;

        public Task<IGraphicsLayer> addGdbLayer(LayerDef layerDef, string dbFile, int start = 0, int maxFeatures = 0)
        {
            return null;
        }

        public void addLayer(IGraphicsLayer layer)
        {
        }

        public void addLocalTiledLayer(string filePath, string layerID)
        {
        }

        public void addSeletableLayer(string layerID)
        {
        }

        public Task<IGraphicsLayer> addShpLayer(LayerDef layerDef, string shpFile, int start = 0, int maxFeatures = 0)
        {
            return null;
        }

        public IGraphicsLayer getLayer(string layerID)
        {
            return null;
        }

        public void highlightAll(bool on = true)
        {
            
        }

        public void highlightObject(DGObject obj, bool on = true)
        {
           
        }

        public void highlightObjects(IEnumerable<DGObject> objs, bool on = true)
        {
          
        }

        public void highlightObjects(IEnumerable<DGObject> objs, string layerID, bool on = true)
        {
           
        }

        public void initializeView()
        {
           
        }

        public async Task loadPredefinedLayers()
        {
            Load3DScene();
        }
        protected UnityPanel _parent;
        public TcpServer tcp;
        public void Load3DScene()
        {
            string filename = Runtime.dataPath + "//" + Globals.project.projDef.ID + "//" + eMap.LocalMapFileName;
            try
            {
                var  process = new Process();
                process.StartInfo.FileName = filename;
                process.StartInfo.Arguments = panel.GetArguments();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                process.WaitForInputIdle();

                panel.EnumChildWindows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ".\nCheck if Container.exe is placed next to Child.exe.");
            }
            tcp = new TcpServer();
            TcpServer.messageHandler += ReceiveMessageListener;
            tcp.StartServer();
        }
        public void ReceiveMessageListener(object sender, string e)
        {
            try
            {
                //UnityEvent myEvent = JsonConvert.DeserializeObject<UnityEvent>(e);
                //UnityEventType type = myEvent.type;
                //switch (type)
                //{
                //    case UnityEventType.SelectObjEvent:

                //        SelectObjEvent _event4 =
                //            JsonConvert.DeserializeObject<SelectObjEvent>(e.message);
                //        dealWithSelection(_event4);
                //        break;
                //    default: break;
                //}
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            }
        }

        public Point locationToScreen(IMapPoint mapPoint)
        {
            return new Point();
        }

        public void objSelectionChangedListener(object sender, ObjSelectionChangedEventArgs e)
        {
       
        }

        public void onClose()
        {
           
        }

        public IGraphicsLayer removeLayer(string layerID)
        {
            return null;
        }

        public void removeSelectableLayer(string layerID)
        {
        }

        public IMapPoint screenToLocation(Point screenPoint)
        {
            return null;
        }

        public int syncObjects()
        {
            return 0;
        }

        public void zoomTo(IGeometry geom)
        {
            
        }

        public bool SetData(params object[] objs)
        {
            throw new NotImplementedException();
        }

        public void dgobjectsSelectionChangedListener(object sender, DGObjectsSelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
