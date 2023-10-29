﻿using StudentDiaryWPF.Models.Domains;
using StudentDiaryWPF.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using StudentDiaryWPF.Models.Converters;
using StudentDiaryWPF.Models;
using System.Runtime.Remoting.Contexts;
using StudentDiaryWPF.Properties;

namespace StudentDiaryWPF
{
    public class Repository
    {
        public bool TestDbConnection()
        {
            try
            {
                using (var testContext = new ApplicationDbContext(CreateConnectionString())) 
                { 
                    testContext.Database.Connection.Open();
                    testContext.Database.Connection.Close();
                }
                return true;
            }
            catch (Exception ex) 
            {

                return false;
            }
            
        }
        public List<Group> GetGroups()
        {
            
            using (var context = new ApplicationDbContext(CreateConnectionString()))
            {
                return context.Groups.ToList();
            }
        }

        public List<StudentWrapper> GetStudents(int groupId)
        {
            using (var context = new ApplicationDbContext(CreateConnectionString()))
            {
                var students = context.Students.Include(x => x.Group).Include(x => x.Ratings).AsQueryable();
                if (groupId != 0)
                {
                    students = students.Where(x => x.GroupId == groupId);
                }
                return students.ToList().Select(x => x.ToWrapper()).ToList();
            }
        }

        public void DeleteStudent(int id)
        {
            using (var context = new ApplicationDbContext(CreateConnectionString()))
            {
                var studentToDelete = context.Students.Find(id);
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            }
        }

        public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext(CreateConnectionString()))
            {
                var dbStudent = context.Students.Add(student);

                ratings.ForEach(x =>
                {
                    x.StudentId = dbStudent.Id;
                    context.Ratings.Add(x);
                });

                context.SaveChanges();
            }

        }
        private static void UpdateRate(Student student, List<Rating> newRatings, ApplicationDbContext context, List<Rating> studentsRatings, Subject subject)
         {
            var subRatings = studentsRatings.Where(x => x.SubjectId == (int)subject).Select(x => x.Rate);
            var newSubRatings = newRatings.Where(x => x.SubjectId == (int)subject).Select(x => x.Rate);

            var subRatingsToDelete = subRatings.Except(newSubRatings).ToList();
            var subRatingsToAdd = newSubRatings.Except(subRatings).ToList();

            subRatingsToDelete.ForEach(x =>
            {
                var ratingToDelete = context.Ratings.First(y => y.Rate == x && y.StudentId == student.Id && y.SubjectId == (int)subject);
                context.Ratings.Remove(ratingToDelete);
            }
            );

            subRatingsToAdd.ForEach(x =>
            {
                var ratingToAdd = new Rating
                {
                    Rate = x,
                    StudentId = student.Id,
                    SubjectId = (int)subject
                };
                context.Ratings.Add(ratingToAdd);
            });
        }
        public void UpdateStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext(CreateConnectionString()))
            {
                UpdateStudentProperties(student, context);

                var studentsRatings = GetStudentRating(student, context);

                UpdateRate(student, ratings, context, studentsRatings, Subject.Math);
                UpdateRate(student, ratings, context, studentsRatings, Subject.Technology);
                UpdateRate(student, ratings, context, studentsRatings, Subject.Physics);
                UpdateRate(student, ratings, context, studentsRatings, Subject.PolishLang);
                UpdateRate(student, ratings, context, studentsRatings, Subject.ForeignLang);

                context.SaveChanges();
            } 
        }
        private void UpdateStudentProperties(Student student, ApplicationDbContext context)
        {
            var studentToUpdate = context.Students.Find(student.Id);

            studentToUpdate.Activities = student.Activities;
            studentToUpdate.Comments = student.Comments;
            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.GroupId = student.GroupId;
        }

        private static List<Rating> GetStudentRating(Student student, ApplicationDbContext context)
        {
             return context.Ratings.Where(x => x.StudentId == student.Id).ToList();
        }

        public string CreateConnectionString()
        {
            string dbServerAddress = Properties.Settings.Default.dbServerAddress;
            string dbServerName = Properties.Settings.Default.dbServerName;
            string dbName = Properties.Settings.Default.dbName;
            string dbUserName = Properties.Settings.Default.dbUserName;
            string dbPassWord = Properties.Settings.Default.dbPassWord;

            //"Server=(local)\SQLEXPRESS;Database=StudentDiaryWPF;User Id=studentsAdmin;Password=12345;"
            return $"Server={dbServerAddress}\\{dbServerName};Database={dbName};User Id={dbUserName};Password={dbPassWord};";           
        }

    }
}
