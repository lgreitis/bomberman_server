namespace GameServices.Prototype
{
    public interface IPrototypable
    {
        public abstract IPrototypable DeepCopy();
        public abstract IPrototypable ShallowCopy();
    }
}
