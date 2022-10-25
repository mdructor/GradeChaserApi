using GradeChaser.Core;

namespace GradeChaser.Application.Services
{
    public interface IGradeConversionService
    {
        public GradeValue ConvertGradeValue(RatingSystem from, RatingSystem to, string grade);
    }
}
