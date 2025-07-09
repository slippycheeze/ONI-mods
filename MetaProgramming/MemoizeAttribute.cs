namespace SlippyCheeze.MetaProgramming;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class MemoizeAttribute(): Attribute;
