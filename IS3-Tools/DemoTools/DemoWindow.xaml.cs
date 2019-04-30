using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

using iS3.Core;
using iS3.Core.Geometry;
using iS3.Core.Graphics;
using System;
using iS3.Monitoring;

namespace DemoTools
{
    #region Copyright Notice
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
    #endregion

    /// <summary>
    /// Interaction logic of DemoWindow.xaml
    /// </summary>
    public partial class DemoWindow : Window
    {
        public EventHandler<ObjSelectionChangedEventArgs> objSelectionChangedTrigger;
        public DemoWindow()
        {
            InitializeComponent();
            Loaded += DemoWindow_Loaded;
        }

        private void DemoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            objSelectionChangedTrigger += Globals.mainframe.objSelectionChangedListener;
            Globals.mainframe.objSelectionChangedTrigger += objSelectionChangedListener;
            Project prj = Globals.project;
            Domain monDomain = prj["Monitoring"];
            DGObjectsCollection dc = monDomain.getObjects("MonPoint");
            listLB.ItemsSource = dc.FirstOrDefault().values;
        }
        private void objSelectionChangedListener(object sender,ObjSelectionChangedEventArgs e)
        {
            if (view == null)
            {
                view = Globals.mainframe.getViewByID("DemoLineChart");
                holder.Children.Add(view as UserControl);
            }
            MonPoint mp = e.addedObjs.Values.FirstOrDefault().FirstOrDefault() as MonPoint;
            view.SetData(string.Format("测点{0}监测曲线", mp.Name),
            mp.readingsDict.Values.FirstOrDefault().ToList(),
            "time", "value", "", mp.Name);
        }
        DGObject _lastObj;
        IBaseView view;
        private void listLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGObject selectOne = listLB.SelectedItem as DGObject;
            List<DGObject> addedObjs = new List<DGObject>();
            List<DGObject> removedObjs = new List<DGObject>();
            //select the selected one
            if ((_lastObj != null) && (_lastObj.Name == selectOne.Name)) return;
            addedObjs.Add(selectOne);
            if (_lastObj != null)
            {
                removedObjs.Add(_lastObj);
            }
            if (objSelectionChangedTrigger != null)
            {
                Dictionary<string, IEnumerable<DGObject>> addedObjsDict = null;
                Dictionary<string, IEnumerable<DGObject>> removedObjsDict = null;
                if (addedObjs.Count > 0)
                {
                    addedObjsDict = new Dictionary<string, IEnumerable<DGObject>>();
                    addedObjsDict[selectOne.parent.definition.Name] = addedObjs;
                }
                if (removedObjs.Count > 0)
                {
                    removedObjsDict = new Dictionary<string, IEnumerable<DGObject>>();
                    removedObjsDict[_lastObj.parent.definition.Name] = removedObjs;
                }
                ObjSelectionChangedEventArgs args =
                    new ObjSelectionChangedEventArgs();
                args.addedObjs = addedObjsDict;
                args.removedObjs = removedObjsDict;
                objSelectionChangedTrigger(this, args);
            }
            _lastObj = selectOne;
        }

    }


}
