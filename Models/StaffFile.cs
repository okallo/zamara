namespace zamara.Models;
public abstract class StaffFileModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FileType { get; set; }
        public string? Extension { get; set; }
    }