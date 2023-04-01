using ContentModule.ViewModels;
using ContentModule.Views;
using Microsoft.Win32;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ContentModule
{
    public class ContentModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {


            //注册弹窗
            containerRegistry.RegisterDialog<SucessDialog>();
            containerRegistry.RegisterDialog<ErrorDialog>();
            containerRegistry.RegisterDialog<AddUserDialogView, AddUserDialogViewModel>();
            containerRegistry.RegisterDialog<UpdateUserDialogView, UpdateUserDialogViewModel>();
            containerRegistry.RegisterDialog<AddCargoDialogView, AddCargoDialogViewModel>();

            containerRegistry.RegisterDialog<TestView, TestViewModel>();
        }
    }
}