
namespace 仓库管理系统.Converters
{
    public static class PasswordBoxHelper
    { 
          public static readonly DependencyProperty PasswordProperty = 
             DependencyProperty.RegisterAttached(
                "Password", //属性
                typeof(string),//属性的类型
                typeof(PasswordBoxHelper), //属于的类，我们做的是静态，所以需要这个让附加属性可以知道他所在，我们到时可以使用 sender 拿到实例，所以需要知道他的类可以转
                new System.Windows.PropertyMetadata(default(string),//默认值
                OnPasswordPropertyChanged));//属性改变调的函数

    public static void SetPassword(DependencyObject element, string value)
    {
         element.SetValue(PasswordProperty, value);
    }

    public static string GetPassword(DependencyObject element)
    {
         return (string)element.GetValue(PasswordProperty);
    }

    public static readonly DependencyProperty AttachProperty =
            
            DependencyProperty.RegisterAttached(
                       "Attach", typeof(bool),
                       typeof(PasswordBoxHelper),
                       new System.Windows.PropertyMetadata(default(bool), Attach));

    public static void SetAttach(DependencyObject element, bool value)
    {
          element.SetValue(AttachProperty, value);
    }

    public static bool GetAttach(DependencyObject element)
    {
          return (bool)element.GetValue(AttachProperty);
    }

    public static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached(
                            "IsUpdating",
                            typeof(bool),
                            typeof(PasswordBoxHelper),
                            new System.Windows.PropertyMetadata(default(bool)));

    public static void SetIsUpdating(DependencyObject element, bool value)
    {
          element.SetValue(IsUpdatingProperty, value);
    }

    public static bool GetIsUpdating(DependencyObject element)
    {
          return (bool)element.GetValue(IsUpdatingProperty);
    }

    private static void OnPasswordPropertyChanged(DependencyObject sender,DependencyPropertyChangedEventArgs e)
    {
        PasswordBox passwordBox = sender as PasswordBox;//sender就是实例
        if (passwordBox != null)
        {
            passwordBox.PasswordChanged -= PasswordChanged; //在WPF绑定密码有说为何这样做
                                                            //我们需要修改的是在更改，所以不能让他继续 PasswordChanged 使用了会无限循环 所以先去掉，在后面加上。

            if (!(bool)GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
            }
            passwordBox.PasswordChanged += PasswordChanged;
        }
    }

    private static void Attach(DependencyObject sender,DependencyPropertyChangedEventArgs e)
    {
        PasswordBox passwordBox = sender as PasswordBox;

        if (passwordBox == null)
        {
            return;
        }
        //e.OldValue 改变前的值
        if ((bool)e.OldValue)//如果之前有绑定，我们就解绑
        {
            passwordBox.PasswordChanged -= PasswordChanged;
        }
        //e.NewValue 改变的值
        if ((bool)e.NewValue)
        {
            passwordBox.PasswordChanged += PasswordChanged;
        }
    }

    private static void PasswordChanged(object sender, RoutedEventArgs e)
    {
        PasswordBox passwordBox = sender as PasswordBox;
        if (passwordBox != null)
        {
            SetIsUpdating(passwordBox, true);//设置我们修改的是UI绑定的修改，那么不更改PasswordBox.password
                                             //设置是false会修改，我们通知修改密码，然后他修改后台password又通知PasswordChanged 这样会炸
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
    }
}
}
