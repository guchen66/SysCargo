
namespace 仓库管理系统.Global
{
    public enum MenuTitle
    {
        One,
        Two,
        Three, 
        Four,
        Five,
        Six,
        Seven,
    }

    public class MenuDict
    {
        public readonly string One = "首页";
        public readonly string Two = "设置";
        public readonly string Three = "用户信息";
        public readonly string Four = "仓库汇总";
        public readonly string Five = "入库信息";
        public readonly string Six = "出库信息";
        public readonly string Seven = "智能报警";


        Dictionary<string, string> _dict = new Dictionary<string, string>();
        
        public Dictionary<string, string> GetDict()
        {
            _dict.Add("one", One);
            _dict.Add("two", Two);
            _dict.Add("three", Three);
            _dict.Add("four", Four);
            _dict.Add("five", Five);
            _dict.Add("six", Six);
            _dict.Add("seven",Seven);
            return _dict;
        }
    }
}
