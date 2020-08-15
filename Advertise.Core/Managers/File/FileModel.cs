using System;

namespace Advertise.Core.Managers.File
{
    public class FileModel
    {
        public string Extension { get; set; }
        public Guid Id { get; set; }
        public string Name { set; get; }
        public string Path { get; set; }
        public long Size { set; get; }
        public string Type { set; get; }
    }
}