
namespace 仓库管理系统.ViewModels
{
    public class HeaderViewModel : BaseViewModel
    {
        public SimpleClient<User> sdb = new SimpleClient<User>();


        public HeaderViewModel(IContainerProvider provider) : base(provider)
        {

        }

        #region HeaderView Skin颜色修改
        private SkinColorInfo _selectedSkinColor;
        public SkinColorInfo SelectedSkinColor
        {
            get { return _selectedSkinColor; }
            set
            {
                /* if (_selectedSkinColor != value)
                 {
                     _selectedSkinColor = value;
                     RaisePropertyChanged();
                 }*/
                _selectedSkinColor = value;
                RaisePropertyChanged();

            }
        }
        public IEnumerable<SkinColorInfo> SkinColorItemsProvider
        {
           /* get
            {
                return Enum.GetValues(typeof(SkinColorInfo))
                           .Cast<SkinColorInfo>()
                           .ToList();
            }
*/
            get
            {
                var colors = new List<SkinColorInfo>();
                colors.Add(new SkinColorInfo() { Name = "Blue", Color = Brushes.Blue });
                colors.Add(new SkinColorInfo() { Name = "Green", Color = Brushes.Green });
                colors.Add(new SkinColorInfo() { Name = "Red", Color = Brushes.Red });
                colors.Add(new SkinColorInfo() { Name = "Yellow", Color = Brushes.Yellow });
                return colors;
            }

        }

        #endregion
        //TextBox初始为Empty
        private string search = string.Empty;

        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<User> gridModelList;                     //已经封装好的集合列表，提供实时刷新，当做有通知的List<Student>
        public ObservableCollection<User> GridModelList//和前台要对应
        {
            get { return gridModelList ?? (gridModelList=new ObservableCollection<User>()); }
            set { gridModelList = value; RaisePropertyChanged(); }
        }


        //查询
        private DelegateCommand _queryCommand;
        public DelegateCommand QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand(ExecuteQueryCmd));

        void ExecuteQueryCmd()
        {
            var dataList = sdb.GetList().Where(it => it.Id.ToString().Contains(Search)
            || it.Name.Contains(Search)
            || it.Password.Contains(Search));
            GridModelList = new ObservableCollection<User>();
            if (dataList != null)
            {
                dataList.ToList().ForEach(o => GridModelList.Add(o));
            }
        }


    }
}
