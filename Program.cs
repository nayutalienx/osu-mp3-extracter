// See https://aka.ms/new-console-template for more information

using osu_mp3_extractor;

class Program
{
    static void Main(string[] args)
    {
        string osuPath = null;
        string destinationPath = null;

        List<string> ids = new List<string>();
        string input = null;

        foreach (var s in args)
        {
            if (s.Contains("-osu="))
            {
                osuPath = s.Split("=")[1] + "\\Songs";
            }

            if (s.Contains("-destination="))
            {
                destinationPath = s.Split("=")[1];
            }

            if (s.Contains("-input="))
            {
                input = s.Split("=")[1];
            }

            if (s.Contains("-ids="))
            {
                ids.AddRange(s.Split("=")[1].Split(","));
            }

            if (s.Contains("-help"))
            {
                Console.WriteLine(
                    @"Usage: -osu=C:\Users\user\AppData\Local\osu! -destination=D:\osu-music -input=file.json -ids=1234,1234,4231 \n// Input file must be json array of strings");
                Environment.Exit(1);
            }
        }


        if (String.IsNullOrEmpty(osuPath) || String.IsNullOrEmpty(destinationPath))
        {
            Console.WriteLine("You need pass all directories. Try -help");
            Environment.Exit(1);
        }

        if (String.IsNullOrEmpty(input) && ids.Count == 0)
        {
            Console.WriteLine("You need pass beatmaps ids. Try -help");
            Environment.Exit(1);
        }

        if (!String.IsNullOrEmpty(input))
        {
            FileStore<string> inputStore = new FileStore<string>(input, false);
            ids.AddRange(inputStore.Items);
        }

        Extract(osuPath, destinationPath, ids.ToArray());
    }

    static void Extract(string osuPath, string destinationPath, string[] ids)
    {
        string[] dirs = Directory.GetDirectories(osuPath);
        foreach (var dir in dirs)
        {
            foreach (var id in ids)
            {
                string[] splittedDir = dir.Split("\\");
                string dirName = splittedDir[^1];

                if (dirName.Equals(id))
                {
                    string[] files = Directory.GetFiles(dir);

                    string musicName = null;

                    foreach (var file in files)
                    {
                        if (file.Contains(".osu"))
                        {
                            string[] splitedFile = file.Split('\\');
                            string nameWithExt = splitedFile[^1];
                            string name = nameWithExt.Split(".osu")[0];
                            musicName = name;
                            break;
                        }
                    }


                    foreach (var file in files)
                    {
                        if (file.Contains(".mp3"))
                        {
                            string destinationFile = destinationPath + "\\" + id + ".mp3";
                            if (musicName != null)
                            {
                                destinationFile = destinationPath + "\\" + musicName + ".mp3";
                            }

                            try
                            {
                                File.Copy(file, destinationFile);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                break;
                            }

                            Console.WriteLine("File " + file + " copied to " + destinationFile);
                            break;
                        }
                    }
                }
            }
        }
    }
}