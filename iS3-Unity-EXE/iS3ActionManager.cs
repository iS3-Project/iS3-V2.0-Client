//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using iS3UnityLib.Model.Event;
//using iS3UnityLib.Model.ObjModel;
//using System.Windows;
//using iS3.Core.Client;

//namespace iS3.Unity.EXE
//{
//    public class UnityLayerUpdateEvent
//    {
//        public LayerTreeNode myNode { get; set; }
//    }
//    public class iS3ActionManager
//    {
//        private U3DViewModel model;
//        public iS3ActionManager(U3DViewModel model)
//        {
//            this.model = model;
//        }
//        public void iS3Action(LoadingCompletedEvent _event)
//        {

//        }
//        public void iS3Action(UpdateUnitylayerInfoEvent _event)
//        {
//            try
//            {
//               // UnityLayerWindow.data = _event.MyUnitylayerModel.layerTreeNode;
//                if (model.unityLayerUpdateEventEventHandler != null)
//                {
//                    model.unityLayerUpdateEventEventHandler(this, new UnityLayerUpdateEvent()
//                    {
//                        myNode = _event.MyUnitylayerModel.layerTreeNode,
//                    });
//                }
//                IMainFrame mainframe = Globals.mainframe;
//                IView3D myView3D = null;
//                foreach (IView view in mainframe.views)
//                {
//                    if (view.type == ViewType.General3DView)
//                    {
//                        myView3D = view as IView3D;
//                        ;
//                    }
//                }
//                myView3D.mySceneLayer = new ObservableCollection<ModelLayer>();


//                ModelLayer root = new ModelLayer();

//                CopyTree(_event.MyUnitylayerModel.layerTreeNode, root);
//                myView3D.mySceneLayer = new ObservableCollection<ModelLayer>(root.childLayer);

//                Globals.myLayers = new ObservableCollection<ModelLayer>(root.childLayer);
//            }
//            catch (Exception ex){ MessageBox.Show(ex.ToString()); }
           
//        }

//        void CopyTree(LayerTreeNode node, ModelLayer layer)
//        {
//            layer.path = node.path;
//            layer.name = node.name;
//            layer.childLayer=new ObservableCollection<ModelLayer>().ToList();
//            foreach(var _node in node.childNodes)
//            {
//                ModelLayer _layer=new ModelLayer();
//                layer.childLayer.Add(_layer);
//                CopyTree(_node, _layer);
//            }
//        }

       

//        public void iS3Action(QueryCameraPosEvent _event)
//        {
//            try
//            {
//                Globals.CameraPosManager.Dispatcher.BeginInvoke(new Action(() =>
//                {
//                    IS3.Core.Globals.CameraPosManager.AddPos(new CameraPos()
//                    {
//                        CameraID = _event.cameraID,
//                        Name = "视点" + (Globals.CameraPosManager.CameraPosList.Count + 1),
//                        px = _event.px,
//                        py = _event.py,
//                        pz = _event.pz,
//                        rw = _event.rw,
//                        rx = _event.rx,
//                        ry = _event.ry,
//                        rz = _event.rz
//                    });
//                }));
//            }
//            catch
//            {
//                Globals.application.MainWindow?.Dispatcher.BeginInvoke(new Action(() =>
//                {
//                    IS3.Core.Globals.CameraPosManager.AddPos(new CameraPos()
//                    {
//                        CameraID = _event.cameraID,
//                        Name = "视点" + (Globals.CameraPosManager.CameraPosList.Count + 1),
//                        px = _event.px,
//                        py = _event.py,
//                        pz = _event.pz,
//                        rw = _event.rw,
//                        rx = _event.rx,
//                        ry = _event.ry,
//                        rz = _event.rz
//                    });
//                }));
//            }

//            string json= Newtonsoft.Json.JsonConvert.SerializeObject(Globals.CameraPosManager);
//            Globals.CameraPosManager.Save(json);
//        } 
//    }
//}
