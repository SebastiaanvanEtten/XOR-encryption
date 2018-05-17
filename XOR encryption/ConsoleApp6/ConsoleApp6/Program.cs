using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            //string hexstr = "040000008200E00074C5B7101A82E0080000000622029301EB4592B71528F44A48D7";
            string hexstr2 = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            string hexstr3 = "1c0111001f010100061a024b53535009181c";
            byte[] data = ConvertFromStringToHex(hexstr3);
            string base64 = Convert.ToBase64String(data);

            Console.WriteLine("challenge 1");
            Console.WriteLine(base64);
            Console.WriteLine(" ");
            Console.ReadKey();

            string ToXor1 = "1c0111001f010100061a024b53535009181c";
            string ToXor2 = "686974207468652062756c6c277320657965";

            Console.WriteLine("challenge 2");
            Console.WriteLine(xording(ToXor1, ToXor2));
            Console.WriteLine(" ");
            Console.ReadKey();

            string decryptDit = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";

            Console.WriteLine("challenge 3");
            Console.WriteLine(decryptSingle(decryptDit));
            Console.ReadKey();

            Console.WriteLine("challenge 4");
            Console.WriteLine(findFrom60lines(@"PATH-TO\decrypt.txt"));
            Console.ReadKey();
            
        }


        public static string findFrom60lines(string path)
        {
            string line;
            int counter = 0;
 
            System.IO.StreamReader file =
                new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                int length = line.Length / 2;
                string result = "";

                for (int j = 0; j < 256; j++)
                {
                    char key = Convert.ToChar(j);
                    if (char.IsLetter(key) || char.IsNumber(key))
                    {
                        for (int i = 0; i < length; i++)
                        {
                            char stukjehexstring = Convert.ToChar(Convert.ToUInt32(line.Substring(i * 2, 2), 16));
                            char Xord = (char)(stukjehexstring ^ key);
                            if (char.IsLetter(Xord) || char.IsNumber(Xord) || Xord.ToString() == " ")
                            {
                                result += Xord;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (result.Split(" ").Length > 5)
                        {
                            System.Console.WriteLine(line);
                            Console.WriteLine("sleutel: " + key + "(" + j + ") = " + result + " | spaces: "+ result.Split(" ").Length);
                            counter++;
                        }
                        result = "";
                    }
                }
            }

            file.Close();
            return "all "+counter+" lines done \n\n";
        }

        public static string decryptSingle(string hexstring)
        {
            int length = hexstring.Length / 2;
            string result = "";

            for (int j = 0; j < 128; j++)
            {
                char key = Convert.ToChar(j);
                if (char.IsLetter(key) || char.IsNumber(key))
                {
                    for (int i = 0; i < length; i++)
                    {
                        char stukjehexstring = Convert.ToChar(Convert.ToUInt32(hexstring.Substring(i * 2, 2), 16));
                        char Xord = (char)(stukjehexstring ^ key);
                        if (char.IsLetter(Xord) || char.IsNumber(key) || Xord.ToString() == " ")
                        {
                            result += Xord;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (result.Split(" ").Length > 3)
                    {
                        Console.WriteLine("sleutel: " + key + "(" + j + ") = " + result);
                    }
                    result = "";
                }
            }
            
            return "done\n\n";
        }


        public static string xording(string een, string twee)
        {
            int lengte;

            if (een.Length > twee.Length)
            {
                lengte = twee.Length / 2;
            }
            else
            {
                lengte = een.Length / 2;
            }

            string bytestring = "";

            for (int i = 0; i < lengte; i++)
            {

                byte A = Convert.ToByte(een.Substring(i * 2, 2), 16);
                byte B = Convert.ToByte(twee.Substring(i * 2, 2), 16);
                bytestring +=  ((byte)(A ^ B)).ToString("X"); 

            }

            return bytestring;
        }


        public static byte[] ConvertFromStringToHex(string inputHex)
        {
            inputHex = inputHex.Replace("-", "");

            byte[] resultantArray = new byte[inputHex.Length / 2];
            for (int i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = Convert.ToByte(inputHex.Substring(i * 2, 2), 16);
            }
            return resultantArray;
        }
    }
}
