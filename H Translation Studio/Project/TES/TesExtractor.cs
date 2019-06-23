using HTStudio.Project.Base;
using System.IO;
using System.Text;

namespace HTStudio.Project.TES
{
    internal class TesExtractor : BaseExtractor
    {
        public override bool SupportExtract => true;

        public override bool SupportApply => true;

        public TesExtractor(BaseProject project) : base(project)
        {
        }



        public override void Apply()
        {
            ApplyLine(Path.Combine(project.BackupPath, "main_102_Select.txt"), Path.Combine(project.path, "main_102_Select.txt"));
            ApplyLine(Path.Combine(project.BackupPath, "main_401_Dialog.txt"), Path.Combine(project.path, "main_401_Dialog.txt"));
        }

        public override void Backup()
        {
            Directory.CreateDirectory(project.BackupPath);
            File.Copy(Path.Combine(project.path, "main_102_Select.txt"), Path.Combine(project.BackupPath, "main_102_Select.txt"));
            File.Copy(Path.Combine(project.path, "main_401_Dialog.txt"), Path.Combine(project.BackupPath, "main_401_Dialog.txt"));
        }

        public override void Extract()
        {
            ExtractLine(Path.Combine(project.BackupPath, "main_102_Select.txt"));
            ExtractLine(Path.Combine(project.BackupPath, "main_401_Dialog.txt"));
        }

        private void ExtractLine(string path)
        {
            var lines = File.ReadAllLines(path);
            foreach(var line in lines)
            {
                if(!line.StartsWith("#") && line.Trim() != "")
                {
                    InsertNewTranslateStrings(line);
                }
            }
        }

        private void ApplyLine(string backup, string output)
        {
            var lines = File.ReadAllLines(backup);
            var outStr = new StringBuilder();

            foreach(var line in lines)
            {
                outStr.Append(QueryForTranslate(line));
                outStr.AppendLine();
            }

            File.WriteAllText(output, outStr.ToString());
        }

        public override void Restore()
        {
            File.Copy(Path.Combine(project.BackupPath, "main_102_Select.txt"), Path.Combine(project.path, "main_102_Select.txt"));
            File.Copy(Path.Combine(project.BackupPath, "main_401_Dialog.txt"), Path.Combine(project.path, "main_401_Dialog.txt"));
        }
    }
}