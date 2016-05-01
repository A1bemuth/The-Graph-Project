using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace GraphDataLayer
{
    public class ConcurrentHashSet<T> : IEnumerable<T>, IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private readonly HashSet<T> _hashSet;

        public ConcurrentHashSet()
        {
            _hashSet = new HashSet<T>();
        }

        public ConcurrentHashSet(IEqualityComparer<T> comparer)
        {
            _hashSet = new HashSet<T>(comparer);
        }

        public bool Add(T item)
        {
            return ExecuteWithWriteLock(() => _hashSet.Add(item));
        }

        private TReturn ExecuteWithWriteLock<TReturn>(Func<TReturn> function)
        {
            _lock.EnterWriteLock();
            try
            {
                return function.Invoke();
            }
            finally
            {
                if (_lock.IsWriteLockHeld)
                    _lock.ExitWriteLock();
            }

        }

        public void Clear()
        {
            ExecuteWithWriteLock(() => _hashSet.Clear());
        }

        private void ExecuteWithWriteLock(Action action)
        {
            _lock.EnterWriteLock();
            try
            {
                action.Invoke();
            }
            finally
            {
                if (_lock.IsWriteLockHeld)
                    _lock.ExitWriteLock();
            }

        }

        public bool Contains(T item)
        {
            return ExecuteWithReadLock(() => _hashSet.Contains(item));
        }

        private TReturn ExecuteWithReadLock<TReturn>(Func<TReturn> function)
        {
            _lock.EnterReadLock();
            try
            {
                return function.Invoke();
            }
            finally
            {
                if(_lock.IsReadLockHeld)
                    _lock.ExitReadLock();
            }
        }


        public bool Remove(T item)
        {
            return ExecuteWithWriteLock(() => _hashSet.Remove(item));
        }

        public int Count
        {
            get { return ExecuteWithReadLock(() => _hashSet.Count); }
        }

        public void Dispose()
        {
            _lock?.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ExecuteWithReadLock(() => _hashSet.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}