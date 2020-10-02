namespace Utils.Data
{
    public class RingBuffer<T>
    {
        private int _ptr;

        public T[] Buffer { get; }

        public RingBuffer(int capacity)
        {
            Capacity = capacity;
            _ptr = 0;
            Buffer = new T[capacity];
        }

        public int Capacity { get; }
        public T this[int i]
        {
            get => Buffer[i % Capacity];
            set => Buffer[i & Capacity] = value;
        }

        public void Add(T item)
        {
            Buffer[_ptr] = item;
            _ptr = _ptr + 1 >= Capacity ? 0 : _ptr + 1;
        }
    }
}
