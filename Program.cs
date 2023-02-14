using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mohaymen.Academy
{
    internal abstract class Program
    {
        static void Main(string[] args)
        {
            var students = new List<Student>();
            var teachers = new List<Teacher>();
            var classes = new List<Class>();
            var programRun = true;
            while (programRun)
            {
                var action = Console.ReadLine().Trim().Split(' ');
                if (action[0] == "register_student")
                {
                    var st = new Student();
                    students = st.RegisterStudent(students, teachers, action[1], action[2], int.Parse(action[3]),
                        action[4]);
                }
                else if (action[0] == "register_professor")
                {
                    var te = new Teacher();
                    teachers = te.RegisterProfessor(students, teachers, action[1], action[2], action[3]);
                }
                else if (action[0] == "make_class")
                {
                    var cl = new Class();
                    classes = cl.MakeClass(classes, action[1], action[2], action[3]);
                }
                else if (action[0] == "add_student")
                {
                    var cl = new Class();
                    classes = cl.AddStudent(classes, students, action[1], action[2]);
                }
                else if (action[0] == "add_professor")
                {
                    var cl = new Class();
                    classes = cl.AddProfessor(classes, teachers, action[1], action[2]);
                }
                else if (action[0] == "student_status")
                {
                    var cl = new Class();
                    cl.StudentStatus(classes, students, action[1]);
                }
                else if (action[0] == "end")
                {
                    programRun = false;
                }
            }
        }
    }

    internal class Student
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                var match = Regex.Match(value, @"[a-z]*", RegexOptions.IgnoreCase);
                if (value.Length > 1 & value.Length < 20 & match.Success)
                {
                    _name = value;
                }
            }
        }

        private string _identicalNum;

        public string IdenticalNum
        {
            get => _identicalNum;
            set
            {
                var match = Regex.Match(value, @"[0-9]*", RegexOptions.IgnoreCase);
                if (value.Length == 10 & match.Success)
                {
                    _identicalNum = value;
                }
            }
        }

        private string _field;

        public string Field
        {
            get => _field;
            set
            {
                var match = Regex.Match(value, @"[a-z]*", RegexOptions.IgnoreCase);
                if (value.Length > 1 & value.Length < 20 & match.Success)
                {
                    _field = value;
                }
            }
        }

        private int _enteringYear;

        public int EnteringYear
        {
            get => _enteringYear;
            set
            {
                if (value > 1300 & value < 1500)
                {
                    _enteringYear = value;
                }
            }
        }

        public List<Student> RegisterStudent(List<Student> students, List<Teacher> teachers, string name,
            string identicalNum,
            int enteringYear, string field)
        {
            if (students.Any(s => s.IdenticalNum == identicalNum) | teachers.Any(t => t.IdenticalNum == identicalNum))
            {
                Console.WriteLine("this identical number previously registered");
            }
            else
            {
                students.Add(new Student()
                    { Name = name, IdenticalNum = identicalNum, EnteringYear = enteringYear, Field = field });
                Console.WriteLine("welcome to golestan");
            }

            return students;
        }
    }

    internal class Teacher
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                var match = Regex.Match(value, @"[a-z]*", RegexOptions.IgnoreCase);
                if (value.Length > 1 & value.Length < 20 & match.Success)
                {
                    _name = value;
                }
            }
        }

        private string _identicalNum;

        public string IdenticalNum
        {
            get => _identicalNum;
            set
            {
                var match = Regex.Match(value, @"[0-9]*", RegexOptions.IgnoreCase);
                if (value.Length == 10 & match.Success)
                {
                    _identicalNum = value;
                }
            }
        }

        private string _field;

        public string Field
        {
            get => _field;
            set
            {
                var match = Regex.Match(value, @"[a-z]*", RegexOptions.IgnoreCase);
                if (value.Length > 1 & value.Length < 20 & match.Success)
                {
                    _field = value;
                }
            }
        }

        public List<Teacher> RegisterProfessor(List<Student> students, List<Teacher> teachers, string name,
            string identicalNum, string field)
        {
            if (students.Any(s => s.IdenticalNum == identicalNum) | teachers.Any(t => t.IdenticalNum == identicalNum))
            {
                Console.WriteLine("this identical number previously registered");
            }
            else
            {
                teachers.Add(new Teacher()
                    { Name = name, IdenticalNum = identicalNum, Field = field });
                Console.WriteLine("welcome to golestan");
            }

            return teachers;
        }
    }

    internal class Class
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                var match = Regex.Match(value, @"[a-z]*", RegexOptions.IgnoreCase);
                if (value.Length >= 1 & value.Length <= 20 & match.Success)
                {
                    _name = value;
                }
            }
        }

        private string _classId;

        public string ClassId
        {
            get => _classId;
            set
            {
                var match = Regex.Match(value, @"[0-9]*", RegexOptions.IgnoreCase);
                if (value.Length == 10 & match.Success)
                {
                    _classId = value;
                }
            }
        }

        private string _field;

        public string Field
        {
            get => _field;
            set
            {
                var match = Regex.Match(value, @"[a-z]*", RegexOptions.IgnoreCase);
                if (value.Length >= 1 & value.Length <= 20 & match.Success)
                {
                    _field = value;
                }
            }
        }

        public Teacher Professor { get; set; }
        public List<Student> Students { get; set; }

        public List<Class> MakeClass(List<Class> classes, string name, string classId, string field)
        {
            if (classes.Any(c => c.ClassId == classId))
            {
                Console.WriteLine("this class id previously used");
            }
            else
            {
                classes.Add(new Class()
                    { Name = name, ClassId = classId, Field = field });
                Console.WriteLine("class added successfully");
            }

            return classes;
        }

        public List<Class> AddStudent(List<Class> classes, List<Student> students, string identicalNum,
            string classId)
        {
            if (students.Any(s => s.IdenticalNum == identicalNum))
            {
                var student = students.First(i => i.IdenticalNum == identicalNum);
                if (classes.Any(c => c.ClassId == classId))
                {
                    var classroom = classes.First(i => i.ClassId == classId);
                    if (student.Field == classroom.Field)
                    {
                        if (classroom.Students != null &&
                            classroom.Students.Any(i => i.IdenticalNum == student.IdenticalNum))
                        {
                            Console.WriteLine("student is already registered");
                        }
                        else
                        {
                            var temp = new List<Student> { student };
                            classroom.Students = temp;
                            // classroom.Students.Add(new Student()
                            // {
                            //     Name = student.Name,
                            //     Field = student.Field,
                            //     EnteringYear = student.EnteringYear,
                            //     IdenticalNum = student.IdenticalNum
                            // });
                            Console.WriteLine("student added successfully to the class");
                        }
                    }
                    else
                    {
                        Console.WriteLine("student field is not match");
                    }
                }
                else
                {
                    Console.WriteLine("invalid class");
                }
            }
            else
            {
                Console.WriteLine("invalid student");
            }

            return classes;
        }

        public List<Class> AddProfessor(List<Class> classes, List<Teacher> teachers, string identicalNum,
            string classId)
        {
            if (teachers.Any(s => s.IdenticalNum == identicalNum))
            {
                var teacher = teachers.First(i => i.IdenticalNum == identicalNum);
                if (classes.Any(c => c.ClassId == classId))
                {
                    var classroom = classes.First(i => i.ClassId == classId);
                    if (teacher.Field == classroom.Field)
                    {
                        if (classroom.Professor.IdenticalNum != null)
                        {
                            Console.WriteLine("this class has a professor");
                        }
                        else
                        {
                            classroom.Professor = teacher;
                            Console.WriteLine("professor added successfully to the class");
                        }
                    }
                    else
                    {
                        Console.WriteLine("professor field is not match");
                    }
                }
                else
                {
                    Console.WriteLine("invalid class");
                }
            }
            else
            {
                Console.WriteLine("invalid professor");
            }

            return classes;
        }

        public void StudentStatus(List<Class> classes, List<Student> students, string identicalNum)
        {
            if (students.Any(s => s.IdenticalNum == identicalNum))
            {
                var student = students.First(i => i.IdenticalNum == identicalNum);
                if (classes.Any(i => i.Students.Any(j => j.IdenticalNum == student.IdenticalNum)))
                {
                    var classStudent = classes.First(
                        i => i.Students.Any(j => j.IdenticalNum == student.IdenticalNum));
                    Console.WriteLine(student.Name + student.EnteringYear + student.Field + classStudent.Name);
                }
            }
            else
            {
                Console.WriteLine("invalid student");
            }
        }
    }
}