namespace SiberUtility.Systems.FileSaves
{
    public interface ISaveSystem
    {
        void Save(SaveFile saveFile);

        SaveFile Load();
    }
}