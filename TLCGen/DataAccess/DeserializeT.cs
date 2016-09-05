﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TLCGen.DataAccess
{
    public class DeserializeT<T>
    {
        #region XML Serialization

        public T DeSerialize(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
                return default(T);

            T t = default(T);

            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    t = (T)serializer.Deserialize(fs);
                    fs.Close();
                }
            }
            catch (InvalidOperationException)
            {
                System.Windows.MessageBox.Show("Bestandsformaat onjuist. Is dit een TLCGen bestand?", "Fout bij laden bestand.");
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Fout bij laden bestand: " + e.ToString(), "Fout bij laden bestand.");
            }

            return t;
        }

        #endregion // XML Serialization

        #region GZip Serialization

        public T DeSerializeGZip(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
                return default(T);

            T t = default(T);

            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                using (var gz = new GZipStream(fs, CompressionMode.Decompress))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    t = (T)serializer.Deserialize(gz);
                }
                fs.Close();
            }
            catch (InvalidOperationException)
            {
                System.Windows.MessageBox.Show("Bestandsformaat onjuist. Is dit een TLCGen bestand?", "Fout bij laden bestand.");
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Fout bij laden bestand: " + e.ToString(), "Fout bij laden bestand.");
            }

            return t;
        }

        #endregion // GZip Serialization
    }
}
