using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LexiGame.Utility
{
    public class UserSection : ConfigurationSection
    {
        internal UserSection()
            : base()
        {

        }
        public override bool IsReadOnly()
        {
            return false;
        }
        [ConfigurationProperty("ConnectionStringName")]
        public string ConnectionStringName
        {
            get { return ConfigurationManager.ConnectionStrings[(string)base["ConnectionStringName"]].ConnectionString; }
            set { base["ConnectionStringName"] = value; }
        }
        [ConfigurationProperty("dbProviderName", DefaultValue = "InMemoryProvider", IsRequired = true, IsKey = false)]
        public string ProviderName
        {
            get
            {
                return (string)this["dbProviderName"];
            }
            set
            {
                this["dbProviderName"] = value;

            }

        }

        [ConfigurationProperty("profileSettings")]
        public ProfileSettings Profile
        {
            get
            {
                return (ProfileSettings)base["profileSettings"];
            }
        }
        [ConfigurationProperty("skins", IsDefaultCollection = false)]
        public SkinCollection Skins
        {
            get
            {
                SkinCollection skins = (SkinCollection)base["skins"];
                return skins;
            }
        }

        [ConfigurationProperty("languages", IsDefaultCollection = false)]
        public LanguageCollection Languages
        {
            get
            {
                LanguageCollection languages = (LanguageCollection)base["languages"];
                return languages;
            }
        }
        [ConfigurationProperty("dbProviders", IsDefaultCollection = false)]
        public DBProviderCollection DBProviders
        {
            get
            {
                DBProviderCollection providers = (DBProviderCollection)base["dbProviders"];
                return providers;
            }
        }

    }
    public class ProfileSettings : ConfigurationElement
    {
        [ConfigurationProperty("ThemeSelected")]
        public string ThemeSelected
        {
            get
            {
                return (string)this["ThemeSelected"];
            }
            set
            {
                this["ThemeSelected"] = value;
            }
        }
        [ConfigurationProperty("Range")]
        public int Range
        {
            get
            {
                try
                {
                    return (int)this["Range"];

                }
                catch (Exception ex)
                {
                    throw new Exception("Inappropriate range attribute value in .config ", ex);
                }
            }
            set
            {
                this["Range"] = value;
            }
        }
        [ConfigurationProperty("Step")]
        public int Step
        {
            get
            {
                try
                {
                    return (int)this["Step"];
                }
                catch (Exception ex)
                {
                    throw new Exception("Inappropriate Step attribute value in .config", ex);
                }
            }
            set
            {
                this["Step"] = value;
            }
        }
        [ConfigurationProperty("Appearance")]
        public string Appearance
        {
            get
            {
                return (string)this["Appearance"];
            }
            set
            {
                this["Appearance"] = value;
            }
        }
        [ConfigurationProperty("Language")]
        public string Language
        {
            get
            {
                return (string)this["Language"];
            }
            set
            {
                this["Language"] = value;
            }
        }
    }
    public class SkinCollection : ConfigurationElementCollection
    {
        public SkinCollection()
        {
            SkinElement url = (SkinElement)CreateNewElement();
            Add(url);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return

                    ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SkinElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((SkinElement)element).Name;
        }

        public new string AddElementName
        {
            get
            { return base.AddElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string ClearElementName
        {
            get
            { return base.ClearElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string RemoveElementName
        {
            get
            { return base.RemoveElementName; }
        }

        public new int Count
        {
            get { return base.Count; }
        }

        public SkinElement this[int index]
        {
            get
            {
                return (SkinElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public SkinElement this[string Name]
        {
            get
            {
                return (SkinElement)BaseGet(Name);
            }
        }

        public int IndexOf(SkinElement url)
        {
            return BaseIndexOf(url);
        }

        public void Add(SkinElement url)
        {
            BaseAdd(url);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(SkinElement url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
    public class SkinElement : ConfigurationElement
    {

        public SkinElement(string name)
        {
            this.Name = name;
        }

        public SkinElement()
        {
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }

    }
    public class LanguageCollection : ConfigurationElementCollection
    {
        public LanguageCollection()
        {
            LanguageElement url = (LanguageElement)CreateNewElement();
            Add(url);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return

                    ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LanguageElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((LanguageElement)element).Name;
        }

        public new string AddElementName
        {
            get
            { return base.AddElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string ClearElementName
        {
            get
            { return base.ClearElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string RemoveElementName
        {
            get
            { return base.RemoveElementName; }
        }

        public new int Count
        {
            get { return base.Count; }
        }

        public LanguageElement this[int index]
        {
            get
            {
                return (LanguageElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public LanguageElement this[string Name]
        {
            get
            {
                return (LanguageElement)BaseGet(Name);
            }
        }

        public int IndexOf(LanguageElement url)
        {
            return BaseIndexOf(url);
        }

        public void Add(LanguageElement url)
        {
            BaseAdd(url);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(LanguageElement url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
    public class LanguageElement : ConfigurationElement
    {

        public LanguageElement(string name)
        {
            this.Name = name;
        }

        public LanguageElement()
        {
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }

    }
    public class DBProviderCollection : ConfigurationElementCollection
    {
        public DBProviderCollection()
        {
            DBProviderElement url = (DBProviderElement)CreateNewElement();
            Add(url);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return

                    ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DBProviderElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((DBProviderElement)element).FactoryClassName;
        }

        public new string AddElementName
        {
            get
            { return base.AddElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string ClearElementName
        {
            get
            { return base.ClearElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string RemoveElementName
        {
            get
            { return base.RemoveElementName; }
        }

        public new int Count
        {
            get { return base.Count; }
        }

        public DBProviderElement this[int index]
        {
            get
            {
                return (DBProviderElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public DBProviderElement this[string Name]
        {
            get
            {
                return (DBProviderElement)BaseGet(Name);
            }
        }

        public int IndexOf(DBProviderElement url)
        {
            return BaseIndexOf(url);
        }

        public void Add(DBProviderElement url)
        {
            BaseAdd(url);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(DBProviderElement url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.FactoryClassName);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }

    public class DBProviderElement : ConfigurationElement
    {

        public DBProviderElement(string name, string factoryName, string assembly)
        {
            this.Name = name;
            this.FactoryClassName = factoryName;
            this.Assembly = assembly;
        }

        public DBProviderElement()
        {
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("factoryClassName", IsRequired = true)]
        public string FactoryClassName
        {
            get
            {
                return (string)this["factoryClassName"];
            }
            set
            {
                this["factoryClassName"] = value;
            }
        }

        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly
        {
            get
            {
                return (string)this["assembly"];
            }
            set
            {
                this["urassemblyl"] = value;
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }

    }
}
