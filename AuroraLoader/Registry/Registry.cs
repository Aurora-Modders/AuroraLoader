namespace AuroraLoader.Registry
{
    public interface IRegistry
    {
        /// <summary>
        /// Registries must be initialized by calling Update().
        /// Their update method should call the Update() method of any of their own dependencies
        /// Registry constructors should not call Update().
        /// </summary>
        void Update();
    }
}
