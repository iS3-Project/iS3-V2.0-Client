using System.Collections.Generic;

using iS3.Core;
using System.Windows;

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
    public class DemoTools : Tools
    {
        //基本信息
        public override string name() { return "iS3.DemoTools"; }
        public override string provider() { return "Tongji iS3 team"; }
        public override string version() { return "1.0"; }

        //分析工具列表
        List<ToolTreeItem> items;
        public override IEnumerable<ToolTreeItem> treeItems()
        {
            return items;
        }
        List<iS3MenuItem> menus;
        public override IEnumerable<iS3MenuItem> menuItems()
        {
            return menus;
        }
        //新建分析工具窗口
        DemoWindow demoWindow;
        public void callDemoWindow()
        {
            if (demoWindow != null)
            {
                demoWindow.Show();
                return;
            }

            demoWindow = new DemoWindow();
            demoWindow.Closed += (o, args) =>
                {
                    demoWindow = null;
                };
            demoWindow.Show();
        }

        //新建工具树
        public DemoTools()
        {
            items = new List<ToolTreeItem>();

            ToolTreeItem item = new ToolTreeItem("Demo|Basic", "DemoTest", callDemoWindow);
            items.Add(item);

            menus = new List<iS3MenuItem>();
            iS3MenuItem mItem00 = new iS3MenuItem("地质|数据管理", "钻孔导入", true, "TBM.png", new DFunc(CalMenu00));
            menus.Add(mItem00);
            iS3MenuItem mItem10 = new iS3MenuItem("地质|数据管理", "地层导入", false, "Tunnel-32.png", new DFunc(CalMenu10));
            menus.Add(mItem10);
            iS3MenuItem mItem11 = new iS3MenuItem("地质|数据管理", "水位导入", false, "TunnelAxis-32.png", new DFunc(CalMenu11));
            menus.Add(mItem11);
            iS3MenuItem mItem12 = new iS3MenuItem("地质|数据管理", "断层导入", false, "TunnelCrossSection-32.png", new DFunc(CalMenu11));
            menus.Add(mItem12);
            iS3MenuItem mItem20 = new iS3MenuItem("地质|分析工具", "地质剖切", true, "TunnelDistToSt.png", new DFunc(CalMenu20));
            menus.Add(mItem20);
            iS3MenuItem mItem30 = new iS3MenuItem("监测|报警预警", "报警功能", true, "TunnelDistToSt.png", new DFunc(CalMenu20));
            menus.Add(mItem30);
        }
        public void CalMenu00()
        {
            MessageBox.Show("CalMenu00");
        }
        public void CalMenu10()
        {
            MessageBox.Show("CalMenu10");
        }
        public void CalMenu11()
        {
            MessageBox.Show("CalMenu11");
        }
        public void CalMenu20()
        {
            MessageBox.Show("CalMenu20");
        }
    }
}
