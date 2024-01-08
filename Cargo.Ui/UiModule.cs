
using Cargo.Ui.ViewModels;
using Cargo.Ui.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Cargo.Ui
{
    public class UiModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册弹窗
            containerRegistry.RegisterDialog<SucessDialog>();
            containerRegistry.RegisterDialog<ErrorDialog>();
            containerRegistry.RegisterDialog<AddUserDialog, AddUserDialogViewModel>();
            containerRegistry.RegisterDialog<AddProcessDialog, AddProcessDialogViewModel>();
            containerRegistry.RegisterDialog<AddWorkStationDialog, AddWorkStationDialogViewModel>();
            containerRegistry.RegisterDialog<AddCargoDialog, AddCargoDialogViewModel>();

            containerRegistry.RegisterDialog<UpdateUserDialog, UpdateUserDialogViewModel>();
            containerRegistry.RegisterDialog<UpdateProcessDialog, UpdateProcessDialogViewModel>();
            containerRegistry.RegisterDialog<UpdateWorkPlaceDialog, UpdateWorkPlaceDialogViewModel>();
            containerRegistry.RegisterDialog<UpdateCargoDialog, UpdateCargoDialogViewModel>();
            containerRegistry.RegisterDialog<TestView, TestViewModel>();
        }
    }
}