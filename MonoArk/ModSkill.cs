using MonoArk.Core;
using System;

namespace MonoArk
{
    /// <summary>
    /// Provides modular access to the <see cref="Skill"/> class, which stores
    /// information about each skill loaded in the game system.
    /// </summary>
    public static class ModSkill
    {
        /// <summary>
        /// Gets mod extension of type <typeparamref name="T" />. If it does not
        /// exists, constructs a default value and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the mod extension.</typeparam>
        /// <returns> The extension.</returns>
        public static T GetModSkill<T>(this Skill skill)
            where T : new()
        {
            return ((ModExtensions) typeof(Skill)
                .GetField("Extensions")
                .GetValue(skill))
                .Get<T>();
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
        public static T GetModSkill<T>(this Skill skill, Func<T> def)
        {
            return ((ModExtensions) typeof(Skill)
                .GetField("Extensions")
                .GetValue(skill))
                .Get<T>(def);
        }
    }
}
