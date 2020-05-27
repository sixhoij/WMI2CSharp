namespace WMI2CSharp.Models
{
    public class EventArgs<T>
    {
        public T Object { get; private set; }

        public EventArgs(T obj)
        {
            Object = obj;
        }
    }
}
