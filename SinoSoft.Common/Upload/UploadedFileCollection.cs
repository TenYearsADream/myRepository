namespace SinoSoft.Common.Upload
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class UploadedFileCollection : IList, ICollection, IEnumerable, ICloneable
    {
        private UploadedFile[] _array;
        private int _count;
        private const int _defaultCapacity = 0x10;
        [NonSerialized]
        private int _version;

        internal UploadedFileCollection()
        {
            this._array = null;
            this._count = 0;
            this._version = 0;
            this._array = new UploadedFile[0x10];
        }

        internal UploadedFileCollection(int capacity)
        {
            this._array = null;
            this._count = 0;
            this._version = 0;
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", capacity, "Argument cannot be negative.");
            }
            this._array = new UploadedFile[capacity];
        }

        internal UploadedFileCollection(UploadedFileCollection collection)
        {
            this._array = null;
            this._count = 0;
            this._version = 0;
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            this._array = new UploadedFile[collection.Count];
            this.AddRange(collection);
        }

        private UploadedFileCollection(Tag tag)
        {
            this._array = null;
            this._count = 0;
            this._version = 0;
        }

        internal UploadedFileCollection(UploadedFile[] array)
        {
            this._array = null;
            this._count = 0;
            this._version = 0;
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            this._array = new UploadedFile[array.Length];
            this.AddRange(array);
        }

        public virtual int Add(UploadedFile value)
        {
            if (this._count == this._array.Length)
            {
                this.EnsureCapacity(this._count + 1);
            }
            this._version++;
            this._array[this._count] = value;
            return this._count++;
        }

        public virtual void AddRange(UploadedFileCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (collection.Count != 0)
            {
                if ((this._count + collection.Count) > this._array.Length)
                {
                    this.EnsureCapacity(this._count + collection.Count);
                }
                this._version++;
                Array.Copy(collection._array, 0, this._array, this._count, collection.Count);
                this._count += collection.Count;
            }
        }

        public virtual void AddRange(UploadedFile[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (array.Length != 0)
            {
                if ((this._count + array.Length) > this._array.Length)
                {
                    this.EnsureCapacity(this._count + array.Length);
                }
                this._version++;
                Array.Copy(array, 0, this._array, this._count, array.Length);
                this._count += array.Length;
            }
        }

        public virtual int BinarySearch(UploadedFile value)
        {
            return Array.BinarySearch(this._array, 0, this._count, value);
        }

        private void CheckEnumIndex(int index)
        {
            if ((index < 0) || (index >= this._count))
            {
                throw new InvalidOperationException("Enumerator is not on a collection element.");
            }
        }

        private void CheckEnumVersion(int version)
        {
            if (version != this._version)
            {
                throw new InvalidOperationException("Enumerator invalidated by modification to collection.");
            }
        }

        private void CheckTargetArray(Array array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (array.Rank > 1)
            {
                throw new ArgumentException("Argument cannot be multidimensional.", "array");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Argument cannot be negative.");
            }
            if (arrayIndex >= array.Length)
            {
                throw new ArgumentException("Argument must be less than array length.", "arrayIndex");
            }
            if (this._count > (array.Length - arrayIndex))
            {
                throw new ArgumentException("Argument section must be large enough for collection.", "array");
            }
        }

        public virtual void Clear()
        {
            if (this._count != 0)
            {
                this._version++;
                Array.Clear(this._array, 0, this._count);
                this._count = 0;
            }
        }

        public virtual object Clone()
        {
            UploadedFileCollection files = new UploadedFileCollection(this._count);
            Array.Copy(this._array, 0, files._array, 0, this._count);
            files._count = this._count;
            files._version = this._version;
            return files;
        }

        public virtual bool Contains(UploadedFile value)
        {
            return (this.IndexOf(value) >= 0);
        }

        public virtual void CopyTo(UploadedFile[] array)
        {
            this.CheckTargetArray(array, 0);
            Array.Copy(this._array, array, this._count);
        }

        public virtual void CopyTo(UploadedFile[] array, int arrayIndex)
        {
            this.CheckTargetArray(array, arrayIndex);
            Array.Copy(this._array, 0, array, arrayIndex, this._count);
        }

        private void EnsureCapacity(int minimum)
        {
            int num = (this._array.Length == 0) ? 0x10 : (this._array.Length * 2);
            if (num < minimum)
            {
                num = minimum;
            }
            this.Capacity = num;
        }

        public virtual IEnumerator GetEnumerator()
        {
            return new IUploadedFileEnumerator(this);
        }

        public virtual int IndexOf(UploadedFile value)
        {
            return Array.IndexOf(this._array, value, 0, this._count);
        }

        public virtual void Insert(int index, UploadedFile value)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
            }
            if (index > this._count)
            {
                throw new ArgumentOutOfRangeException("index", index, "Argument cannot exceed Count.");
            }
            if (this._count == this._array.Length)
            {
                this.EnsureCapacity(this._count + 1);
            }
            this._version++;
            if (index < this._count)
            {
                Array.Copy(this._array, index, this._array, index + 1, this._count - index);
            }
            this._array[index] = value;
            this._count++;
        }

        public static UploadedFileCollection ReadOnly(UploadedFileCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return new ReadOnlyList(collection);
        }

        public virtual void Remove(UploadedFile value)
        {
            int index = this.IndexOf(value);
            if (index >= 0)
            {
                this.RemoveAt(index);
            }
        }

        public virtual void RemoveAt(int index)
        {
            this.ValidateIndex(index);
            this._version++;
            if (index < --this._count)
            {
                Array.Copy(this._array, index + 1, this._array, index, this._count - index);
            }
            this._array[this._count] = null;
        }

        public virtual void RemoveRange(int index, int count)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");
            }
            if ((index + count) > this._count)
            {
                throw new ArgumentException("Arguments denote invalid range of elements.");
            }
            if (count != 0)
            {
                this._version++;
                this._count -= count;
                if (index < this._count)
                {
                    Array.Copy(this._array, index + count, this._array, index, this._count - index);
                }
                Array.Clear(this._array, this._count, count);
            }
        }

        public virtual void Reverse()
        {
            if (this._count > 1)
            {
                this._version++;
                Array.Reverse(this._array, 0, this._count);
            }
        }

        public virtual void Reverse(int index, int count)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");
            }
            if ((index + count) > this._count)
            {
                throw new ArgumentException("Arguments denote invalid range of elements.");
            }
            if ((count > 1) && (this._count > 1))
            {
                this._version++;
                Array.Reverse(this._array, index, count);
            }
        }

        public virtual void Sort()
        {
            if (this._count > 1)
            {
                this._version++;
                Array.Sort(this._array, 0, this._count);
            }
        }

        public virtual void Sort(IComparer comparer)
        {
            if (this._count > 1)
            {
                this._version++;
                Array.Sort(this._array, 0, this._count, comparer);
            }
        }

        public virtual void Sort(int index, int count, IComparer comparer)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");
            }
            if ((index + count) > this._count)
            {
                throw new ArgumentException("Arguments denote invalid range of elements.");
            }
            if ((count > 1) && (this._count > 1))
            {
                this._version++;
                Array.Sort(this._array, index, count, comparer);
            }
        }

        public static UploadedFileCollection Synchronized(UploadedFileCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return new SyncList(collection);
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            this.CheckTargetArray(array, arrayIndex);
            this.CopyTo((UploadedFile[]) array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        int IList.Add(object value)
        {
            return this.Add((UploadedFile) value);
        }

        bool IList.Contains(object value)
        {
            return this.Contains((UploadedFile) value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((UploadedFile) value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (UploadedFile) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((UploadedFile) value);
        }

        public virtual UploadedFile[] ToArray()
        {
            UploadedFile[] destinationArray = new UploadedFile[this._count];
            Array.Copy(this._array, destinationArray, this._count);
            return destinationArray;
        }

        public virtual void TrimToSize()
        {
            this.Capacity = this._count;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
            }
            if (index >= this._count)
            {
                throw new ArgumentOutOfRangeException("index", index, "Argument must be less than Count.");
            }
        }

        public virtual int Capacity
        {
            get
            {
                return this._array.Length;
            }
            set
            {
                if (value != this._array.Length)
                {
                    if (value < this._count)
                    {
                        throw new ArgumentOutOfRangeException("Capacity", value, "Value cannot be less than Count.");
                    }
                    if (value == 0)
                    {
                        this._array = new UploadedFile[0x10];
                    }
                    else
                    {
                        UploadedFile[] destinationArray = new UploadedFile[value];
                        Array.Copy(this._array, destinationArray, this._count);
                        this._array = destinationArray;
                    }
                }
            }
        }

        public virtual int Count
        {
            get
            {
                return this._count;
            }
        }

        public virtual bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public virtual UploadedFile this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this._array[index];
            }
            set
            {
                this.ValidateIndex(index);
                this._version++;
                this._array[index] = value;
            }
        }

        public virtual object SyncRoot
        {
            get
            {
                return this;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (UploadedFile) value;
            }
        }

        [Serializable]
        private sealed class IUploadedFileEnumerator : IEnumerator
        {
            private readonly UploadedFileCollection _collection;
            private int _index;
            private readonly int _version;

            internal IUploadedFileEnumerator(UploadedFileCollection collection)
            {
                this._collection = collection;
                this._version = collection._version;
                this._index = -1;
            }

            public bool MoveNext()
            {
                this._collection.CheckEnumVersion(this._version);
                return (++this._index < this._collection.Count);
            }

            public void Reset()
            {
                this._collection.CheckEnumVersion(this._version);
                this._index = -1;
            }

            public UploadedFile Current
            {
                get
                {
                    this._collection.CheckEnumIndex(this._index);
                    this._collection.CheckEnumVersion(this._version);
                    return this._collection[this._index];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
        }

        [Serializable]
        private sealed class ReadOnlyList : UploadedFileCollection
        {
            private UploadedFileCollection _collection;

            internal ReadOnlyList(UploadedFileCollection collection) : base(UploadedFileCollection.Tag.Default)
            {
                this._collection = collection;
            }

            public override int Add(UploadedFile value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(UploadedFileCollection collection)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(UploadedFile[] array)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override int BinarySearch(UploadedFile value)
            {
                return this._collection.BinarySearch(value);
            }

            public override void Clear()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override object Clone()
            {
                return new UploadedFileCollection.ReadOnlyList((UploadedFileCollection) this._collection.Clone());
            }

            public override bool Contains(UploadedFile value)
            {
                return this._collection.Contains(value);
            }

            public override void CopyTo(UploadedFile[] array)
            {
                this._collection.CopyTo(array);
            }

            public override void CopyTo(UploadedFile[] array, int arrayIndex)
            {
                this._collection.CopyTo(array, arrayIndex);
            }

            public override IEnumerator GetEnumerator()
            {
                return this._collection.GetEnumerator();
            }

            public override int IndexOf(UploadedFile value)
            {
                return this._collection.IndexOf(value);
            }

            public override void Insert(int index, UploadedFile value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Remove(UploadedFile value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void RemoveAt(int index)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void RemoveRange(int index, int count)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Reverse()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Reverse(int index, int count)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Sort()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Sort(IComparer comparer)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Sort(int index, int count, IComparer comparer)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override UploadedFile[] ToArray()
            {
                return this._collection.ToArray();
            }

            public override void TrimToSize()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override int Capacity
            {
                get
                {
                    return this._collection.Capacity;
                }
                set
                {
                    throw new NotSupportedException("Read-only collections cannot be modified.");
                }
            }

            public override int Count
            {
                get
                {
                    return this._collection.Count;
                }
            }

            public override bool IsFixedSize
            {
                get
                {
                    return true;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public override bool IsSynchronized
            {
                get
                {
                    return this._collection.IsSynchronized;
                }
            }

            public override UploadedFile this[int index]
            {
                get
                {
                    return this._collection[index];
                }
                set
                {
                    throw new NotSupportedException("Read-only collections cannot be modified.");
                }
            }

            public override object SyncRoot
            {
                get
                {
                    return this._collection.SyncRoot;
                }
            }
        }

        [Serializable]
        private sealed class SyncList : UploadedFileCollection
        {
            private UploadedFileCollection _collection;
            private object _root;

            internal SyncList(UploadedFileCollection collection) : base(UploadedFileCollection.Tag.Default)
            {
                this._root = collection.SyncRoot;
                this._collection = collection;
            }

            public override int Add(UploadedFile value)
            {
                lock (this._root)
                {
                    return this._collection.Add(value);
                }
            }

            public override void AddRange(UploadedFileCollection collection)
            {
                lock (this._root)
                {
                    this._collection.AddRange(collection);
                }
            }

            public override void AddRange(UploadedFile[] array)
            {
                lock (this._root)
                {
                    this._collection.AddRange(array);
                }
            }

            public override int BinarySearch(UploadedFile value)
            {
                lock (this._root)
                {
                    return this._collection.BinarySearch(value);
                }
            }

            public override void Clear()
            {
                lock (this._root)
                {
                    this._collection.Clear();
                }
            }

            public override object Clone()
            {
                lock (this._root)
                {
                    return new UploadedFileCollection.SyncList((UploadedFileCollection) this._collection.Clone());
                }
            }

            public override bool Contains(UploadedFile value)
            {
                lock (this._root)
                {
                    return this._collection.Contains(value);
                }
            }

            public override void CopyTo(UploadedFile[] array)
            {
                lock (this._root)
                {
                    this._collection.CopyTo(array);
                }
            }

            public override void CopyTo(UploadedFile[] array, int arrayIndex)
            {
                lock (this._root)
                {
                    this._collection.CopyTo(array, arrayIndex);
                }
            }

            public override IEnumerator GetEnumerator()
            {
                lock (this._root)
                {
                    return this._collection.GetEnumerator();
                }
            }

            public override int IndexOf(UploadedFile value)
            {
                lock (this._root)
                {
                    return this._collection.IndexOf(value);
                }
            }

            public override void Insert(int index, UploadedFile value)
            {
                lock (this._root)
                {
                    this._collection.Insert(index, value);
                }
            }

            public override void Remove(UploadedFile value)
            {
                lock (this._root)
                {
                    this._collection.Remove(value);
                }
            }

            public override void RemoveAt(int index)
            {
                lock (this._root)
                {
                    this._collection.RemoveAt(index);
                }
            }

            public override void RemoveRange(int index, int count)
            {
                lock (this._root)
                {
                    this._collection.RemoveRange(index, count);
                }
            }

            public override void Reverse()
            {
                lock (this._root)
                {
                    this._collection.Reverse();
                }
            }

            public override void Reverse(int index, int count)
            {
                lock (this._root)
                {
                    this._collection.Reverse(index, count);
                }
            }

            public override void Sort()
            {
                lock (this._root)
                {
                    this._collection.Sort();
                }
            }

            public override void Sort(IComparer comparer)
            {
                lock (this._root)
                {
                    this._collection.Sort(comparer);
                }
            }

            public override void Sort(int index, int count, IComparer comparer)
            {
                lock (this._root)
                {
                    this._collection.Sort(index, count, comparer);
                }
            }

            public override UploadedFile[] ToArray()
            {
                lock (this._root)
                {
                    return this._collection.ToArray();
                }
            }

            public override void TrimToSize()
            {
                lock (this._root)
                {
                    this._collection.TrimToSize();
                }
            }

            public override int Capacity
            {
                get
                {
                    lock (this._root)
                    {
                        return this._collection.Capacity;
                    }
                }
                set
                {
                    lock (this._root)
                    {
                        this._collection.Capacity = value;
                    }
                }
            }

            public override int Count
            {
                get
                {
                    lock (this._root)
                    {
                        return this._collection.Count;
                    }
                }
            }

            public override bool IsFixedSize
            {
                get
                {
                    return this._collection.IsFixedSize;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return this._collection.IsReadOnly;
                }
            }

            public override bool IsSynchronized
            {
                get
                {
                    return true;
                }
            }

            public override UploadedFile this[int index]
            {
                get
                {
                    lock (this._root)
                    {
                        return this._collection[index];
                    }
                }
                set
                {
                    lock (this._root)
                    {
                        this._collection[index] = value;
                    }
                }
            }

            public override object SyncRoot
            {
                get
                {
                    return this._root;
                }
            }
        }

        private enum Tag
        {
            Default
        }
    }
}

