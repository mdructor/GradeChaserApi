using GradeChaser.Application.Exceptions;
using GradeChaser.Core;

namespace GradeChaser.Application.Services
{
    public class GradeConversionService : IGradeConversionService
    {
        private readonly List<string> _fontGrades = new() { "1", "2", "3", "4", "5", "5+", "6A", "6A+", "6B", "6B+", "6C", "6C+", "7A", "7A+", "7B", "7B+", "7C", "7C+", "8A", "8A+", "8B", "8B+", "8C", "8C+", "9A" };
        private readonly List<string> _vGrades = new() { "VB", "V0", "V1", "V2", "V3", "V4", "V5", "V6", "V7", "V8", "V9", "V10", "V11", "V12", "V13", "V14", "V15", "V16", "V17" };
        private readonly List<string> _ydsGrades = new() { "5.0", "5.1", "5.2", "5.3", "5.4", "5.5", "5.6", "5.7", "5.8", "5.9", "5.10A", "5.10B", "5.10C", "5.10D", "5.11A", "5.11B", "5.11C", "5.11D", 
                                                  "5.12A", "5.12B", "5.12C", "5.12D", "5.13A", "5.13B", "5.13C", "5.13D", "5.14A", "5.14B", "5.14C", "5.14D", "5.15A", "5.15B", "5.15C", "5.15D" };

        private bool ValidateGrade(RatingSystem system, string grade)
        {
            return system switch
            {
                RatingSystem.Font => _fontGrades.Contains(grade),
                RatingSystem.VScale => _vGrades.Contains(grade),
                RatingSystem.YDS => _ydsGrades.Contains(grade),
                _ => false,
            };
        }

        private static GradeValue ConvertFromYDS(RatingSystem to, string grade)
        {
            if (to == RatingSystem.YDS) return new GradeValue(to, grade);

            var isFont = to == RatingSystem.Font;

            var converted = grade switch
            {
                "5.0" => "-",
                "5.1" => "-", 
                "5.2" => isFont ? "1" : "-",
                "5.3" => isFont ? "2" : "-",
                "5.4" => isFont ? "3" : "-",
                "5.5" => isFont ? "4A" : "-",
                "5.6" => isFont ? "4B" : "-",
                "5.7" => isFont ? "4C" : "VB",
                "5.8" => isFont ? "5A" : "VB",
                "5.9" => isFont ? "5B" : "VB",
                "5.10A" => isFont ? "5C" : "V0",
                "5.10B" => isFont ? "6A" : "V0",
                "5.10C" => isFont ? "6A+" : "V1",
                "5.10D" => isFont ? "6B" : "V1",
                "5.11A" => isFont ? "6B+" : "V2",
                "5.11B" => isFont ? "6C" : "V2",
                "5.11C" => isFont ? "6C+" : "V3",
                "5.11D" => isFont ? "7A" : "V3",
                "5.12A" => isFont ? "7A+" : "V4",
                "5.12B" => isFont ? "7B" : "V5",
                "5.12C" => isFont ? "7B+" : "V5",
                "5.12D" => isFont ? "7C" : "V6",
                "5.13A" => isFont ? "7C+" : "V7",
                "5.13B" => isFont ? "8A" : "V7",
                "5.13C" => isFont ? "8A+" : "V8",
                "5.13D" => isFont ? "8B" : "V9",
                "5.14A" => isFont ? "8B+" : "V10",
                "5.14B" => isFont ? "8C" : "V11",
                "5.14C" => isFont ? "8C+" : "V12",
                "5.14D" => isFont ? "9A" : "V13",
                "5.15A" => isFont ? "9A+" : "V14",
                "5.15B" => isFont ? "9B" : "V15",
                "5.15C" => isFont ? "9B+" : "V16",
                "5.15D" => isFont ? "9C" : "V17",
                _ => throw new NotImplementedException()
            };

            return new GradeValue(to, converted);
        }

        private static GradeValue ConvertFromVScale(RatingSystem to, string grade)
        {
            if (to == RatingSystem.VScale) return new GradeValue(to, grade);

            var isFont = to == RatingSystem.Font;

            var converted = grade switch
            {
                "VB" => isFont ? "1-3" : "5.7-5.9",
                "V0" => isFont ? "4" : "5.10A/B",
                "V1" => isFont ? "5" : "5.10C/D",
                "V2" => isFont ? "5+" : "5.11A/B",
                "V3" => isFont ? "6A/6A+" : "5.11C/D",
                "V4" => isFont ? "6B/6B+" : "5.12A",
                "V5" => isFont ? "6C/6C+" : "5.12B/C",
                "V6" => isFont ? "7A" : "5.12D",
                "V7" => isFont ? "7A+" : "5.13A/B",
                "V8" => isFont ? "7B/7B+" : "5.13C",
                "V9" => isFont ? "7C" : "5.13D",
                "V10" => isFont ? "7C+" : "5.14A",
                "V11" => isFont ? "8A" : "5.14B",
                "V12" => isFont ? "8A+" : "5.14C",
                "V13" => isFont ? "8B" : "5.14D",
                "V14" => isFont ? "8B+" : "5.15A",
                "V15" => isFont ? "8C" : "5.15B",
                "V16" => isFont ? "8C+" : "5.15C",
                "V17" => isFont ? "9A" : "5.15D",
                _ => throw new NotImplementedException()
            };

            return new GradeValue(to, converted);
        }

        private static GradeValue ConvertFromFont(RatingSystem to, string grade)
        {
            if (to == RatingSystem.Font) return new GradeValue(to, grade);

            var isYds = to == RatingSystem.YDS;

            var converted = grade switch
            {
                "1" => isYds ? "5.2" : "VB",
                "2" => isYds ? "5.3" : "VB",
                "3" => isYds ? "5.4" : "VB",
                "4" => isYds ? "5.5-5.7" : "V0",
                "5" => isYds ? "5.8-5.9" : "V1",
                "5+" => isYds ? "5.10A" : "V2",
                "6A" => isYds ? "5.10B" : "V3",
                "6A+" => isYds ? "5.10C" : "V3",
                "6B" => isYds ? "5.10D" : "V4",
                "6B+" => isYds ? "5.11A" : "V4",
                "6C" => isYds ? "5.11B" : "V5",
                "6C+" => isYds ? "5.11C" : "V5",
                "7A" => isYds ? "5.11D" : "V6",
                "7A+" => isYds ? "5.12A" : "V7",
                "7B" => isYds ? "5.12B" : "V8",
                "7B+" => isYds ? "5.12C" : "V8",
                "7C" => isYds ? "5.12D" : "V9",
                "7C+" => isYds ? "5.13A" : "V10",
                "8A" => isYds ? "5.13B" : "V11",
                "8A+" => isYds ? "5.13C" : "V12",
                "8B" => isYds ? "5.13D" : "V13",
                "8B+" => isYds ? "5.14A" : "V14",
                "8C" => isYds ? "5.14B" : "V15",
                "8C+" => isYds ? "5.14C" : "V16",
                "9A" => isYds ? "5.14D" : "V17",
                _ => throw new NotImplementedException()
            };

            return new GradeValue(to, converted);
        }

        public GradeValue ConvertGradeValue(RatingSystem from, RatingSystem to, string grade)
        {
            grade = grade.Trim().ToUpper();

            if (!ValidateGrade(from, grade)) throw new InvalidGradeException();

            return from switch
            {
                RatingSystem.Font => ConvertFromFont(to, grade),
                RatingSystem.VScale => ConvertFromVScale(to, grade),
                RatingSystem.YDS => ConvertFromYDS(to, grade),
                _ => throw new NotImplementedException()
            };
        }

    }
}
