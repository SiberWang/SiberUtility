namespace SiberUtility.Systems.FileSaves
{
    public interface ISaveSystem<T>
    {
        void Save(T saveFile);

        T Load();
    }
}