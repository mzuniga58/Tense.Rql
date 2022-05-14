using System;
using System.Text;

namespace Tense.Rql
{
	internal class MemoryStreamReader : IDisposable
	{
		private readonly StringBuilder _stream;

		public MemoryStreamReader(string input)
		{
			_stream = new StringBuilder(input);
			Position = 0;
		}

		public bool EndOfStream
		{
			get { return Position >= _stream.Length; }
		}

		public char Peek()
		{
			return _stream[Position];
		}

		public char? LookForward(int Index)
		{
			if (Position + Index >= _stream.Length)
				return null;

			return _stream[Position + Index];
		}

		public char Read()
		{
			return _stream[Position++];
		}

		public int Position { get; set; }

		public void Close()
		{
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		~MemoryStreamReader()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(false);
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
