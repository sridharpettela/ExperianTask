using System.Collections.Generic;

namespace ExperianTask.Entity
{
    public class AlbumEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public IList<PhotoEntity> Photos { get; set; }
    }
}
