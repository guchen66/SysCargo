﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Providers;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using SqlSugar.DbAccess.Services;
using System.Data;
using System.IO;
using System.Windows;

namespace 仓库管理系统.Shell.ViewModels
{

    public class TotalViewModel : BindableBase
    {

        CargoService db = new CargoService();
       
        private readonly IDialogService _dialogService;
        
        private IDialogCoordinator _dialogCoordinator;
      //  IEnumerable<Cargo> dataList=null;
        public TotalViewModel(IDialogService dialogService, IDialogCoordinator dialogCoordinator)
        {

            _dialogService = dialogService;
            _dialogCoordinator = dialogCoordinator;
            //仓储查询结果是List，转成ObservableCollection
            db.GetAllCargos().ForEach(x => GridModelList.Add(x));

        }

        private ObservableCollection<Cargo> gridModelList = new ObservableCollection<Cargo>();//已经封装好的集合列表，提供实时刷新，当做有通知的List<Student>
        public ObservableCollection<Cargo> GridModelList//和前台要对应
        {
            get{ return gridModelList;}
            set{ gridModelList = value;RaisePropertyChanged();}
        }


        //查询全部
        public ObservableCollection<Cargo> SelectAll()
        {
            List<Cargo> cargos = new List<Cargo>();
            if (cargos != null)
            {
                GridModelList.Clear();
                cargos.ForEach(x => GridModelList.Add(x));
            }
            return GridModelList;
        }

        //TextBox初始为Empty
        private string search = string.Empty;

        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }


        //查询
        private DelegateCommand _queryCommand;
        public DelegateCommand QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand(ExecuteQueryCmd));

        void ExecuteQueryCmd()
        {
            
            var dataList = db.GetAllCargos().ToList().Where(it => it.Id.ToString().Contains(Search)
            || it.Name.Contains(Search)||it.UserName.Contains(Search)
            );
            GridModelList = new ObservableCollection<Cargo>();
            if (dataList != null)
            {
                dataList.ToList().ForEach(o => GridModelList.Add(o));

            }
        }


        
        //新增
        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand =>
            _addCommand ?? (_addCommand = new DelegateCommand(ExecuteAddCmd));

        void ExecuteAddCmd()
        {
            DialogParameters paramters = new DialogParameters();
            paramters.Add("RefreshValue", new Action(Refresh));
            _dialogService.ShowDialog("AddCargoDialogView", paramters,r =>
            {
                /*if (r.Result == ButtonResult.Yes)
                {
                    Refresh();
                }*/
            });
        }

      
        //修改
        private DelegateCommand<int?> _updateCommand;
        public DelegateCommand<int?> UpdateCommand =>
            _updateCommand ?? (_updateCommand = new DelegateCommand<int?>(ExecuteUpdateCmd));

        private void ExecuteUpdateCmd(int? id)
        {
            var dataList=db.GetAllCargos().Where(it=>it.Id==id);
            DialogParameters paramters = new DialogParameters();

            paramters.Add("dataList", dataList);

            paramters.Add("RefreshValue", new Action(Refresh));

           
            _dialogService.ShowDialog("UpdateCargoDialogView", paramters, r =>
            {

            });
        }

        //下载
        private DelegateCommand<string> _downloadCommand;
        public DelegateCommand<string> DownLoadCommand =>
            _downloadCommand ?? (_downloadCommand = new DelegateCommand<string>(ExecuteDownLoadCommand));

        private void ExecuteDownLoadCommand(string search)
        {

            //var dataList = sdb.GetList();
            // DataTable dt = FileData.ListToDataTable(dataList);

            var data = GridModelList;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            File.WriteAllText(@"E:\VS Workspace\Apply\仓库管理系统\ContentMoudle\DownLoad\Total.json", json);

            string path = @"E:\VS Workspace\Apply\仓库管理系统\ContentMoudle\DownLoad\Total.json";
            if (File.Exists(path))
            {
                MessageBox.Show("文件下载成功！");
            }
        }

        //删除
        private DelegateCommand<int?> _deleteCommand { get; set; }
        public DelegateCommand<int?> DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new DelegateCommand<int?>(ExecuteDeleCommand));
            set => _deleteCommand = value;
        }



        private async void ExecuteDeleCommand(int? ids)
        {
            var model = db.GetAllCargos().Where(it => it.Id == ids);

            if (model != null)
            {
                var settings = new MetroDialogSettings
                {
                    ColorScheme = MetroDialogColorScheme.Accented,
                    AnimateShow = false,
                    AnimateHide = false,
                    AffirmativeButtonText = "确认"
                };
                var result = await this._dialogCoordinator.ShowMessageAsync(this, "是否删除该用户?", "删除用户", MessageDialogStyle.AffirmativeAndNegative, settings);
                if (result == MessageDialogResult.Affirmative)
                {
                    //刷新
                    db.DeleteCargo((int)ids);
                    Refresh();
                }
                if (result == MessageDialogResult.Negative)
                {
                    Refresh();

                }
            }


        }

        //刷新
        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCmd));

        private void ExecuteRefreshCmd()
        {
            DoRefresh();


        }

        

        //用来刷新界面
        public void Refresh()
        {
            var dataList = db.GetAllCargos().Where(it => it.Name == Search);
           
            GridModelList = new ObservableCollection<Cargo>();
            if (dataList != null)
            {
                db.GetAllCargos().ForEach(x => GridModelList.Add(x));
            }
        }


        private async void DoRefresh()
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                ColorScheme = MetroDialogColorScheme.Accented,
                AnimateShow = true,
                AnimateHide = false,

            };
            var controller = await this._dialogCoordinator.ShowProgressAsync(this, "请稍等", "正在刷新数据...", settings: mySettings);

            controller.SetIndeterminate();
            Search = string.Empty;
            this.Refresh();
            await Task.Delay(3000); // Wait for 3 seconds
            await controller.CloseAsync();
        }

    }
}

