using System;
using SqliteORM;

namespace wpfClient.Model
{
    [Table]
    public class BookMessage : TableBase<BookMessage>
    {
        [PrimaryKey(true)]
        public long Id { get; set; }
        [Field]
        public string Name { get; set; }
        [Field]
        public string AuthorName { get; set; }
        [Field]
        public string ImgUrl { get; set; }
        [Field]
        public string Url { get; set; }
        [Field]
        public string LastChapterUrl { get; set; }
        [Field]
        public string LastChapter { get; set; }
        [Field]
        public string ResourceFromsite { get; set; }
        [Field]
        public bool IsRead { get; set; }
        [Field]
        public DateTime CreateOn { get; set; }
    }
}