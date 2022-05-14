using System.Collections.Generic;

namespace Tense.Rql
{
	internal class QueueStack<T> where T : class
	{
		private readonly List<T> _theList = new();

		public void Push(T? item)
		{
			if ( item != null )
				_theList.Insert(0, item);
		}

		public T Lookforward(int n)
		{
			var index = _theList.Count - n - 1;

			return _theList[index];
		}

		public T? Pop()
		{
			if (_theList.Count == 0)
				return default;

			var item = _theList[0];
			_theList.RemoveAt(0);
			return item;
		}

		public void Put(T? item)
		{
			if ( item != null)
				_theList.Add(item);
		}

		public T? Pull()
		{
			if (_theList.Count == 0)
				return default;

			var item = _theList[^1];
			_theList.RemoveAt(_theList.Count - 1);
			return item;
		}

		public T? Peek()
		{
			if (_theList.Count == 0)
				return default;

			return _theList[^1];
		}

		public int Count { get { return _theList.Count; } }

		public void Clear()
		{
			_theList.Clear();
		}
	}
}
