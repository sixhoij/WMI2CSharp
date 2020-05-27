using System;
using WMI2CSharp.Enums;

namespace WMI2CSharp.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InformationCategoryAttribute : Attribute
    {
        public InformationCategory[] InformationCategories { get; }

        public InformationCategoryAttribute(params InformationCategory[] informationCategories)
        {
            InformationCategories = informationCategories;
        }
    }
}
