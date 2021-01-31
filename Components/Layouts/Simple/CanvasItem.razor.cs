using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using System.Drawing;
namespace BasicBlazorLibrary.Components.Layouts.Simple
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
                    return; //because it did not really change.  this will help with performance.
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

        //hopefully no need for sizeused since that should have been given anyways.


        //[Parameter]
        //public SizeF SizeUsed { get; set; } //hopefully this simple.

        //[Parameter]
        //public float Top { get; set; }
        //[Parameter]
        //public float Left { get; set; }

        private string GetTop()
        {
            //needs to know the viewport.

            var percents = Location.Y / Container!.ViewPort.Height;
            string tops = Container.ContainerHeight.GetLocation(percents);
            return tops;
            //string units = Container.
        }

        private string GetLeft()
        {
            var percents = Location.X / Container!.ViewPort.Width;
            string lefts = Container.ContainerWidth.GetLocation(percents);
            return lefts;

        }


    }
}