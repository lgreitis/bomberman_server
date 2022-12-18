using System;
namespace GameServices.Iterator
{
	public abstract class Iterator
	{
        public abstract object First();
        public abstract object? Next();
    }
}

