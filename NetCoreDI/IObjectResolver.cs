namespace NetCoreDI
{
    public interface IObjectResolver
    {
        TObject ResolveObject<TObject>();
    }
}