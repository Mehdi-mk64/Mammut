using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public static class IOFile
    {
        public static byte[] ConvertImageToBinary(System.Drawing.Image imageIn)
        {
            MemoryStream mStream = new MemoryStream();
            imageIn.Save(mStream, imageIn.RawFormat);
            mStream.Dispose();
            return mStream.ToArray();

        }





        public static byte[] ConvertFileToBinary(string filePath)
        {
            byte[] fileContent = null;
            FileStream fs = new System.IO.FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fs);
            long byteLength = new FileInfo(filePath).Length;
            fileContent = binaryReader.ReadBytes((Int32)byteLength);
            fs.Close();
            fs.Dispose();
            binaryReader.Close();
            return fileContent;

        }


        public static byte[] ConvertStreamToBinary(MemoryStream fileStream)
        {
            BinaryReader binaryReader = new BinaryReader(fileStream);
            byte[] fileContent;
            fileContent = binaryReader.ReadBytes((Int32)fileStream.Length);
            fileStream.Close();
            fileStream.Dispose();
            binaryReader.Close();
            return fileContent;
        }

        public static Stream ConvertBinaryToStream(byte[] byteArray)
        {
            Stream stream = new MemoryStream(byteArray, true);
            return stream;
        }

        public static byte[] ConvertStreamToBinary(Stream fileStream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }





    }
}
