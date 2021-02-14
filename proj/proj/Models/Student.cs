using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace proj.Models
{
    [Table("Student")]
    class Student
    {
   
        [PrimaryKey, AutoIncrement]
        public int IdStudent { get; set; }
        [MaxLength(100), Unique]
        public string cin { get; set; }

        [MaxLength(100)]

        public string Email { get; set; }
        [MaxLength(100)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string nom { get; set; }

        [MaxLength(100)]
        public string Prenom { get; set; }

        [ForeignKey(typeof(Filiere))]
        public int IdFiliere { get; set; }

        [ManyToMany(typeof(Absence))]
        public List<Lesson> Etat { get; set; }

        public Boolean IsChecked { get; set; }

        public int IdAbsence { get; set; }


        public override string ToString()
        {
            return $"{this.nom} : {this.IsChecked}";
        }
    }
}
