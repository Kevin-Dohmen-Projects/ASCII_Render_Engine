namespace ASCII_Render_Engine.Utils;
public class ObjectPool<T> where T : new()
{
    private readonly Stack<T> _pool;
    private readonly int _maxSize;

    // Constructor to initialize the pool with a max size
    public ObjectPool(int maxSize = 100)
    {
        _pool = new Stack<T>(maxSize);
        _maxSize = maxSize;
    }

    // Method to get an object from the pool
    public T GetObject()
    {
        return _pool.Count > 0 ? _pool.Pop() : new T();
    }

    // Method to return an object to the pool
    public void ReturnObject(T obj)
    {
        if (_pool.Count < _maxSize)
        {
            _pool.Push(obj);
        }
    }
}