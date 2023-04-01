using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 仓库管理系统.Global
{
    public class NavigateToInterface
    {
        public static void DoSelect(object menuTitle)
        {
            switch (menuTitle)
            {
                case MenuTitle.One:
                    MessageBox.Show("界面一");
                    break;
                case MenuTitle.Two:
                    MessageBox.Show("界面二");
                    break;
                case MenuTitle.Three:
                    MessageBox.Show("界面3");
                    break;
                case MenuTitle.Four:
                    MessageBox.Show("界面4");
                    break;
                case MenuTitle.Five:
                    MessageBox.Show("界面5");
                    break;
                case MenuTitle.Six:
                    MessageBox.Show("界面6");
                    break;
            }

        }


        public static void DoSelect5(object menuTitle)
        {
            switch (menuTitle)
            {
                case (int)MenuTitle.One:
                    MessageBox.Show("界面一");
                    break;
                case (int)MenuTitle.Two:
                    MessageBox.Show("界面二");
                    break;
                case (int)MenuTitle.Three:
                    MessageBox.Show("界面3");
                    break;
                case (int)MenuTitle.Four:
                    MessageBox.Show("界面4");
                    break;
                case (int)MenuTitle.Five:
                    MessageBox.Show("界面5");
                    break;
                case (int)MenuTitle.Six:
                    MessageBox.Show("界面6");
                    break;
            }

        }

       

    }
}
