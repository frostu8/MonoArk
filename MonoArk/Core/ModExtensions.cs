using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using Plugin = MonoArk.Plugin;

namespace MonoArk.Core
{
    /// <summary>
    /// Mod extensions for classes.
    ///
    /// You should not be using this class directly, but instead using one of the
    /// extension functions for another class.
    /// </summary>
    [Serializable]
    public class ModExtensions
    {
        public class Extension : IXmlSerializable
        {
            public Type Type;
            public object Data;

            public string Name
            {
                get
                {
                    return Type.AssemblyQualifiedName;
                }
            }

            public Extension()
            { }

            public Extension(object data)
            {
                this.Type = data.GetType();
                this.Data = data;
            }

            public void WriteXml(XmlWriter writer)
            {
                //writer.WriteStartElement("Extension");
                writer.WriteAttributeString("Name", this.Name);

                var serializer = new XmlSerializer(this.Type);
                serializer.Serialize(writer, this.Data);

                //writer.WriteEndElement();
            }

            public void ReadXml(XmlReader reader)
            {
                //reader.ReadStartElement("Extension");
                if (reader.HasAttributes)
                {
                    if (reader.MoveToAttribute("Name"))
                    {
                        this.Type = Type.GetType(reader.Value);
                    }
                }

                if (this.Type != null)
                {
                    reader.Read();
                    var serializer = new XmlSerializer(this.Type);
                    this.Data = serializer.Deserialize(reader);
                }
                else
                {
                    throw new InvalidOperationException("Type not defined or missing!");
                }
            }

            public XmlSchema GetSchema()
            {
                return(null);
            }
        }

        [XmlElement(ElementName = "Extension")]
        public List<Extension> Extensions = new List<Extension>();

        public ModExtensions()
        { }

        /// <summary>
        /// Gets mod extension of type <typeparamref name="T" />. If it does not
        /// exists, constructs a default value and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the mod extension.</typeparam>
        /// <returns> The extension.</returns>
        public T Get<T>()
            where T : new()
        {
            return Get(() => new T());
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
        public T Get<T>(Func<T> def)
        {
            Type ty = typeof(T);

            // find extension
            try
            {
                return (T) this.Extensions.First(ex => ex.Name == ty.AssemblyQualifiedName).Data;
            }
            catch (InvalidOperationException)
            {
                // create new extension
                T ext = def();
                this.Extensions.Add(new Extension(ext));
                return ext;
            }
        }
    }
}

