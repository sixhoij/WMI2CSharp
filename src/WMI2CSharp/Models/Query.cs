using WMI2CSharp.Enums;

namespace WMI2CSharp.Models
{
    public class Query
    {
        public InformationType InformationType { get; set; }

        public string Where { get; set; }

        public Query()
        {
        }

        public Query(InformationType informationType, string where)
        {
            InformationType = informationType;
            Where = where;
        }

        public override string ToString()
        {
            return Where.ToLower().Contains("where")
                ? $"{InformationType} {Where}"
                : $"{InformationType} where {Where}";
        }
    }
}
