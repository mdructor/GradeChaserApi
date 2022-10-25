using GradeChaser.Core;

namespace GradeChaser.Api.RequestTypes
{
    public class ConversionRequest
    {
        public RatingSystem From { get; init; }
        public RatingSystem To { get; init; }
        public string Rating { get; init; }
    }
}
