using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            var sortedGrades = Students
                .OrderByDescending(student => student.AverageGrade)
                .Select(student => student.AverageGrade);

            var maxGradesPerBucket = Convert.ToInt32(Math.Ceiling(Students.Count * .20));

            var first20 = sortedGrades.Take(maxGradesPerBucket);
            var second20 = sortedGrades.Skip(maxGradesPerBucket).Take(maxGradesPerBucket);
            var third20 = sortedGrades.Skip(maxGradesPerBucket * 2).Take(maxGradesPerBucket);
            var fourth20 = sortedGrades.Skip(maxGradesPerBucket * 3).Take(maxGradesPerBucket);

            if (averageGrade >= first20.First() && averageGrade <= first20.Last())
            {
                return 'A';
            }

            if (averageGrade >= second20.First() && averageGrade <= second20.Last())
            {
                return 'B';
            }

            if (averageGrade >= third20.First() && averageGrade <= third20.Last())
            {
                return 'C';
            }

            if (averageGrade >= fourth20.First() && averageGrade <= fourth20.Last())
            {
                return 'D';
            }

            return 'F';
        }
    }
}