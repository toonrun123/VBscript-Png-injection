using System;
using System.Runtime;
using System.Threading;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace VBs
{
    class Program
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void OnCommand()
        {
            Console.Write("Picture Path: (only .png): ");
            string pathflie = Console.ReadLine();
            bool isexitsts = File.Exists(@pathflie);
            if (isexitsts == true) //Check file is exitsts.
            {
                string typeFile = Path.GetExtension(@pathflie);
                if (typeFile == ".png") //Check file type.
                {
                    Console.WriteLine("Reading Bytes...");
                    byte[] bam = File.ReadAllBytes(@pathflie); //Table Bytes.
                    int BytesCount = 0;
                    foreach (byte s in bam)
                    {
                        BytesCount += 1;
                    }
                    double BytesMb = (0.001) * BytesCount;
                    Console.WriteLine("Bytes: " + BytesCount);
                    Console.WriteLine("Size: " + BytesMb + " Kb.");
                    /////////////////////////////////////////////////////////////
                    Console.Write("VBScript Path: (only .txt): ");
                    string txtpathflie = Console.ReadLine();
                    bool txtisexitsts = File.Exists(@txtpathflie);
                    if (txtisexitsts == true)
                    {
                        string txttypeFile = Path.GetExtension(@txtpathflie);
                        if (txttypeFile == ".txt")
                        {
                            string SourceVBscript = File.ReadAllText(@txtpathflie);
                            byte[] result = bam;
                            byte[] coach = Encoding.ASCII.GetBytes(SourceVBscript);
                            byte[] combinebytes = new byte[result.Length+coach.Length];
                            Buffer.BlockCopy(result, 0, combinebytes,0,result.Length);
                            Buffer.BlockCopy(coach, 0, combinebytes,result.Length, coach.Length);
                            int indexcount = 0;
                            foreach (byte s in combinebytes)
                            {
                                indexcount += 1;
                            }
                            double newfileBytesMb = (0.001) * indexcount;
                            Console.WriteLine("Bytes: " + indexcount);
                            Console.WriteLine("Size: " + newfileBytesMb + " Kb.");
                            Console.Write("Overwrite Old Picture? (yes/no/cancel): ");
                            string res = Console.ReadLine();
                            res = res.ToLower();
                            if (res == "yes")
                            {
                                File.WriteAllBytes(@pathflie, combinebytes);
                                Console.WriteLine("Now Picture has Overwritted with VBScript.");
                            }
                            else
                            if (res == "no")
                            {
                                string getparentfile = Path.GetDirectoryName(@pathflie);
                                string namewithouttype = Path.GetFileNameWithoutExtension(@pathflie);
                                string newnamerandomed = namewithouttype + "_" + RandomString(5) + ".png";
                                string newpathandname = getparentfile+@"\"+ newnamerandomed;
                                File.WriteAllBytes(@newpathandname, combinebytes);
                                Console.WriteLine("Created. file name is "+ newnamerandomed +".");
                            }
                            else
                            if (res == "cancel")
                            {
                                Console.WriteLine("Now Cancelled.");
                            }
                            GC.Collect(); //Active Garbage Collection
                            OnCommand();
                        }
                        else
                        {
                            Console.WriteLine("File type is " + typeFile + ", allowed only .txt");
                            OnCommand();
                        }
                    }
                    else
                    {
                        Console.WriteLine("VBScript not found.");
                        OnCommand();
                    }
                }
                else
                {
                    Console.WriteLine("File type is " + typeFile + ", allowed only .png");
                    OnCommand();
                }
            }
            else
            {
                Console.WriteLine("Picture not found.");
                OnCommand();
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("============|| VBscript Picture Injection ||============");
            Console.WriteLine("Made by toonrun123.");
            Console.WriteLine("Tool for inject vbscript into Picture.");
            Console.WriteLine("This tool open sourcecode. https://github.com/toonrun123/VBscript-Png-injection");
            Console.WriteLine(" ");
            OnCommand();
            Thread.Sleep(-1);
        }
    }
}
