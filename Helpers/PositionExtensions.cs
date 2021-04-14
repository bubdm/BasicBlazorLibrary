using CommonBasicLibraries.CollectionClasses;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using System;
using System.Drawing;
namespace BasicBlazorLibrary.Helpers
{
    public static class PositionExtensions
    {
        private readonly static BasicList<string> _units = new () { "rem", "em", "px", "vw", "vh", "vmin", "vmax", "%" };

        public static string UnitOfMeasureString(this string size)
        {
            foreach (var unit in _units)
            {
                if (size.EndsWith(unit))
                {
                    return unit;
                }
            }
            throw new CustomBasicException("No unit measure found");
        }
        public static string GetLocation(this string size, Single percents)
        {
            string unit = size.UnitOfMeasureString();
            var subs = size.SizeUsed(unit);
            var total = subs * percents;
            return $"{total}{unit}";
        }
        public static float SizeUsed(this string size)
        {
            string unit = size.UnitOfMeasureString();
            return SizeUsed(unit);
        }
        private static float SizeUsed(this string size, string unit)
        {
            string results = size.Replace(unit, "");
            bool rets = float.TryParse(results, out float output);
            if (rets == false)
            {
                throw new CustomBasicException($"Size {size} incorrect format");
            }
            return output;
        }
        public static string ContainerSize(this string individualSize, float howMany) //to be more flexible
        {
            string unit = individualSize.UnitOfMeasureString();
            float used = individualSize.SizeUsed(unit);
            float total = used * howMany;
            return $"{total}{unit}";
        }
        public static string ElementLocation(this string individualSize, int proposedLocation)
        {
            string unit = individualSize.UnitOfMeasureString();
            return $"{proposedLocation}{unit}";
        }
        private static float GetWidth(this float height, SizeF ratio)
        {
            float output = height * ratio.Width / ratio.Height;
            return output;
        }
        private static float GetHeight(this float width, SizeF ratio)
        {
            float output = width * ratio.Height / ratio.Width;
            return output;
        }
        public static string ContainerWidth(this string elementHeight, int columns, SizeF elementRatio)
        {
            string unit = elementHeight.UnitOfMeasureString();
            float height = elementHeight.SizeUsed(unit);
            float singleWidth = GetWidth(height, elementRatio);
            float totalWidth = singleWidth * columns;
            return $"{totalWidth}{unit}";
        }
        public static string ContainerHeight(this string elementWidth, int rows, SizeF elementRatio)
        {
            string unit = elementWidth.UnitOfMeasureString();
            float width = elementWidth.SizeUsed(unit);

            float singleHeight = GetHeight(width, elementRatio);
            float totalHeight = singleHeight * rows;
            return $"{totalHeight}{unit}";
        }
    }
}