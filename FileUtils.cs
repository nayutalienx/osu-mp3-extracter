namespace osu_mp3_extractor;

public class FileUtils
{
    public static void PreparePath(string file, bool createDir = true, bool createFile = true)
    {
        string[] combinedPath = file.Split("/");
        combinedPath[^1] = "";
        string dirPath = String.Join("/", combinedPath);
        if (createDir && !Directory.Exists(file)) Directory.CreateDirectory(dirPath);
        if (createFile && !File.Exists(file)) File.AppendText(file).Close();
    }
}