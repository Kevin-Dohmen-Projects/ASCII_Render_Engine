namespace ASCII_Render_Engine.Utils;

/// <summary>
/// A generic object pool implementation to manage reusable objects.
/// </summary>
/// <typeparam name="T">The type of objects to be pooled. Must have a parameterless constructor.</typeparam>
public class ObjectPool<T> where T : new()
{
    private readonly Stack<T> _pool;
    private readonly int _maxSize;

    private Func<T>? _initFunc = null;
    private Func<T, T>? _clearFunc = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class with a specified maximum size.
    /// </summary>
    /// <param name="maxSize">The maximum number of objects the pool can hold.</param>
    public ObjectPool(int maxSize = 100)
    {
        _pool = new Stack<T>(maxSize);
        _maxSize = maxSize;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class with a specified initialization function and maximum size.
    /// </summary>
    /// <param name="initFunc">A function to initialize new objects when the pool is empty.</param>
    /// <param name="maxSize">The maximum number of objects the pool can hold.</param>
    public ObjectPool(Func<T> initFunc, int maxSize = 100)
    {
        _pool = new Stack<T>(maxSize);
        _maxSize = maxSize;
        _initFunc = initFunc;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class with specified initialization and clearing functions, and a maximum size.
    /// </summary>
    /// <param name="initFunc">A function to initialize new objects when the pool is empty.</param>
    /// <param name="clearFunc">A function to reset objects before they are returned to the pool.</param>
    /// <param name="maxSize">The maximum number of objects the pool can hold.</param>
    public ObjectPool(Func<T> initFunc, Func<T, T> clearFunc, int maxSize = 100)
    {
        _pool = new Stack<T>(maxSize);
        _maxSize = maxSize;
        _initFunc = initFunc;
        _clearFunc = clearFunc;
    }

    /// <summary>
    /// Retrieves an object from the pool. If the pool is empty, a new object is created.
    /// </summary>
    /// <returns>An object of type <typeparamref name="T"/>.</returns>
    public T GetObject()
    {
        T obj;
        obj = _pool.Count > 0 ? _pool.Pop() : (_initFunc != null ? _initFunc() : new T());
        obj = _clearFunc != null ? _clearFunc(obj) : obj;
        return obj;
    }

    /// <summary>
    /// Returns an object to the pool. If the pool is full, the object is discarded.
    /// </summary>
    /// <param name="obj">The object to return to the pool.</param>
    public void ReturnObject(T obj)
    {
        if (_pool.Count < _maxSize)
        {
            _pool.Push(obj);
        }
    }
}
