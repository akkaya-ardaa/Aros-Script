using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Article:IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime AddedDate { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
