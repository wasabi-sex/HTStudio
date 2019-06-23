using HTStudio.Project.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTStudio.Project.TES
{
    public class TesProject : BaseProject
    {
        public TesProject(string path) : base(path)
        {
            extractor = new TesExtractor(this);
        }

        private TesExtractor extractor;

        public override string Name => "TESPATCHER DATA";

        public override BaseExtractor Extractor => extractor;

        public static TesProject Identification(string path)
        {
            if (!File.Exists(Path.Combine(path, "Game.exe"))) return null;
            if (!File.Exists(Path.Combine(path, "main_401_Dialog.txt"))) return null;

            var project = new TesProject(path);
            if (!Directory.Exists(project.BackupPath))
            {
                project.extractor.Backup();
            }
            return project;
        }
    }
}
