using System;

public static class EventExtensions
{
	public static void Raise(this EventHandler handler, object sender)
	{
		handler?.Invoke(sender, EventArgs.Empty);
	}
	public static void Raise<T>(this EventHandler<T> handler, object sender, T args)
	{
		handler?.Invoke(sender, args);
	}
}