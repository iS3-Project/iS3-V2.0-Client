using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iS3.Core;
using DemoControls;

namespace iS3.Controls
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

    // Summary:
    //     This is the entry point for the extension
    public class DemoControls : ExteralControls
    {
        public override string name() { return "DemoControls"; }
        public override string provider() { return "Tongji iS3 team"; }
        public override string version() { return "1.0"; }
        //public override string init()
        //{
        //    Globals.mainframe.projectLoaded += mainframe_projectLoaded;
        //    return base.init();
        //}
        List<ControlTreeItem> items;
        public override IEnumerable<ControlTreeItem> treeItems()
        {
            return items;
        }
        public DemoControls()
        {
            items = new List<ControlTreeItem>();
            items.Add(new ControlTreeItem("Controls", "DemoControl", new DemoControl()));
            items.Add(new ControlTreeItem("Controls", "DemoControlHidden", new DemoControlHidden()));
            items.Add(new ControlTreeItem("Charts", "DemoLineChart", new DemoLineChart()));
            //items.Add(new ControlTreeItem("Charts", "LineChart", new LineChart()));
            //items.Add(new ControlTreeItem("Charts", "PieChart", new PieChart()));
            //items.Add(new ControlTreeItem("Charts", "DynamicalChart", new DynamicChart()));
            //items.Add(new ControlTreeItem("Charts", "AirConditionChart", new AirConditionChart()));
        }
        //// Summary:
        ////     Project loaded call back function
        //// Remarks:
        ////     When project data is loaded, the relationship between MonGroup 
        ////     and MonPoint need to be established.
        //void mainframe_projectLoaded(object sender, EventArgs e)
        //{
        //    var mainframe = Globals.mainframe;
        //    mainframe.addExternalView(new DemoControl());
        //    mainframe.addExternalView(new DemoControlHidden());
        //    mainframe.addExternalView(new DemoChart());
        //}
    }
}
