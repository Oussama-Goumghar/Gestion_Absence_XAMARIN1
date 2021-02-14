using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace proj.Models
{
    [Table("Absence")]
    class Absence
    {
        [PrimaryKey, AutoIncrement]
        public int IdAbsence { get; set; }
        [ForeignKey(typeof(Student))] 
        public int IdStudent { get; set; }
        [ForeignKey(typeof(Lesson))]
        public int IdLesson { get; set; }
        public bool IsPersnt { get; set; }


    }
}
