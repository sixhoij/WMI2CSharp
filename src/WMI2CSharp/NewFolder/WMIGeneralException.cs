using System;

namespace WMI2CSharp.Exceptions
{
    [Serializable]
    public class WMIGeneralException : Exception
    {
        public WMIGeneralException()
        {
        }

        public WMIGeneralException(string endPoint, string data, Exception exception)
            : base($"{endPoint} failed with {data}", exception)
        {
        }

        public WMIGeneralException(string endPoint, Exception exception)
            : base(endPoint, exception)
        {
        }
    }
}
