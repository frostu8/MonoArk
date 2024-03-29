using MonoArk.Core;
using System;

namespace MonoArk
{
    /// <summary>
    /// Provides modular access to the <see cref="TempSaveData"/> class, which
    /// stores global information about each run that persists in the save file.
    /// </summary>
    public static class ModRun
    {
        /// <summary>
        /// Gets mod extension of type <typeparamref name="T" />. If it does not
        /// exists, constructs a default value and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the mod extension.</typeparam>
        /// <returns> The extension.</returns>
        public static T GetModRun<T>()
            where T : new()
        {
            return GetModExtensions(PlayData.TSavedata).Get<T>();
        }

        /// <summary>
        /// Gets mod extension of type <typeparamref name="T" />. If it does not
        /// exists, constructs a default value using the passed function and
        /// returns it.
        /// </summary>
        /// <param name="def">
        /// The function to call to construct a default value if it does not
        /// exist.
        /// </param>
        /// <typeparam name="T">The type of the mod extension.</typeparam>
        /// <returns> The extension.</returns>
        public static T GetModRun<T>(Func<T> def)
        {
            return GetModExtensions(PlayData.TSavedata).Get<T>(def);
        }

        // moved to its own function if we ever add performant extension access
        static ModExtensions GetModExtensions(TempSaveData val)
        {
            return (ModExtensions) typeof(TempSaveData)
                .GetField("Extensions")
                .GetValue(val);
        }
    }
}
