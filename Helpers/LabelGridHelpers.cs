using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicBlazorLibrary.Helpers
{
    public static class LabelGridHelpers
    {
        public static CustomBasicList<LabelGridModel> AddLabel(this CustomBasicList<LabelGridModel> labels, string header, string value)
        {
            labels.Add(new LabelGridModel(header, value));
            return labels;
        }
    }
}