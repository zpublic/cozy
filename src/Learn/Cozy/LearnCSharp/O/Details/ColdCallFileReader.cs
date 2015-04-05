using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.O.Details
{
    public class ColdCallFileReader
    {
        private FileStream fs;
        private StreamReader sr;
        private uint nPeopleToRing;
        private bool isDisposed = false;
        private bool isOpen = false;

        public void Open(string fileName)
        {
            if (isDisposed)
                throw new ObjectDisposedException("peopleToRing");

            fs = new FileStream(fileName, FileMode.Open);
            sr = new StreamReader(fs);

            try
            {
                string firstLine = sr.ReadLine();
                nPeopleToRing = uint.Parse(firstLine);
                isOpen = true;
            }
            catch (FormatException ex)
            {
                throw new ColdCallFileFormatException(
                   "First line isn\'t an integer", ex);
            }
        }

        public void ProcessNextPerson()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("peopleToRing");
            }

            if (!isOpen)
            {
                throw new UnexpectedException(
                    "Attempted to access coldcall file that is not open");
            }

            try
            {
                string name;
                name = sr.ReadLine();
                if (name == null)
                {
                    throw new ColdCallFileFormatException("Not enough names");
                }
                if (name[0] == 'B')
                {
                    throw new SalesSpyFoundException(name);
                }
                Console.WriteLine(name);
            }
            catch (SalesSpyFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        public uint NPeopleToRing
        {
            get
            {
                if (isDisposed)
                {
                    throw new ObjectDisposedException("peopleToRing");
                }

                if (!isOpen)
                {
                    throw new UnexpectedException(
                        "Attempted to access cold–call file that is not open");
                }

                return nPeopleToRing;
            }
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;
            isOpen = false;

            if (fs != null)
            {
                fs.Close();
                fs = null;
            }
        }
    }
}
