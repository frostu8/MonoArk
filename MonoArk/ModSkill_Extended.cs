using MonoArk.Core;
using System;

namespace MonoArk
{
    /// <summary>
    /// Provides modular access to the <see cref="Skill_Extended"/> class, which
    /// stores information about each skill extension loaded in the game.
    /// </summary>
    public static class ModSkill_Extended
    {
        /// <summary>
        /// Gets mod extension of type <typeparamref name="T" />. If it does not
        /// exists, constructs a default value and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the mod extension.</typeparam>
        /// <returns> The extension.</returns>
        public static T GetModSkill_Extended<T>(this Skill_Extended skill)
            where T : new()
        {
            return GetModExtensions(skill).Get<T>();
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
        public static T GetModSkill_Extended<T>(this Skill_Extended skill, Func<T> def)
        {
            return GetModExtensions(skill).Get<T>(def);
        }

        // moved to its own function if we ever add performant extension access
        static ModExtensions GetModExtensions(Skill_Extended val)
        {
            return (ModExtensions) typeof(Skill_Extended)
                .GetField("Extensions")
                .GetValue(val);
        }
    }
}
