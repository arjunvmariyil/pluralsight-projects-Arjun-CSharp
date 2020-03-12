using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook:BaseGradeBook
    {
        public RankedGradeBook(string name, bool IsWeighted) :base(name,IsWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("You must have at least 5 students to do ranked grading.");
            }
            // We need to seprate each bucket of students by 20% and sort their average through descending order in 20% bucket.
            //So we have 10 students first 2 students will be in the first 20% bucket(garde A) next 20% bucket next 2 (grade b etc)
            //Then we need to grade them accordingly. 
            //In the case of 10 students Student with Highest and Second Highest grade will be stored in the sorted array gardes[0] and grades[1].
            //Next in gardes[2] and grades[3].
            // So if average greater than or equal to average in the last index of first 20% then we will give Grade A
            var thresholdToSeparate = (int)Math.Ceiling(Students.Count * 0.2);
            var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();

            if (averageGrade >= grades[thresholdToSeparate - 1])
                return 'A';
            if (averageGrade >= grades[(thresholdToSeparate * 2) - 1])
                return 'B';
            if (averageGrade >= grades[(thresholdToSeparate * 3) - 1])
                return 'C';
            if (averageGrade >= grades[(thresholdToSeparate * 4) - 1])
                return 'D';
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            else if (Students.Count >= 5)
            {
                base.CalculateStatistics();
            }
        }

        public override void CalculateStudentStatistics(string name)
        {

            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            else if (Students.Count>=5)
            {
                base.CalculateStudentStatistics(name);
                
            }

        }
    }
}
