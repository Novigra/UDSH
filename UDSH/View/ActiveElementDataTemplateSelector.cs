using System.Windows;
using System.Windows.Controls;
using UDSH.Model;

namespace UDSH.View
{
    class ActiveElementDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SceneHeadingTemplate { get; set; }
        public DataTemplate ActionTemplate { get; set; }
        public DataTemplate CharacterTemplate { get; set; }
        public DataTemplate DialogueTemplate { get; set; }
        public DataTemplate BranchNodeTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var Data = item as CSFData;

            if(Data.DataType == "Scene Heading")
                return SceneHeadingTemplate;
            else if(Data.DataType == "Action")
                return ActionTemplate;
            else if(Data.DataType == "Character")
                return CharacterTemplate;
            else if(Data.DataType == "Dialogue")
                return DialogueTemplate;
            else if(Data.DataType == "BranchNodeTemplate")
                return BranchNodeTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
