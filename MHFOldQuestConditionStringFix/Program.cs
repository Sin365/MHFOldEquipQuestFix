using System;
using System.Reflection;
using System.Text;

namespace MHFOldQuestConditionStringFix
{
    internal class Program
    {
        static string loc = Path.GetDirectoryName(AppContext.BaseDirectory) + "\\";
        const string InDir = "Input";
        const string OutDir = "Out";
        const string Ver = "0.1";
        static byte[] SrcStringByte = { 0x83,
0x81,
0x83,
0x43,
0x83,
0x93,
0x83,
0x5e,
0x81,
0x5b,
0x83,
0x51,
0x83,
0x62,
0x83,
0x67,
0x81,
0x41,
0x82,
0xe0,
0x82,
0xb5,
0x82,
0xad,
0x82,
0xcd,
0x0a,
0x83,
0x54,
0x83,
0x75,
0x83,
0x5e,
0x81,
0x5b,
0x83,
0x51,
0x83,
0x62,
0x83,
0x67,
0x82,
0x60,
0x82,
0xA9,
0x82,
0x61,
0x82,
0xCC,
0x92,
0x42,
0x90,
0xAC,
0x00,};
        static byte[] SrcStringByte_2 = { 0x83,
0x81,
0x83,
0x43,
0x83,
0x93,
0x83,
0x5e,
0x81,
0x5b,
0x83,
0x51,
0x83,
0x62,
0x83,
0x67,
0x81,
0x41,
0x82,
0xe0,
0x82,
0xb5,
0x82,
0xad,
0x82,
0xcd,
0x0a,
0x83,
0x54,
0x83,
0x75,
0x83,
0x5e,
0x81,
0x5b,
0x83,
0x51,
0x83,
0x62,
0x83,
0x67,
0x82,
0x60,
0x81,
0x41,
0x82,
0x61,
0x82,
0xCC,
0x92,
0x42,
0x90,
0xAC,
0x00,};
        static byte[] targetStringByte = { 0x82,
0x60,
0x82,
0xA9,
0x82,
0x61,
0x82,
0xCC,
0x92,
0x42,
0x90,
0xAC,
0x00};
        static void Main(string[] args)
        {
            string title = $"MHFOldQuestConditionStringFix Ver.{Ver} By 皓月云 axibug.com";
            Console.Title = title;
            Console.WriteLine(title);


            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //Encoding shiftencode =  Encoding.GetEncoding("Shift-JIS");
            //SrcStringByte = shiftencode.GetBytes("メインターゲット、もしくは\\r\\nサブターゲットＡかＢの達成");
            //targetStringByte = shiftencode.GetBytes("ＡかＢの達成");

            if (!Directory.Exists(loc + InDir))
            {
                Console.WriteLine("Input文件不存在");
                Console.ReadLine();
                return;
            }

            if (!Directory.Exists(loc + OutDir))
            {
                Console.WriteLine("Out文件不存在");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"-----------原数据读取完毕-----------");

            string[] files = FileHelper.GetDirFile(loc + InDir);
            Console.WriteLine($"共{files.Length}个文件，是否处理? (y/n)");

            string yn = Console.ReadLine();
            if (yn.ToLower() != "y")
                return;

            int index = 0;
            int errcount = 0;
            for (int i = 0; i < files.Length; i++)
            {
                string FileName = files[i].Substring(files[i].LastIndexOf("\\"));

                if (!FileName.ToLower().Contains(".mib") && !FileName.ToLower().Contains(".bin"))
                {
                    continue;
                }
                index++;

                Console.WriteLine($">>>>>>>>>>>>>>开始处理 第{index}个文件  {FileName}<<<<<<<<<<<<<<<<<<<");
                FileHelper.LoadFile(files[i], out byte[] data);
                if (Do(data, out byte[] targetdata))
                {
                    string newfileName = FileName;
                    string outstring = loc + OutDir + "\\" + newfileName;
                    FileHelper.SaveFile(outstring, targetdata);
                    Console.WriteLine($">>>>>>>>>>>>>>成功处理 第{index}个:{outstring}");
                }
                else
                {
                    errcount++;
                    Console.WriteLine($">>>>>>>>>>>>>>处理失败 第{index}个");
                }
            }
            Console.WriteLine($"已处理{files.Length}个文件，其中{errcount}个失败");
            Console.WriteLine($"完毕");
            Console.ReadLine();
        }

        static bool Do(byte[] src, out byte[] target)
        {
            try
            {
                int Idx = 0;
                
                if (SearchByte(src, SrcStringByte, 0, ref Idx) > 0)
                {
                    target = HexHelper.CopyByteArr(src);//加载数据
                    for (int i = 0; i < SrcStringByte.Length; i++)
                    {
                        if (i + 1 <= targetStringByte.Length)
                            target[Idx + i] = targetStringByte[i];
                        else
                            target[Idx + i] = 0x00;
                    }
                }
                else if (SearchByte(src, SrcStringByte_2, 0, ref Idx) > 0)
                {
                    target = HexHelper.CopyByteArr(src);//加载数据
                    for (int i = 0; i < SrcStringByte_2.Length; i++)
                    {
                        if (i + 1 <= targetStringByte.Length)
                            target[Idx + i] = targetStringByte[i];
                        else
                            target[Idx + i] = 0x00;
                    }
                }
                else
                {
                    target = src;
                }
                return true;
            }
            catch (Exception e)
            {
                target = null;
                return false;
            }
        }
        static int SearchByte(byte[] buffer, byte[] key, int startPos, ref int pos)
        {
            int i = startPos;
            int tem_pos = 0;
            bool found1 = false;
            int found2 = 0;

            do
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (buffer[i] == key[j])
                    {
                        if (j == 0)
                        {
                            found1 = true;
                            tem_pos = i;
                        }
                        else
                            found2 = 1;
                        i = i + 1;
                    }
                    else
                    {
                        if (j == 0)
                            found1 = false;
                        else
                            found2 = 2;

                    }

                    if (found1 == false)
                    {
                        i = i + 1;
                        break;
                    }

                    if ((found2 == 2))
                    {
                        i = i - j + 1;
                        found1 = false;
                        found2 = 0;
                        break;
                    }

                    if ((found1 == true) && (found2 == 1) && (j == (key.Length - 1))) //匹配上了
                    {
                        pos = i - (key.Length);
                        return 1;
                    }
                }


            }
            while (i < buffer.Length);

            return 0;
        }
    }
}
