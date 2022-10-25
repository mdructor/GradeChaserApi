using GradeChaser.Api.RequestTypes;
using GradeChaser.Application.Exceptions;
using GradeChaser.Application.Services;
using GradeChaser.Core;
using Microsoft.AspNetCore.Mvc;

namespace GradeChaser.Api.Controllers
{

    [ApiController]
    [Route("api/convert")]
    public class ConversionController : ControllerBase
    {
        private readonly IGradeConversionService _gradeConversionService;
        public ConversionController(IGradeConversionService gradeConversionService)
        {
            _gradeConversionService = gradeConversionService;
        }

        [HttpPost]
        public ActionResult<GradeValue> Convert(ConversionRequest requestParams)
        {
            try
            {
                var result = _gradeConversionService.ConvertGradeValue(requestParams.From, requestParams.To, requestParams.Rating);
                return Ok(result);
            }
            catch (InvalidGradeException)
            {
                return BadRequest($"Invalid grade value entered for conversion: {requestParams.Rating}");
            }
        }
    }
}
