using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DynamicPatcher;

namespace PatcherYRpp
{
    #region 核心类型：Index（适配FixedArray的索引类型）
    /// <summary>
    /// 表示数组/集合的索引（支持正向/反向索引）
    /// </summary>
    public readonly struct Index : IEquatable<Index>
    {
        /// <summary>
        /// 索引值
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// 是否从末尾开始计算
        /// </summary>
        public bool IsFromEnd { get; }

        /// <summary>
        /// 构造索引
        /// </summary>
        /// <param name="value">索引值</param>
        /// <param name="isFromEnd">是否从末尾开始</param>
        /// <exception cref="ArgumentOutOfRangeException">索引值为负时抛出</exception>
        public Index(int value, bool isFromEnd = false)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "索引值不能为负数");

            Value = value;
            IsFromEnd = isFromEnd;
        }

        /// <summary>
        /// 获取索引在指定长度集合中的实际偏移量
        /// </summary>
        /// <param name="length">集合长度</param>
        /// <returns>实际偏移量</returns>
        /// <exception cref="ArgumentOutOfRangeException">长度为负时抛出</exception>
        public int GetOffset(int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "集合长度不能为负数");

            return IsFromEnd ? length - Value : Value;
        }

        /// <summary>
        /// 创建起始索引（从0开始）
        /// </summary>
        /// <param name="value">索引值</param>
        /// <returns>起始索引实例</returns>
        public static Index Start(int value) => new(value, false);

        /// <summary>
        /// 创建末尾索引（从末尾开始）
        /// </summary>
        /// <param name="value">索引值</param>
        /// <returns>末尾索引实例</returns>
        public static Index End(int value) => new(value, true);

        #region 相等性判断（移除HashCode依赖）
        public bool Equals(Index other)
        {
            return Value == other.Value && IsFromEnd == other.IsFromEnd;
        }

        public override bool Equals(object obj)
        {
            return obj is Index other && Equals(other);
        }

        /// <summary>
        /// 传统哈希计算方式（兼容所有.NET版本）
        /// </summary>
        /// <returns>哈希值</returns>
        public override int GetHashCode()
        {
            unchecked // 防止溢出
            {
                int hash = 17;
                hash = hash * 23 + Value.GetHashCode();
                hash = hash * 23 + IsFromEnd.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Index left, Index right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Index left, Index right)
        {
            return !left.Equals(right);
        }
        #endregion

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>索引字符串表示</returns>
        public override string ToString()
        {
            return IsFromEnd ? $"^{Value}" : Value.ToString();
        }
    }
    #endregion

    #region 核心类型：Range（适配FixedArray的范围类型）
    /// <summary>
    /// 表示数组/集合的范围（由起始和结束Index组成）
    /// </summary>
    public readonly struct Range : IEquatable<Range>
    {
        /// <summary>
        /// 起始索引（包含）
        /// </summary>
        public Index Start { get; }

        /// <summary>
        /// 结束索引（不包含）
        /// </summary>
        public Index End { get; }

        /// <summary>
        /// 构造范围
        /// </summary>
        /// <param name="start">起始索引</param>
        /// <param name="end">结束索引</param>
        public Range(Index start, Index end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// 获取范围在指定长度集合中的实际偏移量和长度
        /// </summary>
        /// <param name="length">集合长度</param>
        /// <returns>(偏移量, 范围长度)</returns>
        /// <exception cref="ArgumentOutOfRangeException">长度为负时抛出</exception>
        public (int Offset, int Length) GetOffsetAndLength(int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "集合长度不能为负数");

            int startOffset = Start.GetOffset(length);
            int endOffset = End.GetOffset(length);

            if (startOffset < 0)
                throw new ArgumentOutOfRangeException(nameof(Start), "起始索引偏移量不能为负");
            if (endOffset > length)
                throw new ArgumentOutOfRangeException(nameof(End), "结束索引偏移量不能超过集合长度");
            if (startOffset > endOffset)
                throw new ArgumentException("起始索引偏移量不能大于结束索引偏移量");

            return (startOffset, endOffset - startOffset);
        }

        #region 相等性判断（移除HashCode依赖）
        public bool Equals(Range other)
        {
            return Start.Equals(other.Start) && End.Equals(other.End);
        }
        
        public override bool Equals( object obj) 
        {
            return obj is Range other && Equals(other);
        }

        /// <summary>
        /// 传统哈希计算方式（兼容所有.NET版本）
        /// </summary>
        /// <returns>哈希值</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Start.GetHashCode();
                hash = hash * 23 + End.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Range left, Range right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Range left, Range right)
        {
            return !left.Equals(right);
        }
        #endregion

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>范围字符串表示</returns>
        public override string ToString()
        {
            return $"{Start}..{End}";
        }
    }
    #endregion

    #region 核心类型：FixedArray<T>（基于指针的固定长度数组）
    /// <summary>
    /// 基于指针的固定长度数组（兼容.NET系统集合接口）
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    [DebuggerDisplay("Length = {Length}, Pointer = {Ptr}")]
    public struct FixedArray<T> : IReadOnlyList<T>, IEnumerable<T>
    {
        /// <summary>
        /// 底层指针（使用你已实现的Pointer<T>）
        /// </summary>
        public readonly Pointer<T> Ptr;

        /// <summary>
        /// 数组长度
        /// </summary>
        public readonly int Length;

        /// <summary>
        /// 构造函数（从指针和长度创建）
        /// </summary>
        /// <param name="ptr">底层指针</param>
        /// <param name="length">数组长度</param>
        /// <exception cref="ArgumentOutOfRangeException">长度≤0时抛出</exception>
        /// <exception cref="ArgumentNullException">指针为空时抛出</exception>
        public FixedArray(Pointer<T> ptr, int length)
        {
            // 修复：避免 == 运算符二义性，使用 Equals 或 IsNull 属性
            if (ptr.Equals(default(Pointer<T>)))
                throw new ArgumentNullException(nameof(ptr), "指针不能为空");
            
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "长度必须大于0");

            Ptr = ptr;
            Length = length;
        }

        /// <summary>
        /// 构造函数（从引用和长度创建）
        /// </summary>
        /// <param name="ptr">元素引用</param>
        /// <param name="length">数组长度</param>
        public FixedArray(ref T ptr, int length) : this(Pointer<T>.AsPointer(ref ptr), length) { }

        /// <summary>
        /// 索引器（可写，按int索引）
        /// </summary>
        /// <param name="index">索引位置</param>
        /// <returns>元素引用</returns>
        /// <exception cref="IndexOutOfRangeException">索引越界时抛出</exception>
        public ref T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return ref Ptr[index];
            }
        }

        /// <summary>
        /// 索引器（读写，按Index类型索引）
        /// </summary>
        /// <param name="index">Index类型索引</param>
        /// <returns>元素值</returns>
        /// <exception cref="IndexOutOfRangeException">索引越界时抛出</exception>
        public T this[Index index]
        {
            get
            {
                int offset = index.GetOffset(Length);
                ValidateIndex(offset);
                return Ptr[offset];
            }
            set
            {
                int offset = index.GetOffset(Length);
                ValidateIndex(offset);
                Ptr[offset] = value;
            }
        }

        /// <summary>
        /// 范围索引器（返回子数组）
        /// </summary>
        /// <param name="range">范围</param>
        /// <returns>子FixedArray</returns>
        /// <exception cref="IndexOutOfRangeException">范围越界时抛出</exception>
        public FixedArray<T> this[Range range]
        {
            get
            {
                (int startOffset, int subLength) = range.GetOffsetAndLength(Length);
                ValidateIndex(startOffset);
                if (subLength > 0)
                    ValidateIndex(startOffset + subLength - 1);

                return new FixedArray<T>(Ptr + startOffset, subLength);
            }
        }

        #region 系统集合接口实现（IReadOnlyList<T>）
        int IReadOnlyCollection<T>.Count => Length;

        T IReadOnlyList<T>.this[int index] => this[index];
        #endregion

        #region 枚举器（兼容foreach，遵循.NET规范）
        /// <summary>
        /// 获取值类型枚举器（无GC分配）
        /// </summary>
        /// <returns>值类型枚举器</returns>
        public Enumerator GetEnumerator() => new Enumerator(Ptr, Length);

        /// <summary>
        /// 非泛型枚举器（兼容IEnumerable）
        /// </summary>
        /// <returns>非泛型枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 泛型枚举器（兼容IEnumerable<T>）
        /// </summary>
        /// <returns>泛型枚举器</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new EnumeratorWrapper(GetEnumerator());

        /// <summary>
        /// 值类型枚举器（高性能，无GC）
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private readonly Pointer<T> _ptr;
            private readonly int _length;
            private int _index;

            /// <summary>
            /// 当前元素
            /// </summary>
            /// <exception cref="InvalidOperationException">枚举器未启动/已结束枚举</exception>
            public T Current
            {
                get
                {
                    if (_index < 0 || _index >= _length)
                        throw new InvalidOperationException("枚举器未启动或已结束枚举");
                    return _ptr[_index];
                }
            }

            object IEnumerator.Current => Current!;

            internal Enumerator(Pointer<T> ptr, int length)
            {
                _ptr = ptr;
                _length = length;
                _index = -1; // 初始状态：未开始枚举
            }

            /// <summary>
            /// 移动到下一个元素
            /// </summary>
            /// <returns>是否成功移动</returns>
            public bool MoveNext()
            {
                if (_index >= _length - 1)
                    return false;

                _index++;
                return true;
            }

            /// <summary>
            /// 重置枚举器
            /// </summary>
            public void Reset() => _index = -1;

            /// <summary>
            /// 释放资源（值类型枚举器空实现）
            /// </summary>
            public void Dispose() { }
        }

        /// <summary>
        /// 枚举器包装器（适配IEnumerator<T>接口）
        /// </summary>
        private sealed class EnumeratorWrapper : IEnumerator<T>
        {
            private Enumerator _enumerator;

            public T Current => _enumerator.Current;
            object IEnumerator.Current => Current!;

            public EnumeratorWrapper(Enumerator enumerator) => _enumerator = enumerator;

            public bool MoveNext() => _enumerator.MoveNext();
            public void Reset() => _enumerator.Reset();
            public void Dispose() { }
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 验证索引有效性
        /// </summary>
        /// <param name="index">要验证的索引</param>
        /// <exception cref="IndexOutOfRangeException">索引越界时抛出</exception>
        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= Length)
                throw new IndexOutOfRangeException($"索引 {index} 超出范围（0 ~ {Length - 1}）");
        }

        /// <summary>
        /// 转换为原生数组（副本）
        /// </summary>
        /// <returns>原生数组副本</returns>
        public T[] ToArray()
        {
            T[] array = new T[Length];
            for (int i = 0; i < Length; i++)
                array[i] = Ptr[i];
            return array;
        }
        #endregion
    }
    #endregion

    #region 工具类：MathUtilities（通用数学工具）
    /// <summary>
    /// 数学工具类
    /// </summary>
    public static class MathUtilities
    {
        /// <summary>
        /// 获取两个整数的最小值和最大值
        /// </summary>
        /// <param name="a">第一个数</param>
        /// <param name="b">第二个数</param>
        /// <returns>(最小值, 最大值)</returns>
        public static (int Min, int Max) GetMinMax(int a, int b) => (Math.Min(a, b), Math.Max(a, b));
    }
    #endregion
}