using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using System.Drawing;
namespace BasicBlazorLibrary.Components.Layouts
{
    public partial class CanvasItem
    {
        [CascadingParameter]
        public CanvasLayout? Container { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private PointF _location; //if its 0, 0, then should be okay obviously.
        [Parameter]
        public PointF Location
        {
            get { return _location;  }
            set
            {
                if (value.X == _location.X && value.Y == _location.Y)
                {
                    return;
                }
                if (Container != null)
                {
                    SetText();
                }
                _location = value;
            }
        }
        private string _topText = "";
        private string _leftText = "";
        protected override void OnInitialized()
        {
            SetText();
            base.OnInitialized();
        }
        private void SetText()
        {
            _topText = GetTop();
            _leftText = GetLeft();
        }
        private string GetTop()
        {
            var percents = Location.Y / Container!.ViewPort.Height;
            string tops = Container.ContainerHeight.GetLocation(percents);
            return tops;
        }
        private string GetLeft()
        {
            var percents = Location.X / Container!.ViewPort.Width;
            string lefts = Container.ContainerWidth.GetLocation(percents);
            return lefts;
        }
    }
}