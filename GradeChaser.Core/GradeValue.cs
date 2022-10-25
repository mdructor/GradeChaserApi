using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeChaser.Core
{
    public record GradeValue
    {
        public RatingSystem System { get; init; }

        public string Grade { get; init; }

        public GradeValue(RatingSystem system, string grade)
        {
            System = system;
            Grade = grade;
        }
    }
}
