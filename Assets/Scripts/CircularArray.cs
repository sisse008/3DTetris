using System;


    public class CircularArray<T>
    {
        private readonly T[] _baseArray;
        private readonly T[] _facadeArray;
        private int _head;
        private bool _isFilled;

        public CircularArray(int length)
        {
            _baseArray = new T[length];
            _facadeArray = new T[length];
        }

        public T[] Array
        {
            get
            {
                int pos = _head;
                for (int i = 0; i < _baseArray.Length; i++)
                {
                    Math.DivRem(pos, _baseArray.Length, out pos);
                    _facadeArray[i] = _baseArray[pos];
                    pos++;
                }
                return _facadeArray;
            }
        }

        public T[] BaseArray
        {
            get { return _baseArray; }
        }

        public bool IsFilled
        {
            get { return _isFilled; }
        }

        public void Push(T value)
        {
            if (!_isFilled && _head == _baseArray.Length - 1)
                _isFilled = true;

            Math.DivRem(_head, _baseArray.Length, out _head);
            _baseArray[_head] = value;
            _head++;
        }

        public T Get(int indexBackFromHead)
        {
            int pos = _head - indexBackFromHead - 1;
            pos = pos < 0 ? pos + _baseArray.Length : pos;
            Math.DivRem(pos, _baseArray.Length, out pos);
            return _baseArray[pos];
        }
    }
