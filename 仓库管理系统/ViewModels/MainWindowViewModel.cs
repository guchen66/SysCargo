﻿using ImTools;
using Moudles.Common;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using SqlSugar;
using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Providers;
using SqlSugar.DbAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using 仓库管理系统.Global;
using 仓库管理系统.Models;
using 仓库管理系统.Shell.Views;
using 仓库管理系统.Views;

namespace 仓库管理系统.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public AppData appData { get; set; } = AppData.Instance;
        public SimpleClient<User> sdb = new SimpleClient<User>();

       
        //public BaseOperate<User> BaseOperate { get; set; }
      

        IRegionNavigationJournal _navigationJournal;//导航日志，上一页，下一页
        IRegionManager _regionManager;//区域管理
        IDialogService _dialogService;
        IEventAggregator _eventAggregator;
        public MainWindowViewModel(IDialogService dialogService, IRegionManager regionManager, IEventAggregator eventAggregator, IRegionNavigationJournal navigationJournal)
        {
            _dialogService = dialogService;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _navigationJournal= navigationJournal;

            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(HomeView));
            regionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(HeaderView));
            regionManager.RegisterViewWithRegion(RegionNames.FooterRegion, typeof(FooterView));
            /*_eventAggregator.GetEvent<MyEvent2>().Subscribe(DoGoBack);
            _eventAggregator.GetEvent<MyEvent3>().Subscribe(DoForWard);*/

            
        }

        private DelegateCommand<string> _selectCommand;
        public DelegateCommand<string> SelectCommand
        { 
            get
            {
                return _selectCommand = new DelegateCommand<string>(DoSelect);
            }
            set { _selectCommand = value; }
        }
       
        public void DoSelect(string menuTitle)
        {
            //switch ((MenuTitle)Enum.Parse(typeof(MenuTitle), menuTitle))
            switch (menuTitle)
            {
                case "首页":
                    Navigate("HomeView");
                    break;
                case "设置":
                    Navigate("SetView");
                    break;
                case "用户信息":
                    Navigate("User");
                    break;
                case "仓库汇总":
                    Navigate("Total");
                    break;
                case "入库信息":
                    Navigate("StorageView");
                    break;
                case "出库信息":
                    Navigate("Outbound");
                    break;
                case "智能报警":
                    Navigate("Alarm");
                    break;
            }

        }

        MenuDict menuDict = null;
        public void DoSelect2(string menuTitle)
        {
            menuDict = new MenuDict();
            foreach (var item in menuDict.GetDict())
            {

                if (menuTitle==item.Value)
                {
                    // Navigate((item.Value+"View").ToString());
                    switch (menuTitle)
                    {
                        case "首页":
                            Navigate("HomeView");
                            break;
                        case "设置":
                            Navigate("SetView");
                            break;
                        case "用户信息":
                            Navigate("User");
                            break;
                        case "仓库汇总":
                            Navigate("Total");
                            break;
                        case "入库信息":
                            Navigate("StorageView");
                            break;
                        case "出库信息":
                            Navigate("Outbound");
                            break;
                        case "智能报警":
                            Navigate("Alarm");
                            break;
                    }
                }
                
            }
            
        }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath, arg =>
                {
                    _navigationJournal = arg.Context.NavigationService.Journal;
                });

        }

        
        
    }
}
