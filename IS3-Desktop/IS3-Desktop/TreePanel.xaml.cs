using System;
using System.Windows;
using System.Windows.Controls;

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
    /// Interaction logic for TreePanel.xaml
    /// </summary>
    public partial class TreePanel : UserControl
    {
        public EventHandler<DGObjectsSelectionChangedEventArgs> dGObjectsSelectionChangedEventTriggle;
        public TreePanel(Tree rootTree)
        {
            InitializeComponent();
            DomainTreeView.ItemsSource = rootTree.Children;
        }
        private void DomainTreeView_SelectedItemChanged(object sender,
           RoutedPropertyChangedEventArgs<object> e)
        {
            if (dGObjectsSelectionChangedEventTriggle != null)
            {
                DGObjectsSelectionChangedEventArgs args = new DGObjectsSelectionChangedEventArgs();
                if (((e.NewValue as Tree) == null) || ((e.NewValue as Tree).RefObjsName == null))
                {
                    args.addedObjs = null;
                }
                else
                {
                    string dgObjectsName = (e.NewValue as Tree).RefObjsName;
                    if (Globals.project.objsDefIndex.ContainsKey(dgObjectsName))
                    {
                        args.addedObjs = Globals.project.objsDefIndex[dgObjectsName];
                        args.addedObjs.filter = (e.NewValue as Tree).Filter;
                    }
                }
                dGObjectsSelectionChangedEventTriggle(this, args);
            }
        }
    }
}
