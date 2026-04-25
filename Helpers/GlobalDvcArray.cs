using System;
using System.Collections;
using System.Collections.Generic;
using DynamicPatcher;

namespace PatcherYRpp
{
    public class GlobalDvcArray<T>
    {
        public Pointer<DynamicVectorClass<Pointer<T>>> Pointer;

        public GlobalDvcArray(nint pVector)
        {
            Pointer = pVector;
        }

        public ref DynamicVectorClass<Pointer<T>> Array => ref Pointer.Ref;

        public Pointer<T> Find(string id)
        {
            int idx = FindIndex(id);
            return idx >= 0 ? Array.Get(idx) : Pointer<T>.Zero;
        }

        public int FindIndex(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length > 23)
                return -1;

            int i = 0;
            foreach (var ptr in Array)
            {
                var pItem = ptr.Convert<AbstractTypeClass>();
                if (id.Equals(pItem.Ref.ID))
                    return i;
                i++;
            }
            return -1;
        }

        public Pointer<T> FindIgnoreCase(string id)
        {
            int idx = FindIndexIgnoreCase(id);
            return idx >= 0 ? Array.Get(idx) : Pointer<T>.Zero;
        }

        public int FindIndexIgnoreCase(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length > 23)
                return -1;

            int i = 0;
            foreach (var ptr in Array)
            {
                var pItem = ptr.Convert<AbstractTypeClass>();
                if (string.Equals(id, pItem.Ref.ID, StringComparison.OrdinalIgnoreCase))
                    return i;
                i++;
            }
            return -1;
        }

        public Pointer<T> Find(string id, StringComparer comparer)
        {
            int idx = FindIndex(id, comparer);
            return idx >= 0 ? Array.Get(idx) : Pointer<T>.Zero;
        }

        public int FindIndex(string id, StringComparer comparer)
        {
            int i = 0;
            foreach (var ptr in Array)
            {
                var pItem = ptr.Convert<AbstractTypeClass>();
                if (comparer.Equals(pItem.Ref.ID, id))
                    return i;
                i++;
            }
            return -1;
        }

        public Pointer<T> this[int index] => Array[index];
        public Pointer<T> this[string id] => Find(id);
    }
}