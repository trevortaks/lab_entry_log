using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace dbConnectAnd
{
    [Table(Name = "student_details")]
    class Student
    {
        private int _Id;
        private string _Name, _Reg, _Prog;

        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { 
            get{
                return this._Id;
            } 
            set{
                this._Id = value;
            }
        }

        [Column(Name = "student_name")]
        public string Name {
            get {
                return this._Name;
            }
            set {
                this._Name = value;
            }
        }

        [Column(Name = "student_reg", IsUnique = true)]
        public string Registration {
            get {
                return this._Reg;
            }
            set {
                this._Reg = value;
            }
        }

        [Column(Name = "student_prog")]
        public string Program
        {
            get
            {
                return this._Prog;
            }
            set
            {
                this._Prog = value;
            }
        }
        
    }
}
