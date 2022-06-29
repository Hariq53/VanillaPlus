namespace VanillaPlus.Common.Models.Config
{
    public abstract class ElementConfig
    {
        internal virtual ElementConfig? SuperConfig { get; set; }

        public static bool IsEnabled(ElementConfig? config)
        {
            if (config is null)
                return false;
            return config.IsEnabled();
        }

        public abstract bool IsEnabled();

        public virtual bool CanOverride(ElementConfig subConfig) => !IsEnabled();
    }
}
