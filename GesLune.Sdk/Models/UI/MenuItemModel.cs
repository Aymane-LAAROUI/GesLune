using System.Windows.Input;

namespace GesLune.Models.UI
{
    public class MenuItemModel
    {
        public string Text { get; set; } = string.Empty;
        public List<Object> Items { get; set;} = [];
        public ICommand? Command { get; set; }
        public Object? Tag { get; set; }
    }
}
