using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Quiz:IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
    