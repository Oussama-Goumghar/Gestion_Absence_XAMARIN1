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

        [OneToOne]
        public Student student { get; set; }

        [ForeignKey(typeof(Lesson))]
        public int IdLesson { get; set; }

        [OneToOne]
        public Lesson lesson { get; set; }

        public DateTime Date { get; set; }

        public bool IsPresent { get; set; }


    }
}
