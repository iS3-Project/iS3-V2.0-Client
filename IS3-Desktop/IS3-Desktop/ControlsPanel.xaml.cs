using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using iS3.Core;

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

    /// <summary>
    /// Interaction logic for ToolsPanel.xaml
    /// </summary>
    public partial class ControlsPanel : UserControl
    {
        public ControlTreeItem controlsTree { get; set; }
        public EventHandler<IBaseView> AddExteralControlEventTriggle;
        public ControlsPanel()
        {
            InitializeComponent();

            ControlTreeItem root = new ControlTreeItem(null, "Root");
            controlsTree = new ControlTreeItem(null, "controls");
            root.add(controlsTree);

            //ToolTreeItem test1 = new ToolTreeItem("Structure|Tunnel", "Test1");
            //toolboxesTree.add(test1);
            //ToolTreeItem tree = root.find("Toolboxes/Structure/Tunnel/Test1");

            ControlsTreeView.ItemsSource = root.items;
        }

        private void ControlsTreeView_MouseDoubleClick(object sender,
            MouseButtonEventArgs e)
        {
            ControlTreeItem tree = ControlsTreeView.SelectedItem as ControlTreeItem;

            try
            {
                if (tree != null && tree.view != null)
                {
                    Globals.mainframe.AddExteralControl(tree.view);
                    //if (AddExteralControlEventTriggle != null)
                    //{
                    //    AddExteralControlEventTriggle(this, tree.view);
                    //}
                }
            }
            catch (Exception ex)
            {
                string format = "Error running plugin control: {0}.";
                string msg = String.Format(format, tree.displayName);
                ErrorReport.Report(msg);
                ErrorReport.Report(ex.Message);
            }
        }
    }
}
