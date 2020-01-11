using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UTF8Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入需要处理的文件夹完整路径(列如D:\\Test):");
            string rootPath = string.Empty;
            do
            {
                rootPath = Console.ReadLine();
                if (string.IsNullOrEmpty(rootPath))
                    Console.WriteLine("路径不能为空,请重新输入");
            } while (string.IsNullOrEmpty(rootPath));

            Console.WriteLine("请输入需要处理的文件类型(支持通配符,多种以','隔开,列如*.cs,*.java表示所有cs文件与java文件):");
            string suffix = string.Empty;
            do
            {
                suffix = Console.ReadLine();
                if (string.IsNullOrEmpty(suffix))
                    Console.WriteLine("文件类型不能为空,请重新输入");
            } while (string.IsNullOrEmpty(suffix));

            Console.WriteLine("是否递归遍历所有子文件夹(默认是,否请输入0)?");
            bool all = Console.ReadLine() != "0";

            List<string> files = new List<string>();
            foreach (var aSuffix in suffix.Split(','))
            {
                var searchOption = all ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                files.AddRange(Directory.GetFiles(rootPath, suffix, searchOption));
            }
            files = files.Distinct().ToList();
            foreach (var aFile in files)
            {
                var oldEncoding = EncodingHelper.GetEncodingString(new FileInfo(aFile));
                var content = File.ReadAllText(aFile, Encoding.GetEncoding(oldEncoding));
                if (string.IsNullOrEmpty(content))
                    Console.WriteLine($"{aFile}文件为空,无法转换已跳过");
                else
                {
                    File.WriteAllText(aFile, content, Encoding.UTF8);
                    Console.WriteLine($"转换文件成功:{aFile}");
                }
            }

            Console.WriteLine("完成");
            Console.ReadLine();
        }
    }
}
