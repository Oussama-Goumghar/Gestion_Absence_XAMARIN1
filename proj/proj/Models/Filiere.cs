using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace proj.Models
{
    [Table("Filiere")]
    class Filiere
    {
        [PrimaryKey, AutoIncrement]
        public int IdFiliere { get; set; }

        [MaxLength(100), Unique]
        public string  FiliereName { get; set; }

        [OneToMany] 
        public List<Student> student { get; set; }
    }
}
