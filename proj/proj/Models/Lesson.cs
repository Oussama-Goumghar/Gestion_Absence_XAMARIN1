using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace proj.Models
{
    [Table("Lesson")]
    public class Lesson
    {
        [PrimaryKey,AutoIncrement]
        public int IdLesson { get; set; }
        [Unique]
        public string LessonName { get; set; }

        [ManyToMany(typeof(Absence))]
        public List<Student> Etat { get; set; }

        [ForeignKey(typeof(Filiere))]
        public int IdFiliere { get; set; }

        [ManyToOne]
        public Filiere filiere { get; set; }
    }
}
