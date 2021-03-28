using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace proj.Models
{
    [Table("Filiere")]
    public class Filiere
    {
        [PrimaryKey, AutoIncrement]
        public int IdFiliere { get; set; }

        [MaxLength(100), Unique]
        public string  FiliereName { get; set; }

        [OneToMany]
        public List<Student> students { get; set; } = new List<Student>();

        [OneToMany]
        public List<Lesson> lessons { get; set; } = new List<Lesson>();

    }
}
