namespace ASCII_Render_Engine.Utils;

public class ObjectPool<T> where T : new()
{
    private readonly Stack<T> _pool;
    private readonly int _maxSize;

    private Func<T>? _initFunc = null;
    private Func<T, T>? _clearFunc = null;

    public ObjectPool(int maxSize = 100)
    {
        _pool = new Stack<T>(maxSize);
        _maxSize = maxSize;
    }

    public ObjectPool(Func<T> initFunc, int maxSize = 100)
    {
        _pool = new Stack<T>(maxSize);
        _maxSize = maxSize;
        _initFunc = initFunc;
    }

    public ObjectPool(Func<T> initFunc, Func<T, T> clearFunc, int maxSize = 100)
    {
        _pool = new Stack<T>(maxSize);
        _maxSize = maxSize;
        _initFunc = initFunc;
        _clearFunc = clearFunc;
    }

    public T GetObject()
    {
        T obj;
        obj = _pool.Count > 0 ? _pool.Pop() : (_initFunc != null ? _initFunc() : new T());
        obj = _clearFunc != null ? _clearFunc(obj) : obj;
        return obj;
    }

    public void ReturnObject(T obj)
    {
        if (_pool.Count < _maxSize)
        {
            _pool.Push(obj);
        }
    }
}
