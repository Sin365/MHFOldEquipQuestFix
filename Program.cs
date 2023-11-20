using System.Runtime.ConstrainedExecution;

namespace MHFOldEquipQuestFix
{
    internal class Program
    {
        static string loc = Path.GetDirectoryName(AppContext.BaseDirectory) + "\\";
        const string InDir = "Input";
        const string OutDir = "Out";
        const string Ver = "0.2";
        static void Main(string[] args)
        {
            string title = $"MHFOldEquipQuestFix Ver.{Ver} By 皓月云 axibug.com";
            Console.Title = title;
            Console.WriteLine(title);

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

        static bool Do(byte[] src,out byte[] target)
        {
            try
            {

                target = HexHelper.CopyByteArr(src);//加载数据

                byte src1;
                byte src2;
                byte src3;
                byte src4;
                //Weapon
                src1 = src[0x0124];
                src2 = src[0x0125];
                src3 = src[0x0126];
                src4 = src[0x0127];
                target[0x0120] = src1;
                target[0x0121] = src2;
                target[0x0122] = src3;
                target[0x0123] = src4;

                //Head  
                src1 = src[0x012C];
                src2 = src[0x012D];
                src3 = src[0x012E];
                src4 = src[0x012F];
                target[0x0124] = src1;
                target[0x0125] = src2;
                target[0x0126] = src3;
                target[0x0127] = src4;

                //Chest  
                src1 = src[0x0134];
                src2 = src[0x0135];
                src3 = src[0x0136];
                src4 = src[0x0137];
                target[0x0128] = src1;
                target[0x0129] = src2;
                target[0x012A] = src3;
                target[0x012B] = src4;

                //Arms  
                src1 = src[0x013C];
                src2 = src[0x013D];
                src3 = src[0x013E];
                src4 = src[0x013F];
                target[0x012C] = src1;
                target[0x012D] = src2;
                target[0x012E] = src3;
                target[0x012F] = src4;


                //Waist  
                src1 = src[0x0144];
                src2 = src[0x0145];
                src3 = src[0x0146];
                src4 = src[0x0147];
                target[0x0130] = src1;
                target[0x0131] = src2;
                target[0x0132] = src3;
                target[0x0133] = src4;

                //SR/AI FLag
                src1 = src[336];
                src2 = src[337];
                src3 = src[338];
                src4 = src[339];
                target[312] = src1;
                target[313] = src2;
                target[314] = src3;
                target[315] = src4;

                src1 = src[340];
                src2 = src[341];
                src3 = src[342];
                src4 = src[343];
                target[316] = src1;
                target[317] = src2;
                target[318] = src3;
                target[319] = src4;
                return true;
            }
            catch (Exception e) 
            {
                target = null;
                return false;
            }
        }
    }
}